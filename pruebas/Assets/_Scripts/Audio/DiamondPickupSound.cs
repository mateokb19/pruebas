using UnityEngine;

public class DiamondPickupSound : MonoBehaviour
{
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] [Range(0f, 1f)] private float volume = 1f;

    public void PlayPickupSound()
    {
        if (pickupClip != null)
        {
            AudioSource.PlayClipAtPoint(pickupClip, transform.position, volume * AudioManager.instance.sfxVolume);
        }
    }
}
