using UnityEngine;

public class DiamondPickup : MonoBehaviour
{
    public float powerAmount = 20f;
    public float healthAmount = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Añade poder
            PlayerStats.power = Mathf.Min(PlayerStats.power + powerAmount, PlayerStats.maxPower);

            // Añade vida
            PlayerStats.health = Mathf.Min(PlayerStats.health + healthAmount, PlayerStats.maxHealth);

            Debug.Log("¡Cristal recogido! Power: +" + powerAmount + " | Health: +" + healthAmount);
            Destroy(gameObject);
        }
    }
}