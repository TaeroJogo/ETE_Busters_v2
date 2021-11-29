using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    private Rigidbody2D rig;
    private float verticalSpeed = 2f;
    private float initialX;
    private bool canMove = true;

    public bool isDead = false;
    private float deadTimeAnim = 2f;

    private Transform playerTransform;

    private int health = 20;
    private int damageAmount = 1;

    private float sprintAttackTime = 5f;
    private bool canSprintAttack = false;
    private float waitForFirstSprintAttack = 8f;

    private bool sprintAttacking = false;
    private float sprintAttackingTime = 1f;

    private bool canShoot = false;
    private float waitForFirstShootAttack = 5f;
    private float shootAttackCooldown = 6f;

    public Timer timer;
    public GameObject bossPew;
    public Transform bossFirePoint;

    public AudioSource death;
    public AudioSource hit;
    public AudioSource shoot;
    public AudioSource sprint;

    private float revivinAnimationTime = 2.7f;
    Animator anim;

    public Rigidbody2D rb;

    private Player player;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (PlayerPrefs.GetString("Player") == "M")
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        else
        {
            playerTransform = GameObject.FindGameObjectWithTag("PlayerF").transform;
            player = GameObject.FindGameObjectWithTag("PlayerF").GetComponent<Player>();
        }

        initialX = transform.position.x;

        if (sceneName == "Cena 4")
        {
            health = 30;
            canMove = false;
            anim.SetBool("revive", true);
            timer.CreateTimer("bossReviving", revivinAnimationTime, 0, false, ActivateBasicsFunctions);
        }
        else
        {
            ActivateBasicsFunctions();
        }
    }

    void ActivateBasicsFunctions()
    {
        canMove = true;
        anim.SetBool("revive", false);
        anim.SetBool("attack", true);
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
        if (!sprintAttacking)
        {
            shoot.Play();
            canShoot = true;
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
        canSprintAttack = true;
        anim.SetBool("attack", true);
    }

    void RestartSprintAttack()
    {
        sprint.Play();
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
            canMove = true;
            canSprintAttack = false;
            timer.CreateTimer("bossSprintAttack", sprintAttackTime, 0, false, RestartSprintAttack);
        }
        if (sprintAttacking)
        {
            float dir = -1;

            if (transform.position.y > playerTransform.position.y)
            {
                dir = 1;
            }
            if (transform.position.x > (initialX - 7f))
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerTransform.position.x, playerTransform.position.y + (2.5f * dir)), verticalSpeed * 5 * Time.deltaTime);
            }
        }
    }

    void Move()
    {
        if (canMove)
        {
            if (transform.position.y > playerTransform.position.y)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(initialX, playerTransform.position.y + 2.5f), verticalSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(initialX, playerTransform.position.y - 2.5f), verticalSpeed * Time.deltaTime);
            }
        }
    }

    public void TakeDamage()
    {
        health -= damageAmount;
        if (!isDead)
        {
            hit.Play();
        }
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
        if (!isDead)
        {
            death.Play();
        }
        player.EndGame(true);
        isDead = true;
        anim.SetBool("die", true);
        timer.CreateTimer("bossDieAnimation", deadTimeAnim, 0, false, DestroyBoss);
    }
}
