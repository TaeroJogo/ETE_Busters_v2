using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float speed = 20f;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        if(hitInfo.tag == "Boss"){
            Boss boss = hitInfo.GetComponent<Boss>();
            boss.TakeDamage();
        }
        Destroy(gameObject);
  }
}
