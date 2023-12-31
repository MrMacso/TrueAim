using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;

    public AudioClip clip;

    [Range(0, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public AudioSource source;
}
