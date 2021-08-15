using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Rigidbody2D rig;
    private float verticalSpeed = 2;

    private float moveTime = 1;
    private bool canMove = true;

    public bool isDead = false;
    private float deadTimeAnim = 2f;

    private Transform playerTransform;

    private int health = 10;
    private int damageAmount = 1;

    private float sprintAttackTime = 5f;
    private bool canSprintAttack = false;
    private float waitForFirstSprintAttack = 8f;

    private bool sprintAttacking = false;
    private float sprintAttackingTime = 1f;

    private bool canShoot = false;
    private float waitForFirstShootAttack = 5f;
    private float shootAttackCooldown = 5f;

    public Timer timer;
    public GameObject bossPew;
    public Transform bossFirePoint;

    Animator anim;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("attack", true);

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        timer.CreateTimer("bossSprintAttack", waitForFirstSprintAttack, 0, false, ActivateSprintAttack);
        timer.CreateTimer("bossActivateShootAttack", waitForFirstShootAttack, 0, false, ActivateShoot);
    }

    void Update()
    {
        if (!isDead)
        {
            Move();
            SprintAttack();
            if (canShoot)
            {
                canShoot = false;
                timer.CreateTimer("bossShootAttack", shootAttackCooldown, 0, false, ShootBossPew);
            }
        }
    }

    void ActivateShoot()
    {
        canShoot = true;
    }

    void ShootBossPew()
    {
        canShoot = true;
        if (!sprintAttacking)
        {
            Instantiate(bossPew, bossFirePoint.position, Quaternion.Euler(0, 0, 0));
        }
    }

    void ActivateSprintAttack()
    {
        canSprintAttack = true;
    }


    void StopSprintAttacking()
    {
        sprintAttacking = false;
        ShootBossPew();
        canShoot = true;
        canSprintAttack = true;
        anim.SetBool("attack", true);
    }

    void RestartSprintAttack()
    {
        sprintAttacking = true;
        canShoot = false;
        canMove = false;
        anim.SetBool("attack", false);
        timer.CreateTimer("bossSprintAttacking", sprintAttackingTime, 0, false, StopSprintAttacking);
    }

    void SprintAttack()
    {
        if (canSprintAttack)
        {
            canSprintAttack = false;
            timer.CreateTimer("bossSprintAttack", sprintAttackTime, 0, false, RestartSprintAttack);
        }
        if (sprintAttacking)
        {
            Vector3 p = transform.position;
            if (p.x > -9)
            {
                p.x -= 0.01f;
            }
            transform.position = p;
        }
        else
        {
            Vector3 p = transform.position;
            if (p.x < 6.5)
            {
                p.x += 0.01f;
            }
            transform.position = p;
        }
    }

    void RestartMove()
    {
        canMove = true;
        if (transform.position.y > playerTransform.position.y)
        {
            rig.AddForce(new Vector2(0f, -verticalSpeed), ForceMode2D.Impulse);
        }
        else
        {
            rig.AddForce(new Vector2(0f, verticalSpeed), ForceMode2D.Impulse);
        }
    }

    void Move()
    {
        if (canMove)
        {
            canMove = false;
            timer.CreateTimer("bossMove", moveTime, 0, false, RestartMove);
        }
    }

    public void TakeDamage()
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    void DestroyBoss()
    {
        anim.SetBool("die", false);
        Destroy(gameObject);
    }

    void Die()
    {
        isDead = true;
        anim.SetBool("die", true);
        timer.CreateTimer("bossDieAnimation", deadTimeAnim, 0, false, DestroyBoss);
    }
}
