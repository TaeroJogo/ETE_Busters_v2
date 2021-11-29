using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Classmate : MonoBehaviour
{

    private Rigidbody2D rig;
    Animator anim;

    public Dialogue dialogue;

    bool hasTriggerDialog = false;

    int pressedCount = 0;

    bool run = false;

    public Timer timer;

    private Player player;

    bool checkPlayerPosition = false;

    public Ghost ghost;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (PlayerPrefs.GetString("Player") == "M")
        {
            Destroy(GameObject.FindGameObjectWithTag("PlayerF").gameObject);
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("Player").gameObject);
        }
    }

    void Update()
    {

        if (checkPlayerPosition)
        {
            if (player.transform.position.x > 60)
            {
                Destroy(GameObject.Find("Parent"));
                SceneManager.LoadScene("Corredor 1");
            }
        }

        if (transform.position.x > 45)
        {
            rig.velocity = new Vector2(-5, 0);
        }
        else
        {
            if (!hasTriggerDialog)
            {
                rig.velocity = new Vector2(0, 0);
                anim.SetBool("parado", true);
                hasTriggerDialog = true;
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            if (Input.GetKeyDown(KeyCode.Space) && hasTriggerDialog)
            {
                if (pressedCount == 2)
                {
                    FindObjectOfType<DialogueManager>().DisplayNextSentence("Player");
                }
                else
                {
                    FindObjectOfType<DialogueManager>().DisplayNextSentence("Martins");
                }

                if (pressedCount == 3)
                {
                    anim.SetBool("give", true);
                }
                if (pressedCount == 4)
                {
                    anim.SetBool("parado", false);
                    anim.SetBool("run", true);
                    run = true;
                }
                pressedCount++;
            }
        }

        if (run)
        {
            rig.velocity = new Vector2(-5, 0);
            if (PlayerPrefs.GetString("Player") == "M")
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("PlayerF").GetComponent<Player>();
            }
            player.ManualRun();
            checkPlayerPosition = true;
        }

    }
}
