using UnityEngine;

public class PlayerStats
{
    public static int score;
    public static float health = 100f;
    public static float maxHealth = 100f;
    public static float power = 0f;
    public static float maxPower = 10f;

    // Guardar progreso
    public static void SaveProgress()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetFloat("Health", health);
        PlayerPrefs.SetFloat("Power", power);
        PlayerPrefs.Save();
        Debug.Log("Progreso guardado: Vida=" + health + " | Power=" + power);
    }

    // Cargar progreso
    public static void LoadProgress()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
            health = PlayerPrefs.GetFloat("Health");
            power = PlayerPrefs.GetFloat("Power");
            Debug.Log("Progreso cargado: Vida=" + health + " | Power=" + power);
        }
    }

    // Resetear progreso
    public static void ResetProgress()
    {
        score = 0;
        health = maxHealth;
        power = 0f;
        PlayerPrefs.DeleteAll();
        Debug.Log("Progreso reseteado");
    }
}