using UnityEngine;
public class Enemy : MonoBehaviour , IEnemy
{
    public Animator animator;
    private FSM _fsm;
    private void OnEnable()
    {
        _fsm = new FSM();
        _fsm.AddState(FSM.StateID.Chase, new ChaseState(this));
        _fsm.ChangeState(FSM.StateID.Chase);
    }
    private void Update()
    {
        _fsm.onUpdateState();
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("IsDead", true);
        }
    }
}
