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
        _enemy._onJumpFinish = OnJumpFinished;
    }
    public void OnUpdate()
    {
        _timer += Time.deltaTime;
        float t = _timer / _duration;
        t = Mathf.Clamp01(t);
        Vector3 pos = Vector3.Lerp(_startPos, _targetPos, t);
        pos.y += Mathf.Sin(t * Mathf.PI) * 2f;
        _enemy.transform.position = pos;
        if (t >= 1f)
        {
            OnJumpFinished();
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
    }
}

