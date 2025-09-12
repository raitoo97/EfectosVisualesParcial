using UnityEngine;
public class ControlCanvas : MonoBehaviour
{
    private AnimationCanvas _animationCanvas;
    [SerializeField]private Animator _animator;
    private void OnEnable()
    {
        _animationCanvas = new AnimationCanvas(_animator);
    }
    private void Update()
    {
        var IsShooting = PlayerInputs.instance.ShootAction();
        var IsRunning = PlayerInputs.instance.RunAction();
        var isGrounded = GameManager.instance.player.IsGrounded;    
        if (IsShooting && !IsRunning && isGrounded)
        {
            _animationCanvas?.AimAnimation();
        }
        else
        {
            _animationCanvas?.StopAimAnimation();
        }
    }
    private void OnDisable()
    {
        _animationCanvas = null;
    }
}
