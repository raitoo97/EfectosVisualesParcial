using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private List<AudioSource> sources;
    [SerializeField] private List<AudioClip> clips;
    [SerializeField] private int Pool_Size;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        CompletePool(Pool_Size);
    }
    private void CompletePool(int num)
    {
        for (int i = 0; i < Pool_Size; i++)
        {
            AddSoundSource();
        }
    }
    private void AddSoundSource()
    {
        var AudioSource = this.gameObject.AddComponent<AudioSource>();
        sources.Add(AudioSource);
    }
    private AudioSource GetAudioSourceFromList()
    {
        return sources.Find(x => x.isPlaying == false);
    }
    public AudioSource PlayClip(AudioClip clip, float volumen, bool loop)
    {
        if (GameManager.instance.player.GetPlayerController._isOnCinematic)
            return null;
        var audioSource = GetAudioSourceFromList();
        if (audioSource == null) return null;
        audioSource.volume = volumen;
        audioSource.loop = loop;
        audioSource.clip = clip;
        audioSource.Play();
        return audioSource;
    }
    public AudioSource PlayClipMenu(AudioClip clip, float volumen, bool loop)
    {
        var audioSource = GetAudioSourceFromList();
        if (audioSource == null) return null;
        audioSource.volume = volumen;
        audioSource.loop = loop;
        audioSource.clip = clip;
        audioSource.Play();
        return audioSource;
    }
    public AudioSource PlayCinematicClip(AudioClip clip, float volume, bool loop)
    {
        var audioSource = GetAudioSourceFromList();
        if (audioSource == null) return null;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.clip = clip;
        audioSource.Play();
        return audioSource;
    }
    public AudioClip GetAudioClip(string clip)
    {
        return clips.Find(x => x.name == clip);
    }
}
