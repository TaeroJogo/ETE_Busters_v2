using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossD : MonoBehaviour
{
    public Dialogue dialogue;

    public GameObject dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("test", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void test()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        Invoke("test2", 4);
    }

    void test2()
    {
        Destroy(dialogueBox);
        FindObjectOfType<DialogueManager>().EndDialogue();
    }
}
