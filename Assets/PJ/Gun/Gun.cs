using UnityEngine;
public class Gun : MonoBehaviour
{
    private GunShoot _gunShoot;
    private GunAnimation _gunAnimation;
    [SerializeField]private Animator _animator;
    [SerializeField]private ParticleSystem _bulletParticles;
    public Transform gunSight;
    public Light gunLight;
    private void OnEnable()
    {
        _gunShoot = new GunShoot(gunSight, gunLight, _bulletParticles);
        _gunAnimation = new GunAnimation(_animator);
    }
    private void Update()
    {
        _gunShoot?.OnUpdate();
        var IsShooting = PlayerInputs.instance.ShootAction();
        var IsRunning = PlayerInputs.instance.RunAction();
        var IsGrounded = GameManager.instance.player.IsGrounded;
        var MoveVector = GameManager.instance.player.MoveVector;
        if (IsShooting && !IsRunning)
        {
            _gunAnimation?.ShootAnimation();
        }
        else
        {
            _gunAnimation?.CancelShootAnimation();
        }
        if (IsRunning && MoveVector != Vector2.zero)
        {
            _gunAnimation?.RunningAnimation(true);
        }
        else
        {
            _gunAnimation?.RunningAnimation(false);
        }
        if (!IsGrounded)
        {
            _gunAnimation?.JumpAnimation(true);
        }
        else
        {
            _gunAnimation?.JumpAnimation(false);
        }
    }
    public void CallShootFunction()
    {
        SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("PjShoot"), 0.2f, false);
        _gunShoot?.Shoot();
    }
    private void OnDisable()
    {
        _gunShoot = null;
        _gunAnimation = null;
    }
}
