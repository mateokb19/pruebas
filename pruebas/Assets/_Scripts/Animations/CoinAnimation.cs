using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TriggerAnim();
    }

    public void TriggerAnim()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Active");

        }
    }
}
