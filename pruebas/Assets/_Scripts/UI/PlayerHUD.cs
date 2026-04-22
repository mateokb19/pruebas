using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Scrollbar healthScrollbar;
    public Scrollbar powerScrollbar;

    [SerializeField] private float barLerpSpeed = 4f;

    private float _displayedHealth;

    void Start()
    {
        PlayerStats.LoadProgress();

        _displayedHealth = PlayerStats.health;

        UpdateHUD();
    }

    void Update()
    {
        _displayedHealth = Mathf.Lerp(_displayedHealth, PlayerStats.health, barLerpSpeed * Time.deltaTime);

        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if (healthScrollbar != null)
        {
            healthScrollbar.size = _displayedHealth / PlayerStats.maxHealth;
            healthScrollbar.value = 0f;
        }

        if (powerScrollbar != null)
        {
            float ratio = PlayerStats.power / PlayerStats.maxPower;
            powerScrollbar.size = ratio;
            powerScrollbar.value = 0f;
        }
    }
}
