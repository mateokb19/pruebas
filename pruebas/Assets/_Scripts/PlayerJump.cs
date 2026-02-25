using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    public float jumpForce;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    public void Jump()
    {
        if (Mathf.Abs(_rb.velocity.y) < 0.01)
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
