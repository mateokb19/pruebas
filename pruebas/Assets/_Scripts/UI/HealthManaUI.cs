using UnityEngine;
using UnityEngine.UI;

public class HealthManaUI : MonoBehaviour
{
    [Header("Health Bar")]
    public Image healthBarFill;
    public Text healthText;

    [Header("Power Bar")]
    public Image powerBarFill;
    public Text powerText;

    private void Start()
    {
        // Cargar progreso al inicio
        PlayerStats.LoadProgress();
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Actualizar barra de vida
        if (healthBarFill != null)
            healthBarFill.fillAmount = PlayerStats.health / PlayerStats.maxHealth;

        if (healthText != null)
            healthText.text = Mathf.Round(PlayerStats.health) + " / " + Mathf.Round(PlayerStats.maxHealth);

        // Actualizar barra de poder
        if (powerBarFill != null)
            powerBarFill.fillAmount = PlayerStats.power / PlayerStats.maxPower;

        if (powerText != null)
            powerText.text = Mathf.Round(PlayerStats.power) + " / " + Mathf.Round(PlayerStats.maxPower);
    }

    // Llamar cuando el jugador recibe daño
    public static void TakeDamage(float damage)
    {
        PlayerStats.health -= damage;
        if (PlayerStats.health < 0)
            PlayerStats.health = 0;
        PlayerStats.SaveProgress();
    }

    // Llamar cuando el jugador se cura
    public static void Heal(float amount)
    {
        PlayerStats.health += amount;
        if (PlayerStats.health > PlayerStats.maxHealth)
            PlayerStats.health = PlayerStats.maxHealth;
        PlayerStats.SaveProgress();
    }

    // Llamar cuando el jugador usa poder
    public static void UsePower(float amount)
    {
        PlayerStats.power -= amount;
        if (PlayerStats.power < 0)
            PlayerStats.power = 0;
        PlayerStats.SaveProgress();
    }

    // Llamar cuando el jugador recupera poder
    public static void RestorePower(float amount)
    {
        PlayerStats.power += amount;
        if (PlayerStats.power > PlayerStats.maxPower)
            PlayerStats.power = PlayerStats.maxPower;
        PlayerStats.SaveProgress();
    }
}
