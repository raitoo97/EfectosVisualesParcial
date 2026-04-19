using UnityEngine;
public class JumpState : Istate
{
    private Animator _animator;
    private Enemy _enemy;
    private FSM _fsm;
    private Vector3 _startPos;
    private Vector3 _targetPos;
    private float _timer;
    private float _duration = 0.5f;
    public JumpState(Enemy enemy, FSM fsm, Animator animator)
    {
        _enemy = enemy;
        _fsm = fsm;
        _animator = animator;
    }
    public void SetJump(Vector3 start, Vector3 target)
    {
        _startPos = start;
        _targetPos = target;
    }
    public void OnEnter()
    {
        _animator.SetBool("IsRunning", false);
        _animator.SetBool("Ishoting", false);
        _animator.SetBool("OnJump", true);
        _timer = 0f;
        _enemy.ChangeMove(false);
        _enemy.StopMovement();
        _enemy._onJumpFinish = OnJumpFinished;
        _enemy.IsJumping = true;
    }
    public void OnUpdate()
    {
        _timer += Time.deltaTime;
        float t = _timer / _duration;
        t = Mathf.Clamp01(t);
        Vector3 horizontal = Vector3.Lerp(new Vector3(_startPos.x, 0, _startPos.z),new Vector3(_targetPos.x, 0, _targetPos.z),t);
        float baseY = Mathf.Lerp(_startPos.y, _targetPos.y, t);
        float jumpY = Mathf.Sin(t * Mathf.PI) * 2f;
        Vector3 pos = new Vector3(horizontal.x, baseY + jumpY, horizontal.z);
        _enemy.transform.position = pos;
        if (t >= 1f)
        {
            OnJumpFinished();
            return;
        }
    }
    public void OnJumpFinished()
    {
        _enemy.transform.position = _targetPos;
        _fsm.ChangeState(FSM.StateID.Chase);
    }
    public void OnExit()
    {
        _enemy._onJumpFinish = null;
        _enemy.ChangeMove(true);
        _enemy.StopMovement();
        _enemy.IsJumping = false;
    }
}

