using System.Collections.Generic;
using UnityEngine;
public class PoolBullets : MonoBehaviour
{
    public static PoolBullets instance;
    [SerializeField]private GameObject _bulletPrefab;
    [SerializeField]private int _initialPoolSize = 10;
    private List<GameObject> _bullets;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        _bullets = new List<GameObject>();
    }
    private void Start()
    {
        CompleteList(_initialPoolSize);
    }
    private void CompleteList(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject go = Instantiate(_bulletPrefab, transform);
            go.SetActive(false);
            _bullets.Add(go);
        }
    }
    public GameObject GetBullet()
    {
        foreach (var bullet in _bullets)
        {
            if (!bullet.activeSelf)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }
        CompleteList(5);
        var aux = _bullets[_bullets.Count - 1];
        aux.SetActive(true);
        return aux;
    }
}
