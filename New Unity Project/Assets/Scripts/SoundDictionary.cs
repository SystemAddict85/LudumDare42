using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New SoundDictionary", menuName = "ScriptableObjects/Sound Dictionary", order = 7)]
public class SoundDictionary : ScriptableObject
{
    public List<SoundObject> soundObjects = new List<SoundObject>();

    private Dictionary<string, SoundObject> sounds;

    public void InitializeDictionary()
    {
        sounds = new Dictionary<string, SoundObject>();
        foreach (var s in soundObjects)
        {
            sounds.Add(s.soundName, s);
        }
    }

    public void PlaySound(string soundName)
    {
        if (sounds.ContainsKey(soundName))
        {
            sounds[soundName].PlayAudio(AudioManager.Instance.GetAvailableAudioSource());
        }
        else
        {
            Debug.LogError("No sounds with name: " + soundName);
        }
    }

}
