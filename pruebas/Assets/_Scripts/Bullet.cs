using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    public float speed;
    private float _direction = 1f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetDirection(float dir)
    {
        _direction = dir;
        _spriteRenderer.flipX = dir < 0f;
        _rb.velocity = Vector2.right * _direction * speed;
    }

    private void OnEnable()
    {
        _rb.velocity = Vector2.right * _direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyMovement>()?.TakeDamage(1f);
            Destroy(gameObject);
        }
    }
}
