using UnityEngine;
public class Enemy : Agent , IEnemy
{
    public Animator animator;
    private FSM _fsm;
    [SerializeField]private float _chaseRange;
    private void OnEnable()
    {
        animator.SetBool("IsDead", false);
        _fsm = new FSM();
        _fsm.AddState(FSM.StateID.Chase, new ChaseState(this,_fsm,animator));
        _fsm.AddState(FSM.StateID.Idle, new IdleState(this,_fsm,animator,_chaseRange));
        _fsm.AddState(FSM.StateID.Attack, new AtackState(this,_fsm,animator));
        _fsm.ChangeState(FSM.StateID.Chase);
    }
    protected override void Update()
    {
        _fsm.onUpdateState();
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("IsDead", true);
        }
        base.Update();
    }
    public void Dead()
    {
        gameObject.SetActive(false);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
    }
    public FSM GetFSM { get => _fsm; }
}
