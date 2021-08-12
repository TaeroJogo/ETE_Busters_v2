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

    private bool canFire = true;
    private bool hasShooted = false;
    private float fireRateTime = 0.4f;
    private bool standShoot = false;
    private string shootDirection;

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
        
        ShootHandler();
    }

    void ShootHandler(){

        if(Input.GetKey("left shift")){
           standShoot = true;
           anim.SetBool("run", false);
        }
        else{
            standShoot = false;
        }

        if (Input.GetKey(KeyCode.RightArrow) && !isSneak && isGrounded && canFire) {
             if(standShoot){
                  if ((Input.GetKey("a") || Input.GetKey("d")) && Input.GetKey("w")) {
                    anim.SetBool("firing", true);
                    shootDirection = "dig";
                  }
                  else if(Input.GetKey("w")) {
                      shootDirection = "up";
                      anim.SetBool("vert_firing", true);
                  }
                  else {
                    anim.SetBool("firing", true);
                    shootDirection = "";
                  }
             }
             else {
                 anim.SetBool("firing", true);
                 shootDirection = "";
             }

            canFire = false;
            anim.SetBool("run", false);
        }

        if(!canFire){
            fireRateTime -= Time.deltaTime;

            if (fireRateTime <= 0.2 && !hasShooted) {
                Shoot();
                hasShooted = true;
            }
            if (fireRateTime <= 0)
            {
                anim.SetBool("firing", false);
                anim.SetBool("vert_firing", false);
                canFire = true;
                hasShooted = false;
                fireRateTime = 0.4f;
            }
        }
    }
   
    void Shoot(){
        var diretion = shootDirection == "" ? Quaternion.Euler(0, 0, 0) : shootDirection == "up" ? Quaternion.Euler(0, 0, 90) : Quaternion.Euler(0, 0, 45);
        var position = new Vector3(firePoint.position.x, firePoint.position.y , 0);

        if(shootDirection == "up"){ 
            if(transform.rotation.eulerAngles.y > 0){
                position.x = firePoint.position.x + 0.5f;
            }
            else {
                position.x = firePoint.position.x - 0.5f;
            }
        }
        Instantiate(bulletPrefab, position, firePoint.rotation * diretion);
    }

    void Move(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        
        if(!isSneak && canFire && !standShoot) {
            transform.position += movement * Time.deltaTime * Speed;
        }

            if(canFire && !hasShooted){
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
