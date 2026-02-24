using UnityEngine;
public abstract class InteractiveObject : MonoBehaviour , IGlow
{
    [SerializeField]protected Material _glowMaterial;
    protected bool _canUseGlow = true;
    protected bool _canInteract = true;
    private bool _isAlreadySong = false;
    public void Glow(bool ActivateGlow)
    {
        if (!_canUseGlow) return;
        if (ActivateGlow)
        {
            if (!_isAlreadySong)
            {
                SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("Glow"), 1f, false);
                _isAlreadySong = true;
            }
            _glowMaterial.SetInt("_ActivateOutLine", 1);
        }
        else
        {
            if (_isAlreadySong)
            {
                SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("NotGlow"), 1f, false);
                _isAlreadySong = false;
            }
            _glowMaterial.SetInt("_ActivateOutLine", 0);
        }
    }
    public abstract void Interact();
}