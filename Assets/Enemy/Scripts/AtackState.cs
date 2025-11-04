using UnityEngine;
public class AtackState : Istate
{
    private Enemy _enemy;
    private FSM _fsm;
    private Animator _animator;
    private Player _playerPos;
    public AtackState(Enemy enemy,FSM fsm, Animator animator, Player target)
    {
        _fsm = fsm;
        _enemy = enemy;
        _animator = animator;
        _playerPos = target;
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
        Shoot();
    }
    public void Shoot()
    {
        Debug.Log("Pew Pew");
    }
    public void OnExit()
    {
        _enemy.ChangeMove(true);
        Debug.Log("Exiting Atack State");
    }
}
