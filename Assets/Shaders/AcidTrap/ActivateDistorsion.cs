using System.Collections;
using UnityEngine;
public class ActivateDistorsion : MonoBehaviour
{
    public Material distorsionMaterial;
    private Coroutine _lerpCoroutine;
    private AudioSource _underWaterSource;
    private void Start()
    {
        distorsionMaterial.SetFloat("_LerpActivate", 0);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EyesPlayer"))
            StartLerp(0f);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().GetPlayerController.SetUnderAcid(false);
            SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("ExitWater"), 1f, false);
            if (_underWaterSource != null)
            {
                _underWaterSource.Stop();
                _underWaterSource = null;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EyesPlayer"))
            StartLerp(1f);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().GetPlayerController.SetUnderAcid(true);
            SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("IntoToTheWater"), 1f, false);
            if (_underWaterSource == null)
            {
                _underWaterSource = SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("UnderWater"),0.5f,true);
            }
        }
    }
    private void StartLerp(float targetValue)
    {
        if (_lerpCoroutine != null)
            StopCoroutine(_lerpCoroutine);
        _lerpCoroutine = StartCoroutine(LerpDistorsion(targetValue));
    }
    IEnumerator LerpDistorsion(float target)
    {
        float duration = .5f;
        float time = 0f;
        float startValue = distorsionMaterial.GetFloat("_LerpActivate");
        while (time < duration)
        {
            time += Time.deltaTime;
            float value = Mathf.Lerp(startValue, target, time / duration);
            distorsionMaterial.SetFloat("_LerpActivate", value);
            yield return null;
        }
        distorsionMaterial.SetFloat("_LerpActivate", target);
        _lerpCoroutine = null;
    }
}