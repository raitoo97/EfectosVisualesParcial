using UnityEngine;
public abstract class InteractiveObject : MonoBehaviour , IGlow
{
    [SerializeField]protected Material _glowMaterial;
    protected bool _canUseGlow;
    protected bool _canInteract = true;
    public void Glow(bool ActivateGlow)
    {
        if (!_canUseGlow) return;
        if (ActivateGlow)
            _glowMaterial.SetInt("_ActivateOutLine", 1);
        else
            _glowMaterial.SetInt("_ActivateOutLine", 0);
    }
    public abstract void Interact();
}
