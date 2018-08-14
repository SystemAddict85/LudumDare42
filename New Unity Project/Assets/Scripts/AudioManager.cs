
using UnityEngine;
using System.Collections;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource musicSource;
    private AudioSource[] sfxSources;

    public SoundDictionary GlobalSounds;
    

    public override void Awake()
    {
        base.Awake();
        var sources = GetComponentsInChildren<AudioSource>();

        musicSource = sources[0];
        sfxSources = new AudioSource[sources.Length - 1];
        for(int i = 1; i < sources.Length; ++i)
        {
            sfxSources[i - 1] = sources[i];
        }

        GlobalSounds.InitializeDictionary();

    }

    public static void ChangeMusic(AudioClip audio)
    {
        Instance.musicSource.clip = audio;
        Instance.musicSource.Play();
    }

    public static void PlaySound(SoundObject soundEffect)
    {
        var source = Instance.GetAvailableAudioSource();
        soundEffect.PlayAudio(source);
    }

    public AudioSource GetAvailableAudioSource()
    {
        foreach(var s in sfxSources)
        {
            if (!s.isPlaying)
            {
                return s;
            }            
        }
        sfxSources[0].Stop();
        return sfxSources[0];
    }

    
}