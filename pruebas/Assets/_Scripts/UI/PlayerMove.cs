using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private NewInput _newInput;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.score = 0;
        _newInput = GetComponent<NewInput>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate() //Fixed es para movimiento
    {
        Movement();
    }
    public void Movement()
    {
        Debug.Log("Movement ejecutado. InputX: " + _newInput.inputX + " | Speed: " + speed + " | RB: " + (_rb != null));
        //transform.Translate(Vector3.right * _newInput.inputX * speed * Time.deltaTime);
        _rb.velocity= new Vector2(_newInput.inputX * speed, _rb.velocity.y);
        Flip();
    }

    public void Flip()
    {
        if(_newInput.inputX > 0)
            _spriteRenderer.flipX = false;
        else if(_newInput.inputX < 0)
            _spriteRenderer.flipX = true;
    }
}