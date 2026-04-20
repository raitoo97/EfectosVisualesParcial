using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
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
    [HideInInspector]private Shield _shieldChildRef;
    private EnemyGetHit _hitEffect;
    List<Material> allHitMaterials = new List<Material>();
    public VisualEffect hitParticleEffect;
    public VisualEffect hitAcidEffect;
    private bool _isDead = false;
    private AudioSource _audioSource;
    private bool _isInAcid = false;
    public Action _onJumpFinish;
    public Transform _eyePoint;
    public bool isOnGround;
    [SerializeField] private Transform _checkGround;
    public Transform _emergencyCheck;
    public bool isOnJump;
    public SpawnEnemy spawner;
    private void Awake()
    {
        Renderer[] allRenderers = GetComponentsInChildren<Renderer>(true);
        const int HIT_MATERIAL_INDEX = 1;
        foreach (Renderer renderer in allRenderers)
        {
            Material[] currentMaterials = renderer.materials;
            if (currentMaterials.Length > HIT_MATERIAL_INDEX)
            {
                Material hitMaterialInstance = currentMaterials[HIT_MATERIAL_INDEX];
                allHitMaterials.Add(hitMaterialInstance);
            }
        }
        _life = new Life(_maxHealth);
        _hitEffect = new EnemyGetHit(allHitMaterials, this);
    }
    private void OnEnable()
    {
        _player = GameManager.instance.player;
        _fsm = new FSM();
        _fsm.AddState(FSM.StateID.Attack, new AtackState(this, _fsm, animator, _player, _aim));
        _fsm.AddState(FSM.StateID.Chase, new ChaseState(this, _fsm, animator, _player, _atackRange));
        _fsm.AddState(FSM.StateID.Jump, new JumpState(this, _fsm, animator));
        _fsm.AddState(FSM.StateID.Fall, new FallState(this, _fsm, animator));
        _fsm.AddState(FSM.StateID.Emergency, new EmergencyState(this, _fsm));
        _fsm.ChangeState(FSM.StateID.Chase);
        if (animator != null) animator.SetBool("IsDead", false);
        if (_life != null) _life.SetHealthToMax();
        if (_shieldChildRef != null) _shieldChildRef.ActivateShield();
        if (_hitEffect != null) _hitEffect.OnEnable();
        if(hitParticleEffect != null) hitParticleEffect.Stop();
        if (hitAcidEffect != null) hitAcidEffect.Stop();
        if (_audioSource != null)
        {
            _audioSource.Stop();
            _audioSource = null;
        }
        _isInAcid = false;
        _isDead = false;
        spawner = null;
    }
    override protected void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        CheckGround();
        bool inside = Physics.CheckSphere(_emergencyCheck.position, 0.3f, _groundMask);
        if (inside)
        {
            if (!(_fsm.getCurrentState is EmergencyState))
            {
                _fsm.ChangeState(FSM.StateID.Emergency);
            }
        }
        if (_fsm != null)
        {
            var current = _fsm.getCurrentState;
            if (!isOnGround && !isOnJump)
            {
                if (!(current is FallState))
                {
                    _fsm.ChangeState(FSM.StateID.Fall);
                    return;
                }
            }
            _fsm.onUpdateState();
        }
        base.Update();
    }
    public void Dead()
    {
        spawner?.NotifyEnemyDeath();
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
            SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("EnemyShoot"), 0.1f, false);
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
        Gizmos.DrawWireSphere(_checkGround.position, 0.5f);
        bool inside = Physics.CheckSphere(_emergencyCheck.position, 0.3f, LayerMask.GetMask("Ground"));
        Gizmos.color = inside ? Color.red : Color.green;
        Gizmos.DrawWireSphere(_emergencyCheck.position, 0.3f);
    }
    public void TakeDamage(float damage)
    {
        _life.TakeDamage(damage, ChangeStateDead);
        if (_life.GetHealth <= 0 && !_isDead)
        {
            _isDead = true;
            SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("EnemyDeath"), 1f, false);
        }
        _hitEffect.ActivteCorutineDamageHit(Color.red * 2f);
    }
    public void TakeAcidDamage(float damage)
    {
        _life.TakeDamage(damage, ChangeStateDead);
        if (_life.GetHealth <= 0 && !_isDead)
        {
            _isDead = true;
            SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("EnemyDeath"),1f,false);
        }
        _hitEffect.ActivteCorutineDamageHit(Color.green * 2f);
    }
    public void ReceiveAreaDamage(float damage, Vector3 hitPos)
    {
        if (_shieldChildRef != null && _shieldChildRef.gameObject.activeInHierarchy)
        {
            _shieldChildRef.OnImpact(hitPos);
            _shieldChildRef.TakeDamage(damage);
        }
        else
        {
            TakeAcidDamage(damage);
        }
    }
    private void ChangeStateDead()
    {
        animator.SetBool("IsDead", true);
    }
    public void OnEnterAcid()
    {
        if (_isInAcid) return;
        _isInAcid = true;
        if (hitAcidEffect != null)
            hitAcidEffect.Play();
        _audioSource = SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("AcidDamage"),1f,true);
    }
    public void OnExitAcid()
    {
        if (!_isInAcid) return;
        _isInAcid = false;
        if (hitAcidEffect != null)
            hitAcidEffect.Stop();
        if (_audioSource != null)
        {
            _audioSource.Stop();
            _audioSource = null;
        }
    }
    private void OnDisable()
    {
        _fsm = null;
        if (hitParticleEffect != null) hitParticleEffect.Stop();
        if (hitAcidEffect != null) hitAcidEffect.Stop();
        if (_audioSource != null)
        {
            _audioSource.Stop();
            _audioSource = null;
        }
        _isInAcid = false;
    }
    public void PlayParticleDeath()
    {
        hitParticleEffect.Play();
    }
    public void OnJumpFinish()
    {
        _onJumpFinish?.Invoke();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (animator == null || _player == null || _isDead)
        {
            animator.SetLookAtWeight(0f);
            return;
        }
        animator.SetLookAtWeight(1f, 0.9f, 1f, 1f, 0.5f);
        Vector3 targetLookPos = _player.transform.position + Vector3.up * 1.5f;
        animator.SetLookAtPosition(targetLookPos);
    }
    private void CheckGround()
    {
        isOnGround = Physics.CheckSphere(_checkGround.position, 0.5f, _groundMask);
    }
    public FSM GetFSM { get => _fsm; }
    public Life GetLife { get => _life; }
    public float AttackRange { get => _atackRange; }
}