using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AcidPlatform : MonoBehaviour
{
    private Vector3 _targetSize;
    private Vector3 _initSize;
    private Coroutine _sizeCoroutine;
    private Coroutine _destroyCoroutine;
    private Coroutine _areaDamageCoroutine;
    [SerializeField] private float radius = 4f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float tickTime = 1f;
    [SerializeField] private LayerMask enemyMask;
    private HashSet<Enemy> enemiesInArea = new HashSet<Enemy>();
    private void Awake()
    {
        _initSize = Vector3.zero;
        transform.localScale = _initSize;
        _targetSize = new Vector3(9f, 2f, 8f);
    }
    void Start()
    {
        if(_sizeCoroutine == null)
            _sizeCoroutine = StartCoroutine(SizeAnimation());
        _areaDamageCoroutine = StartCoroutine(AreaDamage());
        Invoke("DestroyObject", 10f);
    }
    private IEnumerator AreaDamage()
    {
        while (true)
        {
            var hits = Physics.OverlapSphere(transform.position, radius, enemyMask);
            HashSet<Enemy> currentEnemies = new HashSet<Enemy>();
            foreach (var hit in hits)
            {
                var enemy = hit.GetComponentInParent<Enemy>();
                if (enemy == null) continue;
                currentEnemies.Add(enemy);
                // ENTRA al área
                if (!enemiesInArea.Contains(enemy))
                {
                    enemy.OnEnterAcid();
                }
                enemy.ReceiveAreaDamage(damage, transform.position);
            }
            foreach (var enemy in enemiesInArea)
            {
                if (!currentEnemies.Contains(enemy))
                {
                    enemy.OnExitAcid();
                }
            }
            enemiesInArea = currentEnemies;
            yield return new WaitForSeconds(tickTime);
        }
    }
    private IEnumerator SizeAnimation()
    {
        float time = 0;
        float duration = 0.5f;
        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(_initSize, _targetSize, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = _targetSize;
        _sizeCoroutine = null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    private void DestroyObject()
    {
        if (_areaDamageCoroutine != null)
        {
            StopCoroutine(_areaDamageCoroutine);
            _areaDamageCoroutine = null;
        }
        if (_destroyCoroutine == null)
            _destroyCoroutine = StartCoroutine(SizeAnimationDestroy());
        foreach (var enemy in enemiesInArea)
        {
            if (enemy != null)
                enemy.OnExitAcid();
        }
        enemiesInArea.Clear();
    }
    private IEnumerator SizeAnimationDestroy()
    {
        float time = 0;
        float duration = 0.5f;
        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(_targetSize, _initSize, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = _initSize;
        Destroy(gameObject);
        _destroyCoroutine = null;
    }
}