using System.Collections;
using UnityEngine;
public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]private bool canSpawn = false;
    public bool inistSpawn = false;  
    private void OnEnable()
    {
        var enemy = PoolEnemy.instance.GetEnemy();
        enemy.transform.position = transform.position;
        enemy.transform.rotation = transform.rotation;
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
            print("Spawn Enemy");
            yield return new WaitForSeconds(3f);
        }
    }
}
