using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelExit : MonoBehaviour
{
    [Header("Scene to Load")]
    public string nextScene = "LevelTwo";
    public bool isLastLevel = false;

    [Header("UI Feedback")]
    public TextMeshProUGUI messageText;
    public float messageDuration = 2f;

    private float messageTimer = 0f;

    void Update()
    {
        if (messageTimer > 0f)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0f && messageText != null)
                messageText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        DiamondPickup[] remaining = FindObjectsOfType<DiamondPickup>();

        if (remaining.Length == 0)
        {
            if (isLastLevel)
                SceneManager.LoadScene("Ganaste");
            else
                SceneManager.LoadScene(nextScene);
        }
        else
        {
            ShowMessage("¡Recoge todos los cristales! Faltan: " + remaining.Length);
        }
    }

    void ShowMessage(string text)
    {
        if (messageText == null) return;
        messageText.text = text;
        messageText.gameObject.SetActive(true);
        messageTimer = messageDuration;
    }
}
