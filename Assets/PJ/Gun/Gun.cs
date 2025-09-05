using UnityEngine;
public class Gun : MonoBehaviour
{
    private GunShoot _gunShoot;
    private GunAnimation _gunAnimation;
    [SerializeField]private Animator _animator;
    public Transform gunSight;
    public Light gunLight;
    private void OnEnable()
    {
        _gunShoot = new GunShoot(gunSight, gunLight);
        _gunAnimation = new GunAnimation(_animator);
    }
    private void Update()
    {
        _gunShoot?.OnUpdate();
        var IsShooting = PlayerInputs.instance.ShootAction();
        var IsRunning = PlayerInputs.instance.RunAction();
        var IsGrounded = GameManager.instance.player.IsGrounded;
        if (IsShooting && !IsRunning)
        {
            _gunAnimation?.ShootAnimation();
        }
        else
        {
            _gunAnimation?.CancelShootAnimation();
        }
        if (IsRunning)
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
        _gunShoot?.Shoot();
    }
    private void OnDisable()
    {
        _gunShoot = null;
        _gunAnimation = null;
    }
}
