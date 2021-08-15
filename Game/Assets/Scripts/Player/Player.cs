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

    private bool isPunching = false;
    private float punchTime = 0.4f;

    private bool isKicking = false;
    private float kickTime = 0.6f;

    public Timer timer;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
        if (isGrounded) {
            anim.SetBool("kicking", false);
            isKicking = false;
            kickTime = 0.6f;
        }

        Move();
        Jump();
        
        ShootHandler();
        PhysicalAttackHandler();
    }

    private void RestartPunch(){
        isPunching = false;
        anim.SetBool("punching", false);
    }

    private void RestartKick() {
        isKicking = false;
        anim.SetBool("kicking", false);
    }

    private void RestartShoot(){
        anim.SetBool("firing", false);
        anim.SetBool("vert_firing", false);
        canFire = true;
        hasShooted = false;
    }

    void PhysicalAttackHandler() {
        if(!isSneak && canFire){
        if (Input.GetKey(KeyCode.UpArrow) && isGrounded && !isPunching) {
            anim.SetBool("punching", true);
            anim.SetBool("run", false);
            isPunching = true;

            timer.CreateTimer("punch", punchTime, 0, false, RestartPunch);
        }

        if(Input.GetKey(KeyCode.UpArrow) && !isGrounded && !isKicking) {
            isKicking = true;
            anim.SetBool("kicking", true);

            timer.CreateTimer("kick", kickTime, 0, false, RestartKick);
        }
    }
    }

    void ShootHandler(){

        if(!isPunching) {
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

            timer.CreateTimer("shoot", fireRateTime, 0, false, RestartShoot);
        }

        if(!canFire){
            if (timer.GetTimeStamp("shoot") <= 0.2 && !hasShooted) {
                Shoot();
                hasShooted = true;
            }
        }
        }
    }
   
    public void Shoot(){
        var diretion = shootDirection == "" ? Quaternion.Euler(0, 0, 0) : shootDirection == "up" ? Quaternion.Euler(0, 0, 90) : Quaternion.Euler(0, 0, 45);
        var position = new Vector3(firePoint.position.x, firePoint.position.y , 0);

        if(shootDirection == "up"){ 
            if(transform.rotation.eulerAngles.y > 0){
                position.x = firePoint.position.x + 2;
            }
            else {
                position.x = firePoint.position.x - 2;
            }
        }
        Instantiate(bulletPrefab, position, firePoint.rotation * diretion);
    }

    void Move(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        
        if(!isSneak && canFire && !standShoot && !isPunching) {
            transform.position += movement * Time.deltaTime * Speed;
        }

            if(canFire && !hasShooted && !isPunching && !isKicking){
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

        if (Input.GetKeyDown("w") && isGrounded && !isSneak && !standShoot)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ghost")) {
            Destroy(other.gameObject);
        }
    }
}
