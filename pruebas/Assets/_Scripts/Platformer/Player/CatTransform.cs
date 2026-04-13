using UnityEngine;

public class CatTransform : MonoBehaviour
{
    [Header("Cat Form Settings")]
    public float catScaleMultiplier = 0.5f;

    [Header("Collider Settings")]
    public Vector2 humanColliderSize = new Vector2(1f, 2f);
    public Vector2 catColliderSize = new Vector2(1f, 1f);
    public Vector2 humanColliderOffset = new Vector2(0f, 0f);
    public Vector2 catColliderOffset = new Vector2(0f, -0.5f);

    private Vector3 _normalScale;
    private bool _isCat = false;
    private Animator _animator;
    private CapsuleCollider2D _collider;

    void Start()
    {
        _normalScale = transform.localScale;
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
    }

    public void ToggleTransform()
    {
        _isCat = !_isCat;

        if (_isCat)
        {
            transform.localScale = _normalScale * catScaleMultiplier;
            if (_collider != null)
            {
                _collider.size = catColliderSize;
                _collider.offset = catColliderOffset;
            }
        }
        else
        {
            transform.localScale = _normalScale;
            if (_collider != null)
            {
                _collider.size = humanColliderSize;
                _collider.offset = humanColliderOffset;
            }
        }

        if (_animator != null)
            _animator.SetBool("IsCat", _isCat);
    }

    public bool IsCat => _isCat;
}