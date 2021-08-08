using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)) {
            Shoot();
        }
    }

    void Shoot(){

    }
}
