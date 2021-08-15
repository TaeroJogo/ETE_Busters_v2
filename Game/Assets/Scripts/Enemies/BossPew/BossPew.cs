using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPew : MonoBehaviour
{

    public Timer timer;
    private Boss boss;

    Animator anim;
    private float dieTimeAnim = 0.4f;
    private float speed = -2f;
    public Rigidbody2D rb;


    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("flying", true);
        rb.velocity = transform.right * speed;
    }

    private void Die()
    {
        anim.SetBool("die", false);
        Destroy(gameObject);
    }

    public void DestroyBossPew()
    {
        anim.SetBool("flying", false);
        anim.SetBool("die", true);
        rb.velocity = transform.right * 0;
        timer.CreateTimer("bossDieAnimation", dieTimeAnim, 0, false, Die);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
