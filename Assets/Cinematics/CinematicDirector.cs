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
    public List<GameObject> tempEnemies = new List<GameObject>();
    [SerializeField]private GameObject[] _stairBroke;
    [SerializeField]private GameObject[] _stairNew;
    [Header("ThirdCinematic")]
    [SerializeField] private Transform _starshipPos;
    [SerializeField] private Transform _starshipArrivePos;
    private ThirdCinematic _thirdCinematic;
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
        _secondCinematic = new SecondCinematic(_spawners, tempEnemies, _stairBroke, _stairNew);
        _thirdCinematic = new ThirdCinematic(_starshipPos, _starshipArrivePos,this);
    }
    public void DesactivateGunAndPlayer()
    {
        for (int i = 0; i < objectsToDesactivate.Count; i++)
        {
            if (i == 2)// EL INDICE 2 ES EL PJ
            {
                foreach (MeshRenderer mr in objectsToDesactivate[i].GetComponentsInChildren<MeshRenderer>())
                {
                    mr.enabled = false;
                }
            }
            else
            {
                objectsToDesactivate[i].SetActive(false);
            }
        }
        GameManager.instance.player.GetPlayerController._isOnCinematic = true;
    }
    public void ActivateGunAndPlayer()
    {
        for (int i = 0; i < objectsToDesactivate.Count; i++)
        {
            if (i == 2)// EL INDICE 2 ES EL PJ
            {
                foreach (MeshRenderer mr in objectsToDesactivate[i].GetComponentsInChildren<MeshRenderer>())
                {
                    mr.enabled = true;
                }
            }
            else
            {
                objectsToDesactivate[i].SetActive(true);
            }
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
    public void DesactivateTempEnemies()
    {
        _secondCinematic.DesactivateEnemiesFromList();
    }
    public void ActivateBrokenStair()
    {
        _secondCinematic.ActivateBrokenStair();
    }
    public void ActivateCleanStair()
    {

        _secondCinematic.ActivateCleanStair();
    }
    #endregion
    #region ThirdCinematic
    public void ActivateThirdCinematic()
    {
        CanvasManager.instance.HiddenTime();
        _thirdCinematic.StartCinematic();
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
