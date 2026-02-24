using UnityEngine;
public class SubEmitterSound : MonoBehaviour
{
    private AudioSource _audio;
    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Playing sound");
        _audio.PlayOneShot(_audio.clip);
    }
}
