using UnityEngine;
public enum LeverAction
{
    FirstCinematic,
    SecondCinematic,
    OpenDoor
}
public class Lever : MonoBehaviour ,IInteractiveObject
{
    [SerializeField] private Animator leverAnimator;
    public LeverAction leverAction;
    private void Awake()
    {
        leverAnimator = GetComponent<Animator>();
    }
    public void Interact()
    {
        leverAnimator.SetBool("IsActivate", true);
    }
    public void OnAnimationEvent()
    {
        switch (leverAction)
        {
            case LeverAction.FirstCinematic:
                CinematicDirector.instance?.GetDirectorFirstCinematic.Play();
                break;
        }
    }
}
