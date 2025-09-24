using UnityEngine;
public class CanvasManager : MonoBehaviour
{
    private AnimationCanvas _animationCanvas;
    [SerializeField]private Animator _animator;
    [SerializeField]private GameObject _aim;
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
        if (GameManager.instance.player.GetComponent<Player>().GetPlayerController.ViewEnemy)
        {
            ChangeColorAim(Color.red);
        }
        else
        {
            ChangeColorAim(Color.green);
        }
    }
    private void OnDisable()
    {
        _animationCanvas = null;
    }
    public void ChangeColorAim(Color color)
    {
        _aim.GetComponent<UnityEngine.UI.Image>().color = color;
    }
}
