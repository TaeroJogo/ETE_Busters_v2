using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jp : MonoBehaviour
{
    Animator anim;

    public Boss boss;

    public Dialogue dialogue;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        if(boss.health <= 0) {
            anim.SetBool("IdleJP", true);
        }
    }
}
