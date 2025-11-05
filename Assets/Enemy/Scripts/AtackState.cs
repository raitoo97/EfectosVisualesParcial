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
        _animator.SetBool("Ishoting", true);
        _enemy.ChangeMove(false);
    }
    public void OnUpdate()
    {
        var canSeePlayer = LineOfSight.IsOnSight(_enemy.transform.position, _playerPos.transform.position);
        if (!canSeePlayer)
        {
            _fsm.ChangeState(FSM.StateID.Chase);
            return;
        }
        _enemy.RotateTo(_playerPos.transform.position);
    }
    public void Shoot()
    {
        var bullet = PoolBulletEnemy.instance.GetBullet();
        bullet.transform.position = _aim.position;
        bullet.transform.rotation = _aim.rotation;
    }
    public void OnExit()
    {
        _enemy.ChangeMove(true);
        Debug.Log("Exiting Atack State");
    }
}
