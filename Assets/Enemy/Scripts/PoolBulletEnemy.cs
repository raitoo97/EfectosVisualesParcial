using System.Collections.Generic;
using UnityEngine;
public class PoolBulletEnemy : MonoBehaviour
{
    public static PoolBulletEnemy instance;
    public GameObject bullet;
    [SerializeField] private int initialCount;
    private List<GameObject> poolBulletEnemy = new List<GameObject>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        CompleteList(50);
    }
    private void CompleteList(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var go = Instantiate(bullet,this.transform);
            poolBulletEnemy.Add(go.gameObject);
            go?.SetActive(false);
        }
    }
    public GameObject GetBullet()
    {
        foreach (var bullet in poolBulletEnemy)
        {
            if (!bullet.activeSelf)
            {
                return bullet;
            }
        }
        CompleteList(1);
        var aux = poolBulletEnemy[poolBulletEnemy.Count - 1];
        return aux;
    }
}
