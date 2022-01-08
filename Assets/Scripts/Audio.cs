using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Audio
{
    public string name;
    public string audioType;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;
    public float pitch = 1;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
