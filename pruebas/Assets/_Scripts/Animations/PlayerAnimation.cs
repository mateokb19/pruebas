using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _animator;
    private NewInput _newInput;
    private CatMeowSound _catMeowSound;
    private CatTransform _catTransform;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _newInput = GetComponent<NewInput>();
        _catMeowSound = GetComponent<CatMeowSound>();
        _catTransform = GetComponent<CatTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementAnim();
    }


    public void MovementAnim()
    {
        bool isCat = _catTransform != null && _catTransform.IsCat;

        if(_newInput.inputX > 0 || _newInput.inputX < 0)
        {
            _animator.SetBool("Bool", true);
            if (isCat) _catMeowSound?.OnCatMovement();
        }
        else if(_newInput.inputX == 0)
        {
            _animator.SetBool("Bool", false);
            if (isCat) _catMeowSound?.OnCatIdle();
        }
    }
}
