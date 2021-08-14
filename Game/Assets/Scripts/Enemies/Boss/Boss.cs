using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Rigidbody2D rig;
    private float verticalSpeed = 2;

    private float moveTime = 1;
    private bool canMove = true;

    private Transform playerTransform;

    private int health = 10;
    private int damageAmount = 1;

    private float sprintAttackTime = 5f;
    private bool canSprintAttack = false;
    private float waitForFirstSprintAttack = 8f;

    private bool sprintAttacking = false;
    private float sprintAttackingTime = 1f;

    public Timer timer;

    void Start()
    {

        rig = GetComponent<Rigidbody2D>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        timer.CreateTimer("bossSprintAttack", waitForFirstSprintAttack, 0, false, ActivateSprintAttack);
    }

    void Update()
    {
        Move();
        SprintAttack();
    }

    void ActivateSprintAttack()
    {
        canSprintAttack = true;
    }


    void StopSprintAttacking()
    {
        sprintAttacking = false;
        canSprintAttack = true;
    }

    void RestartSprintAttack()
    {
        sprintAttacking = true;
        canMove = false;
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
            Destroy(gameObject);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
