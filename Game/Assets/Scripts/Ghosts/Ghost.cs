using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float speed;

    private Transform target;

    private Rigidbody2D rig;

    Animator anim;

    void Start() {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        
        if(transform.position.x > target.position.x) {
            anim.SetBool("right", false);
            anim.SetBool("left", true);
        }
        else {
            anim.SetBool("left", false);
            anim.SetBool("right", true);
        }
    }

    private void SpawnGhosts() {

    }
}
