using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _animator;
    private NewInput _newInput;
    private CatMeowSound _catMeowSound;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _newInput = GetComponent<NewInput>();
        _catMeowSound = GetComponent<CatMeowSound>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementAnim();
    }


    public void MovementAnim()
    {
        if(_newInput.inputX > 0 || _newInput.inputX < 0)
        {
            _animator.SetBool("Bool", true);
            _catMeowSound?.OnCatMovement();
        }else if(_newInput.inputX == 0)
        {
            _animator.SetBool("Bool", false);
            _catMeowSound?.OnCatIdle();
        }
    }
}
