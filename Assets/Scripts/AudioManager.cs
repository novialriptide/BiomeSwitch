using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Audio[] audioData;

    [HideInInspector]
    public AudioSource[] sources;
    [HideInInspector]
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Audio a in audioData)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.loop = a.loop;
        }

        Play("music_arctic_tundra_biome");
    }

    public Audio PlayAtPosition(string name, float position)
    {
        Audio a = Play(name);
        a.source.time = position;

        return a;
    }

    public Audio Play(string name)
    {
        Audio a = Array.Find(audioData, audio => audio.name == name);
        if (a == null)
            return null;
        a.source.Play();
        return a;
    }

    public void stopAllAudio()
    {
        sources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in sources)
            a.Stop();
    }
}
