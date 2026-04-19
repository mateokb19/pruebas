using System.Collections;
using UnityEngine;

public class CatMeowSound : MonoBehaviour
{
    [SerializeField] private AudioClip meowClip;
    private AudioSource meowAudio;
    private float idleTimer;
    private float idleThreshold = 3f;
    private bool hasMeowed;

    void Start()
    {
        meowAudio = gameObject.AddComponent<AudioSource>();
        meowAudio.clip = meowClip;
        meowAudio.playOnAwake = false;

        AudioManager.instance.Register(meowAudio);
    }

    public void OnCatIdle()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleThreshold && !hasMeowed)
        {
            AudioManager.instance.PlaySound(meowAudio);
            hasMeowed = true;
        }
    }

    public void OnCatMovement()
    {
        idleTimer = 0;
        hasMeowed = false;
    }
}
