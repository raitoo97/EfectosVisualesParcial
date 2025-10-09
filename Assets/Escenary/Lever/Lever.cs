using UnityEngine;
public enum LeverAction
{
    FirstCinematic,
    SecondCinematic,
    OpenDoor
}
public class Lever : InteractiveObject
{
    [SerializeField]private Animator leverAnimator;
    public LeverAction leverAction;
    [SerializeField]private CinematicDirector _cinematicDirector;
    private void Awake()
    {
        leverAnimator = GetComponent<Animator>();
        _canUseGlow = true;
    }
    private void Start()
    {
        _cinematicDirector = CinematicDirector.instance;
    }
    public override void Interact()
    {
        leverAnimator.SetBool("IsActivate", true);
        _glowMaterial.SetInt("_ActivateOutLine", 0);
        _canUseGlow = false;
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
