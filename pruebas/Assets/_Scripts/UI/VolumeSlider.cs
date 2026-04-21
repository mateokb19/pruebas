using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        if (slider != null && BackgroundMusic.instance != null)
            slider.value = BackgroundMusic.instance.GetMusicVolume();
    }
}
