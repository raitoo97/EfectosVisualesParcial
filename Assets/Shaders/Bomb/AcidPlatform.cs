using System.Collections;
using UnityEngine;
public class AcidPlatform : MonoBehaviour
{
    private Vector3 _targetSize;
    private Vector3 _initSize;
    private Coroutine _sizeCoroutine;
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
}
