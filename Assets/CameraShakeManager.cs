using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ShakesType
{
    PlayerUnderAtack,
}
public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;
    [SerializeField] private CinemachineVirtualCamera _cameraShake;
    private CinemachineBasicMultiChannelPerlin noise;
    public List<ShakesClass> shakesClasses = new List<ShakesClass>();
    public Dictionary<ShakesType, ShakesClass> shakesDictionary = new Dictionary<ShakesType, ShakesClass>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        noise = _cameraShake.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        foreach (var shake in shakesClasses)
        {
            shakesDictionary[shake.type] = shake;
        }
    }
    public void ShakeCamera(ShakesType type,float duration)
    {
        if (!shakesDictionary.ContainsKey(type)) return;
        var _currentShakeClass = shakesDictionary[type];
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine(_currentShakeClass.setting, _currentShakeClass.amplitude, _currentShakeClass.frequency, _currentShakeClass.pivotOffset,duration));
    }
    private IEnumerator ShakeCoroutine(NoiseSettings setting, float amplitude, float frequency, Vector3 pivotOffset, float duration)
    {
        noise.m_NoiseProfile = setting;
        noise.m_PivotOffset = pivotOffset;
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);
        noise.m_NoiseProfile = null;
        noise.m_PivotOffset = Vector3.zero;
        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
    }
}
[Serializable]
public class ShakesClass
{
    public ShakesType type;
    public NoiseSettings setting;
    public float amplitude,frequency;
    public Vector3 pivotOffset;
    public ShakesClass(ShakesType type,NoiseSettings setting,float amplitude, float frequency, Vector3 pivotOffset)
    {
        this.type = type;
        this.setting = setting;
        this.amplitude = amplitude;
        this.frequency = frequency;
        this.pivotOffset = pivotOffset;
    }
}

