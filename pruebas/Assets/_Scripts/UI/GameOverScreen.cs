using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Reintentar()
    {
        PlayerStats.ResetProgress();
        SceneManager.LoadScene("LevelOne");
    }
}
