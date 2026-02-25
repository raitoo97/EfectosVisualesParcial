using UnityEngine;
public class SubEmitterSound : MonoBehaviour
{
    private AudioSource _audio;
    public Vector2 pitchRange = new Vector2(0.9f, 1.1f);
    public Vector2 volumeRange = new Vector2(0.4f, 0.6f);
    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    void OnParticleCollision(GameObject other)
    {
        _audio.pitch = Random.Range(pitchRange.x, pitchRange.y);
        _audio.volume = Random.Range(volumeRange.x, volumeRange.y);
        _audio.PlayOneShot(_audio.clip, 1.1f);
    }
}
