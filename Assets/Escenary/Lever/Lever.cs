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
    private void Awake()
    {
        leverAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        _cinematicDirector = CinematicDirector.instance;
    }
    public void Interact()
    {
        leverAnimator.SetBool("IsActivate", true);
    }
    public void Glow(bool ActivateGlow)
    {
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
