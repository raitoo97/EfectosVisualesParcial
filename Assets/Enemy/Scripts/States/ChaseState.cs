using System.Collections.Generic;
using UnityEngine;
public class ChaseState : Istate
{
    private Enemy _enemy;
    private FSM _fsm;
    private Animator _animator;
    private List<Vector3> _path = new List<Vector3>();
    private Player _playerPos;
    private float _rangeAtack;
    private Vector3 _lastGoalPos;
    private float _repathTimer;
    private float _repathInterval = 10f;
    private float _repathDistance = 25f;
    public ChaseState(Enemy enemy, FSM fsm,Animator animator, Player target,float rangeAtack)
    {
        _enemy = enemy;
        _fsm = fsm;
        _animator = animator;
        _playerPos = target;
        _rangeAtack = rangeAtack;
    }
    public void OnEnter()
    {
        _animator.SetBool("IsRunning", true);
        _animator.SetBool("OnJump", false);
        _animator.SetBool("Ishoting", false);
        _enemy.ChangeMove(true);
        CleanUpPath();
        _lastGoalPos = _playerPos.transform.position;
        _repathTimer = 0f;
    }
    public void OnUpdate()
    {
        Vector3 origin = _enemy._eyePoint.position;
        Vector3 target = _playerPos.transform.position;
        var distanceToPlayer = _playerPos.transform.position - _enemy.transform.position;
        int mask = LayerMask.GetMask("Wall", "Ground");
        Vector3 losOrigin = origin + Vector3.up;
        Vector3 direction = (target - losOrigin).normalized;
        float distance = (target - losOrigin).magnitude;
        Debug.DrawLine(losOrigin, target, Color.yellow);
        if (Physics.Raycast(losOrigin, direction, out RaycastHit hit, distance, mask))
        {
            Debug.DrawLine(losOrigin, hit.point, Color.red);
            Debug.Log("LOS HIT: " + hit.collider.name);
        }
        else
        {
            Debug.DrawLine(losOrigin, target, Color.green);
            Debug.Log("LOS LIBRE");
        }
        var canSeePlayer = LineOfSight.IsOnSight(origin, target);
        if (_enemy.isOnGround && distanceToPlayer.magnitude <= _rangeAtack && canSeePlayer)
        {
            _fsm.ChangeState(FSM.StateID.Attack);
            return;
        }
        _repathTimer += Time.deltaTime;
        if (_path.Count == 0 || _repathTimer >= _repathInterval || (_playerPos.transform.position - _lastGoalPos).magnitude > _repathDistance)
        {
            CalculatePath(_playerPos.transform.position);
            _lastGoalPos = _playerPos.transform.position;
            _repathTimer = 0f;
        }
        if (_path.Count == 0)
            return;
        var currentTarget = _path[0];
        if (_path.Count >= 2)
        {
            Vector3 nextPos = _path[1];
            if (TryHandleJump(nextPos))
                return;
        }
        var distanceToTarget = currentTarget - _enemy.transform.position;
        _enemy.FlockingAndSeek(currentTarget);
        _enemy.RotateTo(currentTarget);
        if (distanceToTarget.magnitude < 2f)
            _path.RemoveAt(0);
        for (int i = 0; i < _path.Count - 1; i++)
            Debug.DrawLine(_path[i], _path[i + 1], Color.red);
    }
    private bool TryHandleJump(Vector3 nextPos)
    {
        Node currentNode = NodeManager.GetClosetNode(_enemy.transform.position);
        Node nextNode = NodeManager.GetClosetNode(nextPos);
        if (currentNode == null || nextNode == null)
            return false;

        if (currentNode.GetConnectionType(nextNode) != ConnectionType.Jump)
            return false;

        float heightDiff = nextPos.y - _enemy.transform.position.y;

        Vector3 flatEnemy = new Vector3(_enemy.transform.position.x, 0, _enemy.transform.position.z);
        Vector3 flatTarget = new Vector3(nextPos.x, 0, nextPos.z);
        float horizontalDist = Vector3.Distance(flatEnemy, flatTarget);


        if (Mathf.Abs(heightDiff) <= 0.2f && horizontalDist < 0.5f)
            return false;
        if (horizontalDist < 0.2f)
            return false;

        Vector3 midPoint = (_enemy.transform.position + nextPos) * 0.5f;
        Vector3 rayStart = midPoint + Vector3.up;
        Debug.DrawLine(rayStart, rayStart + Vector3.down * 2f, Color.blue);

        if (Physics.Raycast(midPoint + Vector3.up, Vector3.down, out RaycastHit hit, 2f, LayerMask.GetMask("Ground")))
        {
            float slope = Vector3.Angle(hit.normal, Vector3.up);
            if (slope < 45f)return false;
        }

        var jumpState = _fsm.GetState<JumpState>();
        if (jumpState != null)
        {
            jumpState.SetJump(_enemy.transform.position, nextPos);
        }
        _fsm.ChangeState(FSM.StateID.Jump);
        return true;
    }
    private void CalculatePath(Vector3 goalPosition)
    {
        _path = PathFinding.CalculateAStar(_enemy.transform.position, goalPosition);
    }
    private void CleanUpPath()
    {
        _path.Clear();
    }
    public void OnExit()
    {
        CleanUpPath();
        _enemy.ChangeMove(false);
    }
}