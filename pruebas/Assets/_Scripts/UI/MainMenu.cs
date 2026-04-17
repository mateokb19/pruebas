using System.Collections;
using UnityEngine;
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

    void Start()
    {
        loadingPanel.SetActive(false);
        continueButton.interactable = PlayerPrefs.GetInt("HasSave", 0) == 1;
    }

    public void NewGame()
    {
        StartCoroutine(LoadWithWelcome());
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadWithWelcome());
    }

    IEnumerator LoadWithWelcome()
    {
        loadingPanel.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelOne");
        asyncLoad.allowSceneActivation = false;

        string fullText = "Hace no mucho tiempo, la Reina fue expulsada de su castillo.\nLos siete enanitos, en busca de protegerse, destruyeron el Espejo Mágico y esparcieron sus fragmentos.\nSin su poder, la Reina cayó en una profunda desgracia, pero juró recuperar su trono.\nAhora, debe recuperar los fragmentos para reclamar lo que es suyo";

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

    public void OpenSettings()
    {
        Debug.Log("Configuracion - pendiente de implementar");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
