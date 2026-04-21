using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void IrAlMenu()
    {
        PlayerStats.health = PlayerStats.maxHealth;
        PlayerStats.power = 0f;
        SceneManager.LoadScene("Menu");
    }
}
