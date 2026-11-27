using UnityEngine;
using UnityEngine.Events;
public class LeverL0 : InteractiveObject
{
    private Animator leverAnimator;
    [SerializeField] private UnityEvent onActivate;
    private void Awake()
    {
        leverAnimator = GetComponent<Animator>();
        _canUseGlow = true;
    }
    public override void Interact()
    {
        if (!_canInteract) return;
        leverAnimator.SetBool("IsActivate", true);
        _glowMaterial.SetInt("_ActivateOutLine", 0);
        _canUseGlow = false;
        _canInteract = false;
    }
    public void OnAnimationEvent()
    {
        SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("InteractiveSound"), 1f, false);
        onActivate?.Invoke();
    }
}

