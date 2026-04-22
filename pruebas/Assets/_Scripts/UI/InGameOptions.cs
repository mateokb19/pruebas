using UnityEngine;
using UnityEngine.InputSystem;

public class InGameOptions : MonoBehaviour
{
    [Header("Panel")]
    public GameObject optionsPanel;

    [Header("Configuración")]
    [Tooltip("Pausa el juego (Time.timeScale = 0) cuando el panel está abierto. Desactívalo en Ganaste/Perdiste.")]
    public bool pauseOnOpen = true;

    private bool _isOpen;

    void Start()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }

    void Update()
    {
        // Escape abre/cierra el panel, pero no si hay un rebinding activo
        if (!OptionsMenu.isRebinding
            && Keyboard.current != null
            && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Toggle();
        }
    }

    public void Open()
    {
        if (_isOpen) return;
        _isOpen = true;
        if (optionsPanel != null) optionsPanel.SetActive(true);
        if (pauseOnOpen) Time.timeScale = 0f;
    }

    public void Close()
    {
        if (!_isOpen) return;
        _isOpen = false;
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (pauseOnOpen) Time.timeScale = 1f;
    }

    public void Toggle()
    {
        if (_isOpen) Close();
        else Open();
    }

    void OnDestroy()
    {
        // Garantiza que el juego no quede pausado si se destruye el objeto
        if (_isOpen && pauseOnOpen)
            Time.timeScale = 1f;
    }
}
