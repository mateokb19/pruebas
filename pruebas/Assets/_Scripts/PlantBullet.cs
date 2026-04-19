using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    private Vector2 shootDirection = Vector2.right;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        shootDirection = direction.normalized;
    }

    void Start()
    {
        // La bala se mueve en la dirección que se le pasó
        rb.velocity = shootDirection * speed;

        // Rota la bala hacia la dirección que va
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si golpea al jugador, le hace daño
        if (collision.CompareTag("Player"))
        {
            PlayerStats.health -= 10; // Le resta 10 de vida (ajusta según necesites)
            Destroy(gameObject);
        }
    }
}
