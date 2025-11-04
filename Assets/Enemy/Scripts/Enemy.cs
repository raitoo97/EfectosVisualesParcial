using UnityEngine;
public class Enemy : Agent , IEnemy
{
    public Animator animator;
    private FSM _fsm;
    [SerializeField] private float atackRange;
    private Player _player;
    private float _rotateSpeed = 120f;
    private void OnEnable()
    {
        animator.SetBool("IsDead", false);
    }
    override protected void Start()
    {
        base.Start();
        _fsm = new FSM();
        _player = GameManager.instance.player;
        _fsm.AddState(FSM.StateID.Chase, new ChaseState(this, _fsm, animator, _player, atackRange));
        _fsm.AddState(FSM.StateID.Attack, new AtackState(this, _fsm, animator, _player));
        _fsm.ChangeState(FSM.StateID.Chase);

    }
    protected override void Update()
    {
        _fsm.onUpdateState();
        base.Update();
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("IsDead", true);
        }
    }
    public void Dead()
    {
        gameObject.SetActive(false);
    }
    public void RotateTo(Vector3 target)
    {
        Vector3 dir = target - this.transform.position;
        dir.y = 0f;
        if (dir != Vector3.zero)
        {
            Quaternion desiredRot = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, desiredRot, _rotateSpeed * Time.deltaTime);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, atackRange);
        Gizmos.color = Color.blue;
        if(_player != null)
        {
            var dir = _player.transform.position - transform.position;
            Debug.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + dir,Color.magenta);
        }
    }
    public FSM GetFSM { get => _fsm; }
}
