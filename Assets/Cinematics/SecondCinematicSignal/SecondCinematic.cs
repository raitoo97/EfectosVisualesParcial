using System.Collections.Generic;
using UnityEngine;
public class SecondCinematic
{
    private Transform[] _spawners;
    private List<Enemy> _enemies = new List<Enemy>();
    private List<GameObject> _tempEnemies = new List<GameObject>();
    private GameObject _doorRef;
    private Node[] _nodes;
    public SecondCinematic(Transform[] spawners , List<GameObject> tempEnemies, GameObject doorRef, Node[] nodes)
    {
        _spawners = spawners;
        _tempEnemies = tempEnemies;
        _doorRef = doorRef;
        _nodes = nodes;
    }
    public void SpawnerEnemies()
    {
        _enemies = new List<Enemy>(GameObject.FindObjectsOfType<Enemy>());
        foreach (var enemy in _enemies)
        {
            enemy.gameObject.SetActive(false);
        }
        foreach (var spawn in _spawners)
        {
            spawn.gameObject.SetActive(true);
        }
        foreach (var enemy in _tempEnemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }
    public void DesactivateEnemiesFromList()
    {
        foreach (var enemy in _tempEnemies)
        {
            GameObject.Destroy(enemy);
        }
    }
    public void DesactivateNodes()
    {
        foreach (var node in _nodes)
        {
            GameObject.Destroy(node.gameObject);
        }
    }
    public void ActivateCorutineSpawn()
    {
        foreach (var spawn in _spawners)
        {
            var currentspawn = spawn.GetComponent<SpawnEnemy>();
            currentspawn.inistSpawn = true;
        }
        _enemies = new List<Enemy>(GameObject.FindObjectsOfType<Enemy>());
        foreach (var enemy in _enemies)
        {
            enemy.GetFSM.ChangeState(FSM.StateID.Chase);
        }
    }
    public void CloseDoor()
    {
        _doorRef.GetComponent<Animator>()?.SetTrigger("CloseDoor");
    }
}
