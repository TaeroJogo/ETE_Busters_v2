using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;//faz a carteirinha mover-se
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        List<string> whiteListCollider = new List<string> { "Player", "Pufe" }; //lista de objetos que não podem ser colididos, ou seja, nao pode destruir a carteirinha

        if (hitInfo.tag == "Boss")
        {
            Boss boss = hitInfo.GetComponent<Boss>();
            boss.TakeDamage();//boss leva dano
        }
        if (!whiteListCollider.Contains(hitInfo.tag))
        {
            Destroy(gameObject);
        }
        if (hitInfo.tag == "Ghost")
        {
            Ghost ghost = hitInfo.GetComponent<Ghost>();
            ghost.Die();//ghost morre
            ghost.InvertDirection();//ghost leva um knockback para a direcao oposta
        }
        if (hitInfo.tag == "BossPew")
        {
            BossPew bossPew = hitInfo.GetComponent<BossPew>();
            bossPew.DestroyBossPew();//boss pew morre
        }
    }
}
