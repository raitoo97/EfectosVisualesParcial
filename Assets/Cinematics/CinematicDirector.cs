using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CinematicDirector : MonoBehaviour
{
    public List <GameObject> objectsToDesactivate;
    public PlayableDirector director;
    public void DesactivateGun()
    {
        foreach (GameObject _currentObject in objectsToDesactivate)
        {
            _currentObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            director.Play();
        }
    }
}
