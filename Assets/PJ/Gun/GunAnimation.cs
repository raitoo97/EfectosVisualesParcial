using UnityEngine;
public class GunAnimation
{
    private Animator _animator;
    public GunAnimation(Animator _animator)
    {
        this._animator = _animator;
    }
    public void ShootAnimation()
    {
        _animator?.SetTrigger("IsFire");
    }
    public void CancelAnimation()
    {
        _animator?.ResetTrigger("IsFire");
    }
    public void RunningAnimation(bool isRunning)
    {
        _animator?.SetBool("IsRunning", isRunning);
    }
}
