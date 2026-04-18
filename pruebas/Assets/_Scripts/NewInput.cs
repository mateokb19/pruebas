
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInput : MonoBehaviour
{
    // Se declaran las variables
    private PlayerInput playerInput;
    private PlayerJump _playerJump;
    private CatTransform _catTransform;
    [HideInInspector] public float inputX;

    // Al inicio de juego
    private void Start()
    {
        // Se almacena en la variable el componente acorde de Unity
        playerInput = GetComponent<PlayerInput>();
        _playerJump = GetComponent<PlayerJump>();
        _catTransform = GetComponent<CatTransform>();
    }

    // Cada frame
    private void Update()
    {
        // Se llama al m�todo para que funcione
        GetInput();
    }

    public void Button(InputAction.CallbackContext context)
    {
        // Tres momentos: cuando se presiona, cuando se est� presionando y cuando se deja de presionar
        if (context.started)
        {
            // Forma de escribir en consola
            Debug.Log("Shoot");
            _playerJump.Jump();
        }
    }

  
    public void CatTransformAction(InputAction.CallbackContext context)
    {
        if (context.started)
            _catTransform?.ToggleTransform();
    }

    public void GetInput()
    {
        // Almacena en la variable el varlor del axis del archvio de PlayerActions
        inputX = playerInput.actions["Move"].ReadValue<float>();
        Debug.Log("InputX: " + inputX);
    }
}
