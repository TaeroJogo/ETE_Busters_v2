using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
  private Rigidbody2D rig;
  private float verticalSpeed = 2;

  private float moveTime = 1.5f;

  private Transform playerTransform;

  private int health = 10;
  private int damageAmount = 1;

  void Start(){
    rig = GetComponent<Rigidbody2D>();

    playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
  }
 
  void Update(){ 
    moveTime -= Time.deltaTime;
    if (moveTime <= 0){
      moveTime = 1.5f;

      if(transform.position.y > playerTransform.position.y) {
        rig.AddForce(new Vector2(0f, -verticalSpeed), ForceMode2D.Impulse);
      }
      else {
        rig.AddForce(new Vector2(0f, verticalSpeed), ForceMode2D.Impulse);
      }
    }
  }

  public void TakeDamage(){
    health -= damageAmount;
    if (health <= 0){
      Destroy(gameObject);
    }
   }

  void Die(){
    Destroy(gameObject);
  }
}
