using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 4;
    public float JumpForce = 11;
    
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundlayer;
    
    private Rigidbody2D rig;

    Animator anim;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private bool isSneak;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);

        Move();
        Jump();

        if (Input.GetKeyDown(KeyCode.RightArrow) && !isSneak && isGrounded) {
            Shoot();
        }
    }

    void Shoot(){
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void Move(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        
        if(!isSneak) {
            transform.position += movement * Time.deltaTime * Speed;
        }

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
            if (Input.GetKey("s")) {
                isSneak = true;
                anim.SetBool("sneak", true);
            }
            else {
                isSneak = false;
                anim.SetBool("sneak", false);
            }
    }

    void Jump(){
        if (isGrounded)
        {
            anim.SetBool("jump", false);
        }
        else
        {
            anim.SetBool("run", false);
            anim.SetBool("jump", true);
        }

        if (Input.GetButtonDown("Jump") && isGrounded && !isSneak)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }
}
