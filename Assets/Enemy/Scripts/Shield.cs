using System.Collections;
using UnityEngine;
public class Shield : MonoBehaviour, IShield ,IImpact
{
    private Material _material;
    private Coroutine _flashCoroutine;
    private float _maxIntensity;
    private float _minIntensity;
    private float _flashDuration;
    private void OnEnable()
    {
        _material = GetComponent<Renderer>().material;
        _maxIntensity = 30f;
        _minIntensity = 1f;
        _flashDuration = 0.1f;
    }
    public void ActivateShield()
    {
        this.gameObject.SetActive(true);
    }
    public void DeactivateShield()
    {
        this.gameObject.SetActive(false);
    }
    public void OnImpact(Vector3 hitPosition)
    {
        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
            _flashCoroutine = null;
        }
        if(_flashCoroutine == null)
            _flashCoroutine = StartCoroutine(FlashDuration());
        if (GameManager.instance.impactParticlesPrefab != null)
        {
            ParticleSystem ps = Instantiate(GameManager.instance.impactParticlesPrefab, hitPosition,Quaternion.identity);
            ps.Play();
            Destroy(ps.gameObject, ps.main.duration);
        }
    }
    public IEnumerator FlashDuration()
    {
        _material.SetFloat("_ColorIntensity", _maxIntensity);
        yield return new WaitForSeconds(_flashDuration);
        _material.SetFloat("_ColorIntensity", _minIntensity);
        _flashCoroutine = null;
    }

}
