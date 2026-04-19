using UnityEngine;
public class FallState : Istate
{
    private Enemy _enemy;
    private FSM _fsm;
    private Animator _animator;
    private float _fallSpeed = 7.5f;
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
        Vector3 origin = _enemy.transform.position;
        Vector3 direction = Vector3.down;
        Debug.DrawRay(origin, direction * 1f, Color.white);
        if (Physics.Raycast(_enemy.transform.position, Vector3.down, out RaycastHit hit, 1f, _enemy.GroundMask))
        {
            if (hit.distance <= 0.2f)
            {
                Vector3 pos = _enemy.transform.position;
                pos.y = hit.point.y;
                _enemy.transform.position = pos;

                _fsm.ChangeState(FSM.StateID.Chase);
            }
        }
    }
    public void OnExit()
    {
        _enemy.ChangeMove(true);
    }
}
