using System.Collections;
using UnityEngine;
public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]private bool canSpawn = false;
    public bool inistSpawn = false;
    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 6f;
    private Coroutine spawnCoroutine;
    [Header("Aggression")]
    [SerializeField] private float aggressionRamp = 0.98f;
    [SerializeField] private int maxExtraSpawns = 3;
    [SerializeField]private int enemiesAliveInWave = 0;
    float pressureTimer = 0f;
    float maxPressureTime = 20f;
    private void Update()
    {
        if (inistSpawn)
        {
            inistSpawn = false;
            canSpawn = true;
            StartCoroutine(StartSpawnSystem());
        }

    }
    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(3f);
        float currentInterval = spawnInterval;
        int currentMaxSpawns = maxExtraSpawns;
        while (canSpawn)
        {
            yield return new WaitUntil(() => enemiesAliveInWave <= 0 || pressureTimer >= maxPressureTime);
            pressureTimer = 0f;
            int spawnCount = Random.Range(1, currentMaxSpawns);
            enemiesAliveInWave = spawnCount;
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnEnemyUnit();
                float stagger = Random.Range(1f, 2f);
                yield return new WaitForSeconds(stagger);
            }
            //  espera entre oleadas (global)
            float randomInterval = Random.Range(currentInterval * 0.5f, currentInterval * 1.2f);
            yield return new WaitForSeconds(randomInterval);
            currentInterval *= aggressionRamp;
            currentInterval = Mathf.Max(currentInterval, 1.5f);
            maxExtraSpawns++;
        }
    }
    private IEnumerator PressureClock()
    {
        while (canSpawn)
        {
            pressureTimer += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator StartSpawnSystem()
    {
        yield return new WaitForSeconds(3f);

        StartCoroutine(PressureClock());
        StartCoroutine(Spawner());
    }
    private void SpawnEnemyUnit()
    {
        var enemy = PoolEnemy.instance.GetEnemy();
        Vector3 spawnPos = GetValidSpawnPosition();
        enemy.transform.position = spawnPos;
        enemy.GetComponent<Enemy>().spawner = this;
    }
    private Vector3 GetValidSpawnPosition()
    {
        Vector3 spawnPos;
        int tries = 0;
        do
        {
            Node node = NodeManager.GetRandomValidNode(GameManager.instance.player.transform.position, 2.5f);
            spawnPos = node.transform.position;
            tries++;
            if (tries > 10)
                break;

        }
        while (GameManager.instance.player != null && Vector3.Distance(spawnPos, GameManager.instance.player.transform.position) < 2.5f);
        return spawnPos;
    }
    public void NotifyEnemyDeath()
    {
        enemiesAliveInWave--;
    }
    public void StopSpawn()
    {
        canSpawn = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
}
