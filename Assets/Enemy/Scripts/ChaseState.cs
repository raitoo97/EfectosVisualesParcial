using System.Collections.Generic;
using UnityEngine;
public class ChaseState : Istate
{
    private Enemy _enemy;
    private FSM _fsm;
    private Animator _animator;
    private List<Vector3> _path = new List<Vector3>();
    private List<Node> _nodesTempList = new List<Node>();
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
        if (canSeePlayer)
        {
            CleanUpPath();
            _enemy.GetSeekForce(_playerPos.transform.position);
            _enemy.RotateTo(_playerPos.transform.position);
            return;
        }
        if (_path.Count > 0)
        {
            var currentTarget = _path[0];
            var distanceToTarget = currentTarget - _enemy.transform.position;
            _enemy.GetSeekForce(currentTarget);
            _enemy.RotateTo(currentTarget);
            if (distanceToTarget.magnitude < 2f)
                _path.RemoveAt(0);
            for (int i = 0; i < _path.Count - 1; i++)
                Debug.DrawLine(_path[i], _path[i + 1], Color.red);
            return;
        }
        _repathTimer += Time.deltaTime;
        if (_path.Count == 0 || _repathTimer >= _repathInterval || (_playerPos.transform.position - _lastGoalPos).magnitude > _repathDistance)
        {
            CalculatePath(_playerPos.transform.position);
            _lastGoalPos = _playerPos.transform.position;
            _repathTimer = 0f;
        }
    }
    private void CalculatePath(Vector3 goalPosition)
    {
        CleanUpPath();
        var startNode = NodeManager.GetClosetNode(_enemy.transform.position);
        var endNode = NodeManager.GetClosetNode(goalPosition);
        _nodesTempList = PathFinding.CalculateAStar(startNode, endNode);
        foreach (var node in _nodesTempList)
        {
            _path.Add(node.transform.position);
        }
        _path.Add(goalPosition);
    }
    private void CleanUpPath()
    {
        _nodesTempList.Clear();
        _path.Clear();
    }
    public void OnExit()
    {
        CleanUpPath();
        _enemy.ChangeMove(false);
    }
}
