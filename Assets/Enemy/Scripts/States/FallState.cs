using UnityEngine;
public class FallState : Istate
{
    private Enemy _enemy;
    private FSM _fsm;
    private Animator _animator;
    private float _fallSpeed = 5f;
    public FallState(Enemy enemy, FSM fsm, Animator animator)
    {
        _enemy = enemy;
        _fsm = fsm;
        _animator = animator;
    }
    public void OnEnter()
    {
        _animator.SetBool("IsRunning", false);
        _animator.SetBool("OnJump", false);
        _animator.SetBool("Ishoting", false);
        _enemy.ChangeMove(false);
        _enemy.StopMovement();
    }
    public void OnUpdate()
    {
        _enemy.transform.position += Vector3.down * _fallSpeed * Time.deltaTime;
        if (_enemy.isOnGround)
        {
            _fsm.ChangeState(FSM.StateID.Chase);
        }
    }
    public void OnExit()
    {
        _enemy.ChangeMove(true);
    }
}
