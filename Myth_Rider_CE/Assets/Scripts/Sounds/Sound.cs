using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(1.0f, 100.0f)]
    public float volume = 10;
    [Range(1.0f, 10.0f)]
    public float pitch = 1;
    public AudioSource src;
}
