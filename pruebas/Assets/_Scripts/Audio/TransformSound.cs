using UnityEngine;

public class TransformSound : MonoBehaviour
{
    [SerializeField] private AudioClip transformClip;
    private AudioSource transformAudio;

    void Start()
    {
        transformAudio = gameObject.AddComponent<AudioSource>();
        transformAudio.clip = transformClip;
        transformAudio.playOnAwake = false;

        AudioManager.instance.Register(transformAudio);
    }

    public void PlayTransformSound()
    {
        AudioManager.instance.PlaySound(transformAudio);
    }
}
