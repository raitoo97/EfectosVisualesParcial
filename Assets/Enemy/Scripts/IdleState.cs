using UnityEngine;
public class IdleState : Istate
{
    private Animator _animator;
    private Enemy _enemy;
    private FSM _fsm;
    private float _chaseRange;
    public IdleState(Enemy enemy, FSM fsm,Animator animator, float chaseRange)
    {
        _animator = animator;
        _enemy = enemy;
        _fsm = fsm;
        _chaseRange = chaseRange;
    }
    public void OnEnter()
    {
        _animator.SetBool("Ishoting", false);
        _animator.SetBool("IsRunning", false);
    }
    public void OnExit()
    {
        Debug.Log("Exiting Idle State");
    }
    public void OnUpdate()
    {
        var dist = GameManager.instance.player.transform.position - _enemy.transform.position;
        if (dist.magnitude < _chaseRange)
        {
            _fsm.ChangeState(FSM.StateID.Chase);
        }
    }
}
