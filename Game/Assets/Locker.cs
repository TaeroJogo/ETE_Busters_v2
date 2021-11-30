using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    Animator anim;

    public GameObject gameObject1;
    public GameObject gameObject2;

    public AudioSource lockerSound;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerF"))
        {
            Player player = other.GetComponent<Player>();
            if (player.isPunching)
            {
                lockerSound.Play();
                gameObject1.SetActive(true);
                gameObject2.SetActive(true);
                anim.SetBool("aberto", true);
            }
        }
    }
}
