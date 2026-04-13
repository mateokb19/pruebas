using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        PlayerStats.health = PlayerStats.maxHealth;
        PlayerStats.power = 0f;
        SceneManager.LoadScene("LevelOne");
    }

    public void OpenSettings()
    {
        Debug.Log("Configuracion - pendiente de implementar");
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
