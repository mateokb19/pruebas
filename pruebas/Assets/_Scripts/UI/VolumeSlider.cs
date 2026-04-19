using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;

    void Start()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = AudioManager.instance.masterVolume;
        volumeSlider.onValueChanged.AddListener(AudioManager.instance.SetMasterVolume);
    }
}
