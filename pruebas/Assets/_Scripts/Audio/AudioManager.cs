using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Range(0f, 1f)] public float masterVolume = 1f;

    private List<AudioSource> registeredSources = new List<AudioSource>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Register(AudioSource source)
    {
        if (!registeredSources.Contains(source))
            registeredSources.Add(source);
    }

    public void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.volume = masterVolume;
            audioSource.Play();
        }
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);

        // Actualiza todos los AudioSources registrados en tiempo real
        foreach (AudioSource source in registeredSources)
        {
            if (source != null)
                source.volume = masterVolume;
        }
    }
}
