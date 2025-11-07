using UnityEngine;
public class Enemy : Agent , IEnemy ,ITakeDamage
{
    public Animator animator;
    private FSM _fsm;
    [SerializeField]private float _atackRange;
    [SerializeField]private Transform _aim;
    private Player _player;
    private float _rotateSpeed = 120f;
    [SerializeField]private float _maxHealth;
    private Life _life;
    [SerializeField]private Shield _shieldChildRef;
    private void Awake()
    {
        _life = new Life(_maxHealth);
    }
    private void OnEnable()
    {
        if(animator != null)
        {
            animator.SetBool("IsDead", false);
        }
        else
        {
            Debug.LogWarning("Animator reference is missing on Enemy.");
        }
        if (_life != null)
        {
            _life.SetHealthToMax();
        }
        else
        {
            Debug.LogWarning("Life reference is missing on Enemy.");
        }
        if (_shieldChildRef != null)
        {
            _shieldChildRef.ActivateShield();
        }
        else
        {
            Debug.LogWarning("Shield reference is missing on Enemy.");
        }
    }
    override protected void Start()
    {
        base.Start();
        _fsm = new FSM();
        _player = GameManager.instance.player;
        _fsm.AddState(FSM.StateID.Chase, new ChaseState(this, _fsm, animator, _player, _atackRange));
        _fsm.AddState(FSM.StateID.Attack, new AtackState(this, _fsm, animator, _player,_aim));
        _fsm.ChangeState(FSM.StateID.Chase);
    }
    protected override void Update()
    {
        _fsm.onUpdateState();
        base.Update();
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
    public void OnShootEvent()
    {
        if (_fsm == null) return;
        var currentState = _fsm.getCurrentState;
        if (currentState is AtackState attackState)
        {
            attackState.Shoot();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _atackRange);
        if(_player != null)
        {
            var dir = _player.transform.position - transform.position;
            Debug.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + dir,Color.magenta);
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _SeparationRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundCheckObject.position, _groundCheckRadius);
    }
    public void TakeDamage(float damage)
    {
        _life.TakeDamage(damage, ChangeStateDead);
    }
    private void ChangeStateDead()
    {
        animator.SetBool("IsDead", true);
    }
    public FSM GetFSM { get => _fsm; }
    public Life GetLife { get => _life; }
}
