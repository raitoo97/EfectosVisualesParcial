using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    private AnimationCanvas _animationCanvas;
    [SerializeField]private Animator _animator;
    [SerializeField]private GameObject _aim;
    [SerializeField]private Text _timer;
    [SerializeField]private TextMeshProUGUI _granades;
    [SerializeField]private TextMeshProUGUI _talkWhitNullText;
    [SerializeField]private Slider _healthSlider;
    public static CanvasManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        UpdateGranades(3);
        GameManager.instance.player.GetComponent<Player>().GetPlayerRecolectObjects.OnBombsCountChanged += UpdateGranades;
        GameManager.instance.player.GetComponent<Player>().GetLife.ChangeLife += UpdateHealth;
        ShowTalkWhitNullText(false);
    }
    private void OnEnable()
    {
        _animationCanvas = new AnimationCanvas(_animator);
        GameManager.instance.Timer.FinishTimer += HiddenTime;
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
    public void UpdateGranades(int granades)
    {
        _granades.text = granades.ToString();
    }
    public void UpdateHealth(float health)
    {
        _healthSlider.value = health;
    }
    public void ShowTalkWhitNullText(bool canInteract)
    {
        _talkWhitNullText.gameObject.SetActive(canInteract);
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
        _aim.GetComponent<Image>().color = color;
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
