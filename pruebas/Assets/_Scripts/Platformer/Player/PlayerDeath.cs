using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private bool isDead = false;

    void Update()
    {
        if (!isDead && PlayerStats.health <= 0f)
        {
            isDead = true;
            Debug.Log("Jugador muerto. Cargando escena Perdiste...");
            SceneManager.LoadScene("Perdiste");
        }
    }
}
