using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    private void Awake()
    {
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;

            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;

        }
    }

    public void Play(string name)
    {
        if (GameManager.isSoundsActive)
        {
            Sounds s = Array.Find(sounds, sound => sound.Name == name);
            s.source.Play();
        }
        
    }
}
