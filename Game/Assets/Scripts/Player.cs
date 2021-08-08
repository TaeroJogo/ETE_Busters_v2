using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 4;
    public float JumpForce = 8;
    
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundlayer;
    
    private Rigidbody2D rig;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);

        Move();
        Jump();
    }

    void Move(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
    }

    void Jump(){
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }
}
