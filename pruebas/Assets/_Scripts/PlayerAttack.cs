using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private Rigidbody2D _rb;

    public float bounceForce;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (_rb.velocity.y < 0)
            {
                _rb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
                EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
                if (enemy != null) enemy.canMove = false;
                Destroy(collision.gameObject, 1);
            }

        }
    }


}
