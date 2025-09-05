using UnityEngine;
public class AnimationCanvas
{
    private Animator _animator;
    public AnimationCanvas(Animator _animator)
    {
        this._animator = _animator;
    }
    public void AimAnimation()
    {
        _animator.SetTrigger("ShootingAim");
    }
    public void StopAimAnimation()
    {
        _animator.ResetTrigger("ShootingAim");
    }
}
