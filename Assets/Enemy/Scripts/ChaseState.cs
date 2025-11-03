using UnityEngine;
public class ChaseState : Istate
{
    private Enemy _enemy;
    private FSM _fsm;
    private Animator _animator;
    public ChaseState(Enemy enemy,FSM fsm,Animator animator)
    {
        _enemy = enemy;
        _fsm = fsm;
        _animator = animator;
    }
    public void OnEnter()
    {
        _animator.SetBool("IsRunning", true);
        _animator.SetBool("Ishoting", false);
    }
    public void OnExit()
    {
        Debug.Log("Exiting Chase State");
    }
    public void OnUpdate()
    {
        var dir = GameManager.instance.player.transform.position - _enemy.transform.position;
        dir.y = 0;
        if(dir != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(dir);
            _enemy.transform.rotation = Quaternion.RotateTowards(_enemy.transform.rotation, targetRotation, Time.deltaTime * 180f);
        }
        _enemy.GetSeekForce(GameManager.instance.player.transform.position);
    }
}
