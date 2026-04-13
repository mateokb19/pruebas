using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Slider healthSlider;
    public Slider powerSlider;

    void Start()
    {
    }

    void Update()
    {
        healthSlider.value = PlayerStats.health / PlayerStats.maxHealth;
        powerSlider.value = PlayerStats.power / PlayerStats.maxPower;
    }
}