using System.Collections;
using UnityEngine;
public class DamageScreen : MonoBehaviour
{
    [SerializeField]private Material _screenDamageMat;
    [SerializeField]private Material _impactFrame;
    private Coroutine _damageCoroutine;
    private Coroutine _impactFrameCoroutine;
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
        _impactFrame.SetFloat("_OnImpactFrame", 0);
    }
    public void ShowDamage()
    {
        SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("Hit"), 1f, false);
        if (_impactFrameCoroutine != null)
            StopCoroutine(_impactFrameCoroutine);
        _impactFrame.SetFloat("_OnImpactFrame", 1f);
        _impactFrameCoroutine = StartCoroutine(ImpactFrameFadeOut());
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
    private IEnumerator ImpactFrameFadeOut()
    {
        float time = 0f;
        float duration = 0.15f;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        _impactFrame.SetFloat("_OnImpactFrame", 0f);
        _impactFrameCoroutine = null;
    }
}
