using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private NewInput _newInput;
    private Rigidbody2D _rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.score = 0;
        _newInput = GetComponent<NewInput>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() //Fixed es para movimiento
    {
        Movement();
    }
    public void Movement()
    {
        //transform.Translate(Vector3.right * _newInput.inputX * speed * Time.deltaTime);
        _rb.velocity= new Vector2(_newInput.inputX * speed, _rb.velocity.y);
        Flip();
    }

    public void Flip()
    {
        if(_newInput.inputX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(_newInput.inputX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}