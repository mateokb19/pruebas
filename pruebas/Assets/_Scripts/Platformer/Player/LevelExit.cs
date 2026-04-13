using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [Header("Scene to Load")]
    public string nextScene = "LevelTwo";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadScene(nextScene);
    }
}
