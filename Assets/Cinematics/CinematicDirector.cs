using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CinematicDirector : MonoBehaviour
{
    [SerializeField]private List <GameObject> objectsToDesactivate;
    [SerializeField]private PlayableDirector directorFirstCinematic;
    [Header("FirstCinematic")]
    [SerializeField]private GameObject _waterDrop;
    private FirstCinematic _firstCinematic;
    public static CinematicDirector instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void OnEnable()
    {
        _firstCinematic = new FirstCinematic(_waterDrop.transform, this);
    }
    public void DesactivateGunAndPlayer()
    {
        foreach (GameObject _currentObject in objectsToDesactivate)
        {
            _currentObject.SetActive(false);
        }
        GameManager.instance.player.GetPlayerController._isOnCinematic = true;
    }
    public void ActivateGunAndPlayer()
    {
        foreach (GameObject _currentObject in objectsToDesactivate)
        {
            _currentObject.SetActive(true);
        }
        GameManager.instance.player.GetPlayerController._isOnCinematic = false;
    }
    public void ActivateFirstCinematic()
    {
        _firstCinematic.StartCinematic();
    }
    public PlayableDirector GetDirectorFirstCinematic { get => directorFirstCinematic; }
}
