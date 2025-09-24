using UnityEngine;
public class Lever : MonoBehaviour ,IInteractiveObject
{
    [SerializeField] private Animator leverAnimator;
    private void Awake()
    {
        leverAnimator = GetComponent<Animator>();
    }
    public void Interact()
    {
        leverAnimator.SetBool("IsActivate", true);
    }
}
