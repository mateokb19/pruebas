using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform startPos;

    [SerializeField] private float shootCooldown = 0.4f;

    private CatTransform _catTransform;
    private SpriteRenderer _spriteRenderer;
    private float _cooldownTimer;

    void Start()
    {
        _catTransform = GetComponent<CatTransform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_cooldownTimer > 0f)
            _cooldownTimer -= Time.deltaTime;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started && !_catTransform.IsCat && _cooldownTimer <= 0f)
        {
            float direction = _spriteRenderer.flipX ? -1f : 1f;
            GameObject bulletClone = Instantiate(bullet, startPos.position, startPos.rotation);
            bulletClone.GetComponent<Bullet>().SetDirection(direction);
            Destroy(bulletClone, 1);
            _cooldownTimer = shootCooldown;
        }
    }
}