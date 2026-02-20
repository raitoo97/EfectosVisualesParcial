using System.Collections;
using UnityEngine;
public class ActivateDistorsion : MonoBehaviour
{
    public Material distorsionMaterial;
    private Coroutine _lerpCoroutine;
    private void Start()
    {
        distorsionMaterial.SetFloat("_LerpActivate", 0);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EyesPlayer"))
            StartLerp(0f);
        if (other.CompareTag("Player"))
            other.GetComponent<Player>().GetPlayerController.SetUnderAcid(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EyesPlayer"))
            StartLerp(1f);
        if (other.CompareTag("Player"))
            other.GetComponent<Player>().GetPlayerController.SetUnderAcid(true);
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