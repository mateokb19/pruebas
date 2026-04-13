using UnityEngine;

public class DiamondPickup : MonoBehaviour
{
    public float powerAmount = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats.power = Mathf.Min(PlayerStats.power + powerAmount,
PlayerStats.maxPower);
            Destroy(gameObject);
        }
    }
}