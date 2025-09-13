using System.Collections;
using UnityEngine;
public class WallAnimation
{
    public Material _wallMaterial;
    private int _layer = 10;
    private float _maxValue = 1f;
    private float _minValue = 0f;
    private Coroutine _wallCoroutine;
    private MonoBehaviour _wallCoroutineStarter;
    public WallAnimation(MonoBehaviour _wallCoroutineStarter)
    {
        this._wallCoroutineStarter = _wallCoroutineStarter;
    }
    public void Onstart()
    {
        var allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (var obj in allObjects)
        {
            if (obj.layer == _layer)
            {
                var renderer = obj.GetComponent<Renderer>().sharedMaterial;
                _wallMaterial = renderer;
                break;
            }
        }
    }
    public void OnUpdate()
    {
        if (_wallMaterial != null && _wallCoroutine == null)
            _wallCoroutine = _wallCoroutineStarter.StartCoroutine(AnimWalls());
    }
    private IEnumerator AnimWalls()
    {
        float _currentTime = 0f;
        float _duration = 30f;
        while (_duration >= _currentTime)
        {
            _currentTime += Time.deltaTime;
            var Lerpvalue = Mathf.Lerp(_minValue, _maxValue, _currentTime / _duration);
            _wallMaterial.SetFloat("_LerpSpot", Lerpvalue);
            yield return null;
        }
        _wallMaterial.SetFloat("_LerpSpot", _maxValue);
        _currentTime = 0f;
        while (_duration >= _currentTime)
        {
            _currentTime += Time.deltaTime;
            var Lerpvalue = Mathf.Lerp(_maxValue, _minValue, _currentTime / _duration);
            _wallMaterial.SetFloat("_LerpSpot", Lerpvalue);
            yield return null;
        }
        _wallMaterial.SetFloat("_LerpSpot", _minValue);
        _currentTime = 0f;
        yield return null;
        _wallCoroutine = null;
    }
}
