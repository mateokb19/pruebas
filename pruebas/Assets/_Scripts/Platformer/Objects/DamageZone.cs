using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float damage = 25f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManaUI.TakeDamage(damage);
            Debug.Log($"[DamageZone] Jugador tocó zona de daño. Daño: {damage} | Vida restante: {PlayerStats.health}");
        }
    }
}
