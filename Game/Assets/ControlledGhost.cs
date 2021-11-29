using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledGhost : MonoBehaviour
{
    private Rigidbody2D rig;

    public Vector3 target;

    Animator anim;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, 1 * Time.deltaTime);
        anim.SetBool("right", true);
    }
}
