using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Speed = 4;
    public float JumpForce = 11;

    bool isGrounded;
    public Transform groundCheck;
    public Transform groundCheck2;
    public LayerMask groundlayer;

    private Rigidbody2D rig;

    Animator anim;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private bool isSneak;

    private bool canFire = true;
    private bool hasShooted = false;
    private float fireRateTime = 0.4f;
    private bool standShoot = false;
    private string shootDirection;

    private bool isPunching = false;
    private float punchTime = 0.4f;

    private bool isKicking = false;
    private float kickTime = 0.6f;

    public Timer timer;
    public HealthBar healthBar;

    public IDCardsCounterController idCardsCounter;
    private int idCards = 100;

    public AudioSource punch;
    public AudioSource kick;
    public AudioSource jump;
    public AudioSource cardthrow;
    public AudioSource victoryy;
    public AudioSource finish;

    public bool canPlay = true;
    public GameObject loseMsg;
    public GameObject winMsg;

    private bool canTakeDamage = true;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        loseMsg.gameObject.SetActive(false);
        winMsg.gameObject.SetActive(false);

    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer) || Physics2D.OverlapCircle(groundCheck2.position, 0.2f, groundlayer);
        if (isGrounded)
        {
            anim.SetBool("kicking", false);
            isKicking = false;
            kickTime = 0.6f;
        }
        if (canPlay)
        {
            Move();
            Jump();

            ShootHandler();
            PhysicalAttackHandler();
        }
    }

    public void DeleteAll()
    {
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            Destroy(o);
        }
        SceneManager.LoadScene("Corredor 1");
    }

    public void EndGame(bool victory)
    {
        if (!victory)
        {
            loseMsg.gameObject.SetActive(true);
            anim.SetBool("sneak", true);
        }
        else
        {
            winMsg.gameObject.SetActive(true);
            anim.SetBool("sneak", false);
        }
        anim.SetBool("jump", false);
        anim.SetBool("run", false);
        anim.SetBool("kicking", false);
        timer.CreateTimer("DeleteAll", 3f, 0, false, DeleteAll);
        canPlay = false;
        canTakeDamage = false;
    }

    private void RestartPunch()
    {
        isPunching = false;
        anim.SetBool("punching", false);
    }

    private void RestartKick()
    {
        isKicking = false;
        anim.SetBool("kicking", false);
    }

    private void RestartShoot()
    {
        anim.SetBool("firing", false);
        anim.SetBool("vert_firing", false);
        canFire = true;
        hasShooted = false;
    }

    void PhysicalAttackHandler()
    {
        if (!isSneak && canFire)
        {
            if (Input.GetKey(KeyCode.UpArrow) && isGrounded && !isPunching)
            {
                anim.SetBool("punching", true);
                anim.SetBool("run", false);
                isPunching = true;
                punch.Play();

                timer.CreateTimer("punch", punchTime, 0, false, RestartPunch);
            }

            if (Input.GetKey(KeyCode.UpArrow) && !isGrounded && !isKicking)
            {
                isKicking = true;
                anim.SetBool("kicking", true);
                kick.Play();

                timer.CreateTimer("kick", kickTime, 0, false, RestartKick);
            }
        }
    }

    void ShootHandler()
    {

        if (!isPunching && idCards > 0)
        {
            if (Input.GetKey("left shift"))
            {
                standShoot = true;
                anim.SetBool("run", false);
            }
            else
            {
                standShoot = false;
            }

            if (Input.GetKey(KeyCode.RightArrow) && !isSneak && isGrounded && canFire)
            {
                if (standShoot)
                {
                    if ((Input.GetKey("a") || Input.GetKey("d")) && Input.GetKey("w"))
                    {
                        anim.SetBool("firing", true);
                        shootDirection = "dig";
                    }
                    else if (Input.GetKey("w"))
                    {
                        shootDirection = "up";
                        anim.SetBool("vert_firing", true);
                    }
                    else
                    {
                        anim.SetBool("firing", true);
                        shootDirection = "";
                    }
                }
                else
                {
                    anim.SetBool("firing", true);
                    shootDirection = "";
                }

                canFire = false;
                anim.SetBool("run", false);

                idCards--;
                idCardsCounter.UpdateIDCardsCounter(idCards.ToString());
                cardthrow.Play();
                timer.CreateTimer("shoot", fireRateTime, 0, false, RestartShoot);
            }

            if (!canFire)
            {
                if (timer.GetTimeStamp("shoot") <= 0.2 && !hasShooted)
                {
                    Shoot();
                    hasShooted = true;
                }
            }
        }
    }

    public void Shoot()
    {
        var diretion = shootDirection == "" ? Quaternion.Euler(0, 0, 0) : shootDirection == "up" ? Quaternion.Euler(0, 0, 90) : Quaternion.Euler(0, 0, 45);
        var position = new Vector3(firePoint.position.x, firePoint.position.y, 0);

        if (shootDirection == "up")
        {
            if (transform.rotation.eulerAngles.y > 0)
            {
                position.x = firePoint.position.x + 2;
            }
            else
            {
                position.x = firePoint.position.x - 2;
            }
        }
        Instantiate(bulletPrefab, position, firePoint.rotation * diretion);
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        if (!isSneak && canFire && !standShoot && !isPunching)
        {
            transform.position += movement * Time.deltaTime * Speed;
        }

        if (canFire && !hasShooted && !isPunching && !isKicking)
        {
            if (Input.GetAxis("Horizontal") > 0f)
            {
                anim.SetBool("run", true);
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }

            if (Input.GetAxis("Horizontal") < 0f)
            {
                anim.SetBool("run", true);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }

            if (Input.GetAxis("Horizontal") == 0f)
            {
                anim.SetBool("run", false);
            }
            if (Input.GetKey("s"))
            {
                isSneak = true;
                anim.SetBool("run", false);
                anim.SetBool("sneak", true);
            }
            else
            {
                isSneak = false;
                anim.SetBool("sneak", false);
            }
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            anim.SetBool("jump", false);
        }
        else
        {
            anim.SetBool("run", false);
            anim.SetBool("jump", true);
        }

        if (Input.GetKeyDown("w") && isGrounded && !isSneak && !standShoot)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            jump.Play();
        }

    }

    bool HasPlayerTakenDamage(Transform entityT, int damage)
    {

        bool hasTakenDamage = true;
        if (isKicking || isPunching)
        {
            if (!((transform.forward.z == 1 && entityT.position.x > (transform.position.x + 1.2)) || (transform.forward.z == -1 && entityT.position.x > (transform.position.x - 1.2))))
            {
                if (canTakeDamage)
                    healthBar.loseHealth(damage);
            }
            else
            {
                hasTakenDamage = false;
            }
        }
        else if (!isKicking && !isPunching)
        {
            if (canTakeDamage)
                healthBar.loseHealth(damage);
        }

        return hasTakenDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ghost"))
        {
            Ghost ghost = other.GetComponent<Ghost>();
            ghost.Die();

            if (!HasPlayerTakenDamage(ghost.transform, 5))
            {
                ghost.InvertDirection();
                idCards += 5;
                idCardsCounter.UpdateIDCardsCounter(idCards.ToString());
            }
        }
        if (other.gameObject.CompareTag("BossPew"))
        {
            BossPew pew = other.GetComponent<BossPew>();
            pew.DestroyBossPew();
            if (!HasPlayerTakenDamage(pew.transform, 2))
            {
                idCards += 3;
                idCardsCounter.UpdateIDCardsCounter(idCards.ToString());
            }
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            Boss boss = other.GetComponent<Boss>();
            if (!HasPlayerTakenDamage(boss.transform, 10))
            {
                boss.TakeDamage();
            }
        }
    }

    public void ManualRun()
    {
        anim.SetBool("run", true);
        rig.velocity = new Vector2(5, 0);
    }
}
