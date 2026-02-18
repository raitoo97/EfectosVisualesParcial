using System.Collections;
using UnityEngine;
public class AcidPlatform : MonoBehaviour
{
    private Vector3 _targetSize;
    private Vector3 _initSize;
    private Coroutine _sizeCoroutine;
    [SerializeField] private float radius = 4f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float tickTime = 1f;
    [SerializeField] private LayerMask enemyMask;
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
        StartCoroutine(AreaDamage());
    }
    private IEnumerator AreaDamage()
    {
        while (true)
        {
            var hits = Physics.OverlapSphere(transform.position, radius, enemyMask);
            foreach (var hit in hits)
            {
                var enemy = hit.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    enemy.ReceiveAreaDamage(damage, transform.position);
                }
            }
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
    private void DestroObject()
    {

    }
}