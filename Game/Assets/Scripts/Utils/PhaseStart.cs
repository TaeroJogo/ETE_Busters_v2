using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseStart : MonoBehaviour
{

    public Player player;

    void Start()
    {
        if (PlayerPrefs.GetString("Player") == "M") {
            Destroy(GameObject.Find("Parent").transform.Find("PlayerF").gameObject);
            player = GameObject.Find("Parent").transform.Find("Player").GetComponent<Player>();
        } else {
            Destroy(GameObject.Find("Parent").transform.Find("Player").gameObject);
            GameObject.Find("Parent").transform.Find("PlayerF").gameObject.SetActive(true);
            player = GameObject.Find("Parent").transform.Find("PlayerF").GetComponent<Player>();
        }

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
        Destroy(GameObject.Find("Parent"));
        SceneManager.LoadScene(scene);
    }
}

