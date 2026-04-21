using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Scrollbar healthScrollbar;
    public Scrollbar powerScrollbar;

    [SerializeField] private float regenDelay = 3f;
    [SerializeField] private float regenRate = 10f;
    [SerializeField] private float barLerpSpeed = 4f;

    private float _displayedHealth;

    void Start()
    {
        PlayerStats.LoadProgress();

        _displayedHealth = PlayerStats.health;

        if (powerScrollbar != null)
            powerScrollbar.numberOfSteps = (int)PlayerStats.maxPower;

        UpdateHUD();
    }

    void Update()
    {
        RegenerateHealth();

        _displayedHealth = Mathf.Lerp(_displayedHealth, PlayerStats.health, barLerpSpeed * Time.deltaTime);

        UpdateHUD();
    }

    private void RegenerateHealth()
    {
        if (PlayerStats.health >= PlayerStats.maxHealth) return;
        if (Time.time - PlayerStats.lastDamageTime < regenDelay) return;

        PlayerStats.health = Mathf.Min(
            PlayerStats.health + regenRate * Time.deltaTime,
            PlayerStats.maxHealth
        );

        if (PlayerStats.health >= PlayerStats.maxHealth)
            PlayerStats.SaveProgress();
    }

    private void UpdateHUD()
    {
        if (healthScrollbar != null)
            healthScrollbar.value = _displayedHealth / PlayerStats.maxHealth;

        if (powerScrollbar != null)
            powerScrollbar.value = PlayerStats.power / PlayerStats.maxPower;
    }
}
