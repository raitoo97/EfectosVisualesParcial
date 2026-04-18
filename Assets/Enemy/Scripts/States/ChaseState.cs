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
        var distanceToPlayer = _playerPos.transform.position - _enemy.transform.position;
        var canSeePlayer = LineOfSight.IsOnSight(_enemy.transform.position, _playerPos.transform.position);
        if (distanceToPlayer.magnitude <= _rangeAtack && canSeePlayer)
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
            Node currentNode = NodeManager.GetClosetNode(_enemy.transform.position);
            Node nextNode = NodeManager.GetClosetNode(_path[1]);
            if (currentNode != null && nextNode != null)
            {
                if (currentNode.GetConnectionType(nextNode) == ConnectionType.Jump)
                {
                    var jumpState = _fsm.GetState<JumpState>();
                    if (jumpState != null)
                    {
                        jumpState.SetJump(_enemy.transform.position, nextNode.transform.position);
                    }
                    _fsm.ChangeState(FSM.StateID.Jump);
                    return;
                }
            }
        }
        var distanceToTarget = currentTarget - _enemy.transform.position;
        _enemy.FlockingAndSeek(currentTarget);
        _enemy.RotateTo(currentTarget);
        if (distanceToTarget.magnitude < 2f)
            _path.RemoveAt(0);
        for (int i = 0; i < _path.Count - 1; i++)
            Debug.DrawLine(_path[i], _path[i + 1], Color.red);
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