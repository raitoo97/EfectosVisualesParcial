using System.Collections;
using UnityEngine;
public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]private bool canSpawn = false;
    public bool inistSpawn = false;
    private void Update()
    {
        if (!inistSpawn) return;
        StartCoroutine(Spawner());
        canSpawn = true;
        inistSpawn = false;
    }
    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(3f);
        while (canSpawn)
        {
            var enemy = PoolEnemy.instance.GetEnemy();
            enemy.transform.position = transform.position;
            yield return new WaitForSeconds(7.5f);
        }
    }
    public void StopSpawn()
    {
        canSpawn = false;
    }
}
