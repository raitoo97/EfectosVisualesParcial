using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CinematicDirectorL0 : MonoBehaviour
{
    [SerializeField]private List <GameObject> objectsToDesactivate;
    [SerializeField]private List<PlayableDirector> _directorsCinematic = new List<PlayableDirector>();
    public static CinematicDirectorL0 instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
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
            else if (i == 5 || i == 6)// EL INDICE 5 ES EL DIALOGO
            {
                continue;
            }
            else
            {
                objectsToDesactivate[i].SetActive(true);
            }
        }
        GameManager.instance.player.GetPlayerController._isOnCinematic = false;
    }
}
