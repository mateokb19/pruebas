using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private NewInput _newInput;
    private CatMeowSound _catMeowSound;
    private CatTransform _catTransform;
    private Rigidbody2D _rb;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _newInput = GetComponent<NewInput>();
        _catMeowSound = GetComponent<CatMeowSound>();
        _catTransform = GetComponent<CatTransform>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        JumpAnim();
        MovementAnim();
    }

    void JumpAnim()
    {
        bool isAirborne = Mathf.Abs(_rb.velocity.y) > 0.1f;
        _animator.SetBool("IsJumping", isAirborne);
    }

    public void MovementAnim()
    {
        bool isCat = _catTransform != null && _catTransform.IsCat;

        if (_newInput.inputX > 0 || _newInput.inputX < 0)
        {
            _animator.SetBool("Bool", true);
            if (isCat) _catMeowSound?.OnCatMovement();
        }
        else if (_newInput.inputX == 0)
        {
            _animator.SetBool("Bool", false);
            if (isCat) _catMeowSound?.OnCatIdle();
        }
    }
}
