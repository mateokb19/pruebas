using UnityEngine;

public class CatTransform : MonoBehaviour
{
    [Header("Cat Form Settings")]
    public float catScaleMultiplier = 0.5f;

    [Header("Collider Settings (Cat)")]
    public Vector2 catColliderSize = new Vector2(0.5f, 0.5f);
    public Vector2 catColliderOffset = new Vector2(0f, 0.3f);

    [Header("Colisiones")]
    [Tooltip("Layer de los enemigos (debe coincidir con el Layer asignado en sus GameObjects)")]
    public string enemyLayerName = "Enemy";

    private Vector3 _normalScale;
    private bool _isCat = false;
    private Animator _animator;
    private BoxCollider2D _collider;
    private TransformSound _transformSound;

    // Valores originales del collider humano
    private Vector2 _originalColliderSize;
    private Vector2 _originalColliderOffset;

    void Start()
    {
        _normalScale = transform.localScale;
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _transformSound = GetComponent<TransformSound>();

        // Guardar los valores originales del collider
        if (_collider != null)
        {
            _originalColliderSize = _collider.size;
            _originalColliderOffset = _collider.offset;
            Debug.Log("Valores originales guardados - Size: " + _originalColliderSize + " | Offset: " + _originalColliderOffset);
        }
    }

    public void ToggleTransform()
    {
        _isCat = !_isCat;
        Debug.Log("Transform activado. IsCat: " + _isCat);
        _transformSound?.PlayTransformSound();

        int enemyLayer = LayerMask.NameToLayer(enemyLayerName);

        if (_isCat)
        {
            transform.localScale = _normalScale * catScaleMultiplier;
            if (_collider != null)
            {
                _collider.size = catColliderSize;
                _collider.offset = catColliderOffset;
                Debug.Log("Gato - Size: " + catColliderSize + " | Offset: " + catColliderOffset);
            }

            if (enemyLayer != -1)
                Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, true);
        }
        else
        {
            transform.localScale = _normalScale;
            if (_collider != null)
            {
                _collider.size = _originalColliderSize;
                _collider.offset = _originalColliderOffset;
                Debug.Log("Humano - Size: " + _originalColliderSize + " | Offset: " + _originalColliderOffset);
            }

            if (enemyLayer != -1)
                Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, false);
        }

        if (_animator != null)
            _animator.SetBool("IsCat", _isCat);
    }

    public bool IsCat => _isCat;
}