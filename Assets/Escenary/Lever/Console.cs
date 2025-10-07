using UnityEngine;
public class Console : MonoBehaviour ,IInteractiveObject
{
    [SerializeField] private Material _glowMaterial;
    public void Glow(bool ActivateGlow)
    {
        if (ActivateGlow)
            _glowMaterial.SetInt("_Toggle", 1);
        else
            _glowMaterial.SetInt("_Toggle", 0);
    }
    public void Interact()
    {
        CinematicDirector.instance.GetPlayableDirector(1).Play();
    }
}
