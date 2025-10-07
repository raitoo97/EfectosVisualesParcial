using UnityEngine;
public class Console : MonoBehaviour ,IInteractiveObject
{
    [SerializeField] private Material _glowMaterial;
    private bool _canUseGlow;
    private bool _canInteract = true;   
    private void Awake()
    {
        _canUseGlow = true;
    }
    public void Glow(bool ActivateGlow)
    {
        if (!_canUseGlow) return;
        if (ActivateGlow)
            _glowMaterial.SetInt("_Toggle", 1);
        else
            _glowMaterial.SetInt("_Toggle", 0);
    }
    public void Interact()
    {
        if (!_canInteract) return;
        CinematicDirector.instance.GetPlayableDirector(1).Play();
        _glowMaterial.SetInt("_Toggle", 0);
        _canInteract = false;
        _canUseGlow = false;
    }
}
