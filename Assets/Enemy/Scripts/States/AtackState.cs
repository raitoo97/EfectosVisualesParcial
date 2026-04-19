using UnityEngine;
public class AtackState : Istate
{
    private Enemy _enemy;
    private FSM _fsm;
    private Animator _animator;
    private Player _playerPos;
    private Transform _aim;
    public AtackState(Enemy enemy,FSM fsm, Animator animator, Player target,Transform aim)
    {
        _fsm = fsm;
        _enemy = enemy;
        _animator = animator;
        _playerPos = target;
        _aim = aim;
    }
    public void OnEnter()
    {
        _animator.SetBool("IsRunning", false);
        _animator.SetBool("OnJump", false);
        _animator.SetBool("Ishoting", true);
        _enemy.ChangeMove(false);
    }
    public void OnUpdate()
    {
        var canSeePlayer = LineOfSight.IsOnSight(_enemy.transform.position, _playerPos.transform.position);
        var isInRange = Vector3.Distance(_enemy.transform.position, _playerPos.transform.position) <= _enemy.AttackRange;
        if (!canSeePlayer || !isInRange)
        {
            _fsm.ChangeState(FSM.StateID.Chase);
            return;
        }
        _enemy.RotateTo(_playerPos.transform.position);
        Vector3 sepForce = _enemy.GetSeparationForce();
        if (sepForce.sqrMagnitude > 0.01f)
        {
            _enemy.ChangeMove(true);
            _enemy.ApplySeparation(sepForce);
        }
        else
        {
            _enemy.ChangeMove(false);
            _enemy.StopMovement();
        }
    }
    public void Shoot()
    {
        var bullet = PoolBulletEnemy.instance.GetBullet();
        bullet.transform.position = _aim.position;
        Vector3 directionToPlayer = (_playerPos.transform.position - _aim.position).normalized;
        bullet.transform.forward = directionToPlayer;
        bullet.SetActive(true);
    }
    public void OnExit()
    {
        _enemy.ChangeMove(true);
    }
}
