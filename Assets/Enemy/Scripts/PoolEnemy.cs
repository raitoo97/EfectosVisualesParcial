using System.Collections.Generic;
using UnityEngine;
public class PoolEnemy : MonoBehaviour
{
    public static PoolEnemy instance;
    private List<GameObject> enemyList = new List<GameObject>();
    public GameObject enemyPrefab;
    public int poolCount;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        CompleteList(poolCount);
    }
    private void CompleteList(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab,this.transform);
            enemy.SetActive(false);
            enemyList.Add(enemy);
        }
    }
    public GameObject GetEnemy()
    {
        foreach (var enemy in enemyList)
        {
            if (!enemy.activeSelf)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }
        CompleteList(1);
        var auxEnemy = enemyList[enemyList.Count - 1];
        auxEnemy.SetActive(true);
        return auxEnemy;
    }
}
