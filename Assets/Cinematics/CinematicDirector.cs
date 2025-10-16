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
    public Transform[] _spawners;
    private SecondCinematic _secondCinematic;
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
        _secondCinematic = new SecondCinematic(_spawners);
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
    #region SecondCinematic
    public void SpawnEnemies()
    {
        _secondCinematic.SpawnerEnemies();
    }
    public void ActivateCorutineSpawn()
    {
        _secondCinematic.ActivateCorutineSpawn();
    }
    #endregion
    public PlayableDirector GetPlayableDirector(int index)
    {
        if (index < 0 || index >= _directorsCinematic.Count)
        {
            return null;
        }
        return _directorsCinematic[index];
    }
}
