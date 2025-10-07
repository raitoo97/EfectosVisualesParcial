using UnityEngine;
public enum LeverAction
{
    FirstCinematic,
    SecondCinematic,
    OpenDoor
}
public class Lever : MonoBehaviour ,IInteractiveObject
{
    [SerializeField]private Animator leverAnimator;
    public LeverAction leverAction;
    [SerializeField]private CinematicDirector _cinematicDirector;
    [SerializeField]private Material _glowMaterial;
    private bool _canUseGlow;
    private void Awake()
    {
        leverAnimator = GetComponent<Animator>();
        _canUseGlow = true;
    }
    private void Start()
    {
        _cinematicDirector = CinematicDirector.instance;
    }
    public void Interact()
    {
        leverAnimator.SetBool("IsActivate", true);
        _glowMaterial.SetInt("_Toggle", 0);
        _canUseGlow = false;
    }
    public void Glow(bool ActivateGlow)
    {
        if (!_canUseGlow) return;
        if (ActivateGlow)
            _glowMaterial.SetInt("_Toggle", 1);
        else
            _glowMaterial.SetInt("_Toggle", 0);
    }
    public void OnAnimationEvent()
    {
        switch (leverAction)
        {
            case LeverAction.FirstCinematic:
                _cinematicDirector?.GetPlayableDirector(0).Play();
                break;
        }
    }
}
