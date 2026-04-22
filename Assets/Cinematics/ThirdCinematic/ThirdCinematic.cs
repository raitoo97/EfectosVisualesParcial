using System.Collections.Generic;
using UnityEngine;
public class ThirdCinematic
{
    private PortalScript _portalRef;
    private Transform[] _spawners;
    private List<Enemy> _enemies = new List<Enemy>();
    private GameObject _doorRef;
    private GameObject _bossDoorRef;
    public ThirdCinematic(PortalScript portal, Transform[] spawners, GameObject doorRef, GameObject bossDoorRef)
    {
        _portalRef = portal;
        _spawners = spawners;
        _doorRef = doorRef;
        _bossDoorRef = bossDoorRef;
    }
    public void ActivatePortal()
    {
        _portalRef.gameObject.SetActive(true);
    }
    public void DesativateCorutineSpawn()
    {
        foreach (var spawn in _spawners)
        {
            var currentspawn = spawn.GetComponent<SpawnEnemy>();
            currentspawn.StopSpawn();
            currentspawn.gameObject.SetActive(false);
        }
        _enemies = new List<Enemy>(GameObject.FindObjectsOfType<Enemy>());
        foreach (var enemy in _enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }
    public void OpenDoor()
    {
        _doorRef.GetComponent<Animator>()?.SetTrigger("OpenDoor");
    }
    public void OpenBossDoor()
    {
       _bossDoorRef.GetComponent<Animator>()?.SetTrigger("OpenDoor");
    }
}
