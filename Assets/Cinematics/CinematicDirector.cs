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
    [SerializeField]private Animator _bossAnimator;
    [SerializeField]private Animator[] _soldiersAnimator;
    [SerializeField]private Enemy[] _soldiers;
    public GameObject bossDoorRef;
    [Header("SecondCinematic")]
    public Transform[] _spawners;
    private SecondCinematic _secondCinematic;
    public static CinematicDirector instance;
    public List<GameObject> tempEnemies = new List<GameObject>();
    public GameObject doorRef;
    public Node[] nodes;
    [Header("ThirdCinematic")]
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
        _firstCinematic = new FirstCinematic(_waterDrop.transform, this, _bossAnimator, _soldiersAnimator, _soldiers, bossDoorRef);
        _secondCinematic = new SecondCinematic(_spawners, tempEnemies, doorRef, nodes);
        _thirdCinematic = new ThirdCinematic(GameManager.instance.portal, _spawners, doorRef, bossDoorRef);
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
    public void ActivateBoss()
    {
        _firstCinematic.AcitvateBoss();
    }
    public void ActivateSoldiers()
    {
        _firstCinematic.ActivateSoldiers();
    }
    public void ActivateEnemies()
    {
        _firstCinematic.ActivateEnemies();
    }
    public void DesactivateEnemies()
    {
        _firstCinematic.DesactivateEnemies();
    }
    public void CloseBossDoor()
    {
        _firstCinematic.CloseBossDoor();
    }
    #endregion
    #region SecondCinematic
    public void SpawnEnemies()
    {
        _secondCinematic.SpawnerEnemies();
    }
    public void CloseDoor()
    {
        _secondCinematic.CloseDoor();
    }
    public void ActivateCorutineSpawn()
    {
        _secondCinematic.ActivateCorutineSpawn();
    }
    public void DesactivateTempEnemies()
    {
        _secondCinematic.DesactivateEnemiesFromList();
    }
    public void DesactivateNodes()
    {
        _secondCinematic.DesactivateNodes();
    }
    #endregion
    #region ThirdCinematic
    public void ActivatePortal()
    {
        _thirdCinematic.ActivatePortal();
    }
    public void DesactivateCorutineSpawn()
    {
        _thirdCinematic.DesativateCorutineSpawn();
    }
    public void OpenDoor()
    {
        _thirdCinematic.OpenDoor();
    }
    public void OpenBossDoor()
    {
        _thirdCinematic.OpenBossDoor();
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
