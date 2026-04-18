using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Loading Screen")]
    public GameObject loadingPanel;
    public TextMeshProUGUI loadingText;
    [Tooltip("Segundos entre cada caracter")]
    public float typeSpeed = 0.005f;

    [Header("Buttons")]
    public Button continueButton;

    [Header("Options Panel")]
    public GameObject optionsPanel;

    [Header("Input Asset")]
    public InputActionAsset inputActions;

    const string BindSaveKey  = "InputBindings";
    const string VolumeSaveKey = "MasterVolume";

    void Start()
    {
        loadingPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);

        continueButton.interactable = PlayerPrefs.GetInt("HasSave", 0) == 1;

        // Apply saved volume
        AudioListener.volume = PlayerPrefs.GetFloat(VolumeSaveKey, 1f);

        // Apply saved key bindings so gameplay uses them
        if (inputActions != null)
        {
            string json = PlayerPrefs.GetString(BindSaveKey, string.Empty);
            if (!string.IsNullOrEmpty(json))
                inputActions.LoadBindingOverridesFromJson(json);
        }
    }

    // ── Main menu buttons ────────────────────────────────────────────────────

    public void NewGame()
    {
        PlayerPrefs.SetInt("HasSave", 1);
        StartCoroutine(LoadWithWelcome());
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadWithWelcome());
    }

    public void OpenSettings()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(!optionsPanel.activeSelf);
    }

    public void CloseSettings()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // ── Loading sequence ─────────────────────────────────────────────────────

    IEnumerator LoadWithWelcome()
    {
        loadingPanel.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelOne");
        asyncLoad.allowSceneActivation = false;

        string fullText = "Hace no mucho tiempo, la Reina fue expulsada de su castillo.\n" +
                          "Los siete enanitos, en busca de protegerse, destruyeron el Espejo Mágico y esparcieron sus fragmentos.\n" +
                          "Sin su poder, la Reina cayó en una profunda desgracia, pero juró recuperar su trono.\n" +
                          "Ahora, debe recuperar los fragmentos para reclamar lo que es suyo.";

        yield return StartCoroutine(Typewriter(fullText));
        yield return new WaitForSeconds(4f);

        asyncLoad.allowSceneActivation = true;
    }

    IEnumerator Typewriter(string text)
    {
        loadingText.text = "";
        foreach (char c in text)
        {
            loadingText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
