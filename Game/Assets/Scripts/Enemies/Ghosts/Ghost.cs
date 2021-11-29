using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float speed;

    public Transform target;

    private Rigidbody2D rig;

    Animator anim;

    public Timer timer;

    private float dieAnimationTime = 0.5f;

    public AudioSource dieSound;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (PlayerPrefs.GetString("Player") == "M")
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("PlayerF").GetComponent<Transform>();
        }

    }

    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (transform.position.x > target.position.x)
        {
            anim.SetBool("right", false);
            anim.SetBool("left", true);
        }
        else
        {
            anim.SetBool("left", false);
            anim.SetBool("right", true);
        }
    }

    void DieAnimation()
    {
        Destroy(gameObject);
    }

    public void Die()
    {
        dieSound.Play();
        anim.SetBool("die", true);
        timer.CreateTimer("ghostDie", dieAnimationTime, 0, false, DieAnimation);
    }

    public void InvertDirection()
    {
        speed *= -1;
    }
}
