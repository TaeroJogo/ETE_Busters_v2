using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Al : MonoBehaviour
{
    Animator anim;

    public Boss boss;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }


    void Update()
    {
       if(boss.health <= 0) {
            anim.SetBool("Idle", true);
        } 
    }
}
