using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [Header("Input Asset")]
    public InputActionAsset inputActions;

    [Header("Volume")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Key Binding Buttons")]
    public Button bindMoveLeftBtn;
    public Button bindMoveRightBtn;
    public Button bindJumpBtn;
    public Button bindTransformBtn;

    [Header("Key Binding Labels")]
    public TextMeshProUGUI moveLeftLabel;
    public TextMeshProUGUI moveRightLabel;
    public TextMeshProUGUI jumpLabel;
    public TextMeshProUGUI transformLabel;

    private InputActionRebindingExtensions.RebindingOperation currentRebind;

    // InGameOptions lo consulta para no cerrar el panel mientras se reasigna una tecla
    public static bool isRebinding = false;

    const string BindSaveKey = "InputBindings";

    void OnEnable()
    {
        LoadBindings();
        RefreshLabels();

        if (musicSlider != null && BackgroundMusic.instance != null)
            musicSlider.value = BackgroundMusic.instance.GetMusicVolume();

        if (sfxSlider != null && AudioManager.instance != null)
            sfxSlider.value = AudioManager.instance.GetSFXVolume();
    }

    void OnDisable() => currentRebind?.Cancel();

    // ── Volume ───────────────────────────────────────────────────────────────

    public void OnMusicVolumeChanged(float value)
    {
        if (BackgroundMusic.instance != null)
            BackgroundMusic.instance.SetMusicVolume(value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        if (AudioManager.instance != null)
            AudioManager.instance.SetSFXVolume(value);
    }

    // ── Rebind entrypoints ───────────────────────────────────────────────────

    public void RebindMoveLeft()  => StartRebind("Move", 1, moveLeftLabel);
    public void RebindMoveRight() => StartRebind("Move", 2, moveRightLabel);
    public void RebindJump()      => StartRebind("Jump", 0, jumpLabel);
    public void RebindTransform() => StartRebind("CatTransform", 0, transformLabel);

    // ── Core rebind logic ────────────────────────────────────────────────────

    void StartRebind(string actionName, int bindingIndex, TextMeshProUGUI label)
    {
        if (inputActions == null) return;

        currentRebind?.Cancel();

        var action = inputActions.FindAction(actionName);
        if (action == null) return;

        isRebinding = true;
        SetButtonsInteractable(false);
        label.text = "Presiona una tecla...";

        action.Disable();

        currentRebind = action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("<Mouse>")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(_ => FinishRebind(action))
            .OnCancel(_  => FinishRebind(action))
            .Start();
    }

    void FinishRebind(InputAction action)
    {
        isRebinding = false;
        action.Enable();
        currentRebind.Dispose();
        currentRebind = null;
        SaveBindings();
        RefreshLabels();
        SetButtonsInteractable(true);
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    void RefreshLabels()
    {
        if (inputActions == null) return;
        moveLeftLabel?.SetText(GetBindingDisplay("Move", 1));
        moveRightLabel?.SetText(GetBindingDisplay("Move", 2));
        jumpLabel?.SetText(GetBindingDisplay("Jump", 0));
        transformLabel?.SetText(GetBindingDisplay("CatTransform", 0));
    }

    string GetBindingDisplay(string actionName, int bindingIndex)
    {
        var action = inputActions?.FindAction(actionName);
        if (action == null || bindingIndex >= action.bindings.Count) return "?";
        var path = action.bindings[bindingIndex].effectivePath;
        if (string.IsNullOrEmpty(path)) return "?";
        return InputControlPath.ToHumanReadableString(
            path,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    void SaveBindings()
    {
        PlayerPrefs.SetString(BindSaveKey, inputActions.SaveBindingOverridesAsJson());
        PlayerPrefs.Save();
    }

    void LoadBindings()
    {
        if (inputActions == null) return;
        string json = PlayerPrefs.GetString(BindSaveKey, string.Empty);
        if (!string.IsNullOrEmpty(json))
            inputActions.LoadBindingOverridesFromJson(json);
    }

    void SetButtonsInteractable(bool value)
    {
        if (bindMoveLeftBtn)  bindMoveLeftBtn.interactable  = value;
        if (bindMoveRightBtn) bindMoveRightBtn.interactable = value;
        if (bindJumpBtn)      bindJumpBtn.interactable      = value;
        if (bindTransformBtn) bindTransformBtn.interactable = value;
    }

    public void ResetToDefaults()
    {
        if (inputActions == null) return;
        inputActions.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey(BindSaveKey);
        RefreshLabels();
    }
}
