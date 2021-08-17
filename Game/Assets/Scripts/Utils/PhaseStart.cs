using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseStart : MonoBehaviour
{

    public Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        phaseInit();
    }

    void phaseInit()
    {
        if (player.transform.position.x >= 30.85 && player.transform.position.x <= 34.18)
        {
            if (Input.GetKey(KeyCode.Space))
                DeleteAll("Cena Taero");
        }
        else if (player.transform.position.x >= 70.9 && player.transform.position.x <= 74.29)
        {
            if (Input.GetKey(KeyCode.Space))
                DeleteAll("Cena 4");
        }
    }

    public void DeleteAll(string scene)
    {
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            Destroy(o);
        }
        SceneManager.LoadScene(scene);
    }
}

