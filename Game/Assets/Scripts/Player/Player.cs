using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer) || Physics2D.OverlapCircle(groundCheck2.position, 0.2f, groundlayer);//ve se esses dois objetos estao colidindo com o chao, se pelo menos um deles esta, quer dizer que o player esta no chao
        if (isGrounded)
        {
            anim.SetBool("kicking", false);
            isKicking = false;
            kickTime = 0.6f;
        }

        Move();
        Jump();

        ShootHandler();
        PhysicalAttackHandler();
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
            if (Input.GetKey(KeyCode.UpArrow) && isGrounded && !isPunching)//se estiver com as teclas do soco, comeca o soco
            {
                anim.SetBool("punching", true);
                anim.SetBool("run", false);
                isPunching = true;
                punch.Play();

                timer.CreateTimer("punch", punchTime, 0, false, RestartPunch);//cria o timer para o soco, apos a animacao do soco, ele sera reiniciado, como se fosse um soco rate
            }

            if (Input.GetKey(KeyCode.UpArrow) && !isGrounded && !isKicking)//se estiver com as teclas do chute, comeca o chute
            {
                isKicking = true;
                anim.SetBool("kicking", true);
                kick.Play();

                timer.CreateTimer("kick", kickTime, 0, false, RestartKick);//cria o timer para o chute, apos a animacao do chute, ele sera reiniciado, como se fosse um chute rate
            }
        }
    }

    void ShootHandler()
    {

        if (!isPunching && idCards > 0)//verifica se a carteirinhas disponiveis
        {
            if (Input.GetKey("left shift"))
            {
                standShoot = true;//se estiver passando com o shift, ele atira parado
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

                idCards--;//diminui a quantidade de carterinhas
                idCardsCounter.UpdateIDCardsCounter(idCards.ToString());//autaliza o contador da tela
                cardthrow.Play();//toca o som
                timer.CreateTimer("shoot", fireRateTime, 0, false, RestartShoot);//cria o timer para o atirar, apos a animacao do atirar, ele sera reiniciado, como se fosse um atirar rate
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
        var diretion = shootDirection == "" ? Quaternion.Euler(0, 0, 0) : shootDirection == "up" ? Quaternion.Euler(0, 0, 90) : Quaternion.Euler(0, 0, 45);//baseado em qual telcla que ele esta segurando, seta a direcao do tiro
        var position = new Vector3(firePoint.position.x, firePoint.position.y, 0);

        //quando estiver atirando para cima, cria um offset para os lados, antes ele ve se o player da olhando para a esquerda ou direita, e se estiver, o offset sera diferente
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
        Instantiate(bulletPrefab, position, firePoint.rotation * diretion);//cria o objeto da carteirinha
    }

    void Move()
    {
        //logica de movevimento
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
        //logica de pulo
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

    bool HasPlayerTakenDamage(Transform entityT, int damage)//retorna true se o player tiver recebido algum dano, se o player estiver chutando ou socando e o objeto esta na frente dele, ele nao leva dano
    {

        bool hasTakenDamage = true;
        if (isKicking || isPunching)
        {
            //ve se o objeto que colidiu com o player esta na frente dele
            if (!((transform.forward.z == 1 && entityT.position.x > (transform.position.x + 1.2)) || (transform.forward.z == -1 && entityT.position.x > (transform.position.x - 1.2))))
            {
                healthBar.loseHealth(damage);
            }
            else
            {
                hasTakenDamage = false;
            }
        }
        else if (!isKicking && !isPunching)
        {
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

            if (!HasPlayerTakenDamage(ghost.transform, 5))//retorna true se o player tiver recebido algum dano, se o player estiver chutando ou socando e o objeto esta na frente dele, ele nao leva dano
            {
                ghost.InvertDirection();//da um knockback no ghost para a direcao contraria do player
                idCards += 5;
                idCardsCounter.UpdateIDCardsCounter(idCards.ToString());//atualiza o contador de carterijnhas, porque quando ele soca ou chhuta, ele recebe careteirinahs
            }
        }
        if (other.gameObject.CompareTag("BossPew"))
        {
            BossPew pew = other.GetComponent<BossPew>();//da um knockback no ghost para a direcao contraria do player
            pew.DestroyBossPew();
            if (!HasPlayerTakenDamage(pew.transform, 2))//retorna true se o player tiver recebido algum dano, se o player estiver chutando ou socando e o objeto esta na frente dele, ele nao leva dano
            {
                idCards += 3;
                idCardsCounter.UpdateIDCardsCounter(idCards.ToString());//retorna true se o player tiver recebido algum dano, se o player estiver chutando ou socando e o objeto esta na frente dele, ele nao leva dano
            }
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            Boss boss = other.GetComponent<Boss>();//da um knockback no ghost para a direcao contraria do player
            if (!HasPlayerTakenDamage(boss.transform, 10))//retorna true se o player tiver recebido algum dano, se o player estiver chutando ou socando e o objeto esta na frente dele, ele nao leva dano
            {
                boss.TakeDamage();
            }
        }
    }
}
