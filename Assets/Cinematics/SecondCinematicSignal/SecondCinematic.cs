using System.Collections.Generic;
using UnityEngine;
public class SecondCinematic
{
    private Transform[] _spawners;
    private List<Enemy> _enemies = new List<Enemy>();
    public SecondCinematic(Transform[] spawners)
    {
        _spawners = spawners;
    }
    public void SpawnerEnemies()
    {
        foreach (var spawn in _spawners)
        {
            spawn.gameObject.SetActive(true);
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
}
