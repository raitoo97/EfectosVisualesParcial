using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    private AnimationCanvas _animationCanvas;
    [SerializeField]private Animator _animator;
    [SerializeField]private GameObject _aim;
    [SerializeField]private Text _timer;
    public static CanvasManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
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
        _timer.text = GameManager.instance.GetTime;
    }
    public void ShowTime()
    {
        _timer.gameObject.SetActive(true);
    }
    public void HiddenTime()
    {
        _timer.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        _animationCanvas = null;
    }
    public void ChangeColorAim(Color color)
    {
        _aim.GetComponent<UnityEngine.UI.Image>().color = color;
    }
    public void FadeIn()
    {
        _animator.SetTrigger("FadeIn");
    }
    public void FadeOut()
    {
        _animator.SetTrigger("FadeOut");
    }
}
