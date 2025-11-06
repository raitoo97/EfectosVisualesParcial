using System.Collections;
using UnityEngine;
public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]private bool canSpawn = false;
    public bool inistSpawn = false;
    private void OnEnable()
    {
        StartCoroutine(FirstRespawn());
    }
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
            yield return new WaitForSeconds(3f);
        }
    }
    private IEnumerator FirstRespawn()
    {
        yield return new WaitForSeconds(1f);
        var enemy = PoolEnemy.instance.GetEnemy();
        enemy.transform.position = transform.position;
        enemy.transform.rotation = transform.rotation;
    }
}
