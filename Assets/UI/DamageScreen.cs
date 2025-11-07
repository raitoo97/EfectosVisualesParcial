using System.Collections;
using UnityEngine;
public class DamageScreen : MonoBehaviour
{
    [SerializeField]private Material _screenDamageMat;
    private Coroutine _damageCoroutine;
    private float cameraShakeDuration = 0.3f;
    public static DamageScreen instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        HideDamage();
    }
    public void HideDamage()
    {
        _screenDamageMat.SetFloat("_VignetteRadius", 0);
    }
    public void ShowDamage()
    {
        if (_damageCoroutine != null)
            StopCoroutine(_damageCoroutine);
        _damageCoroutine = StartCoroutine(DamageCorutine());
        CameraShakeManager.instance.ShakeCamera(ShakesType.PlayerUnderAtack, cameraShakeDuration);
    }
    private IEnumerator DamageCorutine()
    {
        float _currentTime = 0;
        float _duration = cameraShakeDuration;
        float _minValue = 0f;
        float _maxValue = 1f;
        while (_currentTime < _duration)
        {
            _currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(_minValue, _maxValue, _currentTime / _duration);
            _screenDamageMat.SetFloat("_VignetteRadius", alpha);
            yield return null;
        }
        _screenDamageMat.SetFloat("_VignetteRadius", _maxValue);
        _currentTime = 0;
        while (_currentTime < _duration)
        {
            _currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(_maxValue, _minValue, _currentTime / _duration);
            _screenDamageMat.SetFloat("_VignetteRadius", alpha);
            yield return null;
        }
        _screenDamageMat.SetFloat("_VignetteRadius", _minValue);
        _currentTime = 0;
        _damageCoroutine = null;
    }
}
