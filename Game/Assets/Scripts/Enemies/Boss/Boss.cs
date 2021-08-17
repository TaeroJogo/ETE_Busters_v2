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

    private int health = 10;
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

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name; //pega o nome da cena atual
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; //pega o transform do player
        initialX = transform.position.x; //pega a posição inicial do boss e a armazena na variável

        if (sceneName == "Cena 4")//se a cena for a 4, toca a animacao de reviver
        {
            canMove = false;
            anim.SetBool("revive", true);
            timer.CreateTimer("bossReviving", revivinAnimationTime, 0, false, ActivateBasicsFunctions);//depois da funcao de reviver, ativa as funções básicas
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
        timer.CreateTimer("bossSprintAttack", waitForFirstSprintAttack, 0, false, ActivateSprintAttack); //espera um pouco para ativar o ataque sprint
        timer.CreateTimer("bossActivateShootAttack", waitForFirstShootAttack, 0, false, ActivateShoot); //espera um pouco para ativar o ataque de tiro
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
                timer.CreateTimer("bossShootAttack", shootAttackCooldown, 0, false, ShootBossPew);//quando puder atacar, ativa o ataque de tiro
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
            shoot.Play();//toca o som do tiro
            canShoot = true;
            Instantiate(bossPew, bossFirePoint.position, Quaternion.Euler(0, 0, 0));//atira o fantasma
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
        //tem a ver com o sprint ataque
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
            //da o sprint ataque no player
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
        isDead = true;
        anim.SetBool("die", true);
        timer.CreateTimer("bossDieAnimation", deadTimeAnim, 0, false, DestroyBoss);//cria o timer e espera o tempo de animacao de morte para destruir o boss
    }
}
