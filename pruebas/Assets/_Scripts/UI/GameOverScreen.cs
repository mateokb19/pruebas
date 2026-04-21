using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Reintentar()
    {
        PlayerStats.health = PlayerStats.maxHealth;
        PlayerStats.power = 0f;
        SceneManager.LoadScene("LevelOne");
    }
}
