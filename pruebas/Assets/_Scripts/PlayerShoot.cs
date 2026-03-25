using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform startPos;

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameObject bulletClone = Instantiate(bullet, startPos.position, startPos.rotation);
            Destroy(bulletClone, 1);
        }
    }
}