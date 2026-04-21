using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;

    const string MusicSaveKey = "MusicVolume";

    [SerializeField] private AudioClip musicClip;
    private AudioSource musicAudio;
    private float musicVolume = 1f;

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
            return;
        }

        musicAudio = GetComponent<AudioSource>();
        if (musicAudio == null)
            musicAudio = gameObject.AddComponent<AudioSource>();

        musicAudio.loop = true;
        musicAudio.clip = musicClip;
    }

    void Start()
    {
        musicVolume = PlayerPrefs.GetFloat(MusicSaveKey, 1f);
        musicAudio.volume = musicVolume;

        if (musicClip != null && !musicAudio.isPlaying)
            musicAudio.Play();
    }

    public float GetMusicVolume() => musicVolume;

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicAudio.volume = musicVolume;
        PlayerPrefs.SetFloat(MusicSaveKey, musicVolume);
        PlayerPrefs.Save();
    }
}
