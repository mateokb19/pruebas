using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;

    [SerializeField] private AudioClip musicClip;
    private AudioSource musicAudio;

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
        // Se registra en el AudioManager para que el volumen se controle desde el slider
        AudioManager.instance.Register(musicAudio);
        musicAudio.volume = AudioManager.instance.masterVolume;

        if (!musicAudio.isPlaying)
            musicAudio.Play();
    }
}
