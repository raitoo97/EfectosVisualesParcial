using UnityEngine;
public class Console : MonoBehaviour ,IInteractiveObject
{
    public void Interact()
    {
        CinematicDirector.instance.GetPlayableDirector(1).Play();
    }
}
