using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Vector2 respawnPosition = new Vector2(-24.834f, -7.586f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que toca es el player
        if (collision.CompareTag("Player"))
        {
            // Teleporta al player a la posición inicial
            collision.transform.position = respawnPosition;
            Debug.Log("¡Player respawneado en posición inicial!");
        }
    }
}
