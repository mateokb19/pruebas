using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    const string SFXSaveKey = "SFXVolume";

    [Range(0f, 1f)] public float sfxVolume = 1f;

    private List<AudioSource> registeredSources = new List<AudioSource>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            sfxVolume = PlayerPrefs.GetFloat(SFXSaveKey, 1f);
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
            audioSource.volume = sfxVolume;
            audioSource.Play();
        }
    }

    public float GetSFXVolume() => sfxVolume;

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(SFXSaveKey, sfxVolume);
        PlayerPrefs.Save();
        registeredSources.RemoveAll(s => s == null);

        foreach (AudioSource source in registeredSources)
        {
            if (source != null)
                source.volume = sfxVolume;
        }
    }
}
