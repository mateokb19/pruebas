using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPoint : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Vector2 respawnPosition = new Vector2(-24.834f, -7.586f);

    [Header("Damage Settings")]
    public float damageAmount = 10f;

    private void Start()
    {
        Debug.Log("RespawnPoint script cargado en: " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colisión detectada con: " + collision.name + " | Tag: " + collision.tag);

        // Verifica si el objeto que toca es el player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("¡Es el Player!");

            // Resta vida
            PlayerStats.health -= damageAmount;
            if (PlayerStats.health < 0)
                PlayerStats.health = 0;
            Debug.Log("¡Daño restado! Nuevo health: " + PlayerStats.health);

            // Si la vida llega a 0, game over
            if (PlayerStats.health <= 0f)
            {
                Debug.Log("Jugador sin vida. Cargando Perdiste...");
                SceneManager.LoadScene("Perdiste");
                return;
            }

            // Teleporta al player a la posición inicial
            collision.transform.position = respawnPosition;
            Debug.Log("¡Player respawneado en posición inicial!");
        }
    }
}
