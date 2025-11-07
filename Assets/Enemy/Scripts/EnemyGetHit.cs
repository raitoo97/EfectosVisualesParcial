using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyGetHit
{
    private List<Material> _hitParts = new List<Material>();
    private MonoBehaviour _corutineHandler;
    private Coroutine _damageHitCoroutine;
    public EnemyGetHit(List<Material> hitParts, MonoBehaviour corutineHandler)
    {
        _hitParts = hitParts;
        _corutineHandler = corutineHandler;
    }
    public void OnEnable()
    {
        foreach (var part in _hitParts)
        {
            part.SetFloat("_EffectIntesnity",0);
        }
    }
    public void ActivteCorutineDamageHit()
    {
        if (_damageHitCoroutine != null)
        {
            _corutineHandler.StopCoroutine(_damageHitCoroutine);
            foreach (var part in _hitParts)
            {
                part.SetFloat("_EffectIntesnity", 0f);
            }
        }
        _damageHitCoroutine = _corutineHandler.StartCoroutine(ShowDamageHit());
    }
    IEnumerator ShowDamageHit()
    {
        float _currentTime = 0f;
        float duration = 0.2f;
        float _maxIntensity = 1f;
        float _minIntensity = 0f;
        foreach (var part in _hitParts)
        {
            part.SetFloat("_EffectIntesnity", _maxIntensity);
        }
        yield return null;
        while (_currentTime < duration)
        {
            _currentTime += Time.deltaTime;
            float intensity = Mathf.Lerp(_maxIntensity, _minIntensity, _currentTime / duration);
            foreach (var part in _hitParts)
            {
                part.SetFloat("_EffectIntesnity", intensity);
            }
            yield return null;
        }
        foreach (var part in _hitParts)
        {
            part.SetFloat("_EffectIntesnity", _minIntensity);
        }
        _damageHitCoroutine = null;
    }
}
