using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [Header("Input Asset")]
    public InputActionAsset inputActions;

    [Header("Volume")]
    public Slider volumeSlider;

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

    const string BindSaveKey = "InputBindings";

    void OnEnable()
    {
        LoadBindings();
        RefreshLabels();

        if (volumeSlider != null && AudioManager.instance != null)
            volumeSlider.value = AudioManager.instance.GetMasterVolume();
    }

    void OnDisable() => currentRebind?.Cancel();

    // ── Volume ───────────────────────────────────────────────────────────────

    public void OnVolumeChanged(float value)
    {
        if (AudioManager.instance != null)
            AudioManager.instance.SetMasterVolume(value);
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
        return InputControlPath.ToHumanReadableString(
            action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    void SaveBindings() =>
        PlayerPrefs.SetString(BindSaveKey, inputActions.SaveBindingOverridesAsJson());

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
