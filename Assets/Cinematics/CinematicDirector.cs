using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CinematicDirector : MonoBehaviour
{
    [SerializeField]private List <GameObject> objectsToDesactivate;
    [SerializeField]private List<PlayableDirector> _directorsCinematic = new List<PlayableDirector>();
    [Header("FirstCinematic")]
    [SerializeField]private GameObject _waterDrop;
    private FirstCinematic _firstCinematic;
    [Header("SecondCinematic")]
    public GameObject _secondCinematicPoint;
    public GameObject _secondCinematicPoint2;
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
    #region FirstCinematic
    public void ActivateFirstCinematic()
    {
        _firstCinematic.StartCinematic();
    }
    #endregion
    public void ToggleMessage(bool isActivate)
    {
        _secondCinematicPoint.SetActive(isActivate);
    }
    public void ToggleSecondMessage(bool isActivate)
    {
        _secondCinematicPoint2.SetActive(isActivate);
    }
    public PlayableDirector GetPlayableDirector(int index)
    {
        if (index < 0 || index >= _directorsCinematic.Count)
        {
            return null;
        }
        return _directorsCinematic[index];
    }
}
