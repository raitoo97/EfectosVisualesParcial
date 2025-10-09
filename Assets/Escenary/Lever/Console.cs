public class Console : InteractiveObject
{
    private void Awake()
    {
        _canUseGlow = true;
    }
    public override void Interact()
    {
        if (!_canInteract) return;
        CinematicDirector.instance.GetPlayableDirector(1).Play();
        _glowMaterial.SetInt("_ActivateOutLine", 0);
        _canUseGlow = false;
        _canInteract = false;
    }
}
