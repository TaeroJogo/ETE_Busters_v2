using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        if (PlayerPrefs.GetString("Player") == "M") {
            player = GameObject.Find("Parent").transform.Find("Player").transform;
        } else {
            player = GameObject.Find("Parent").transform.Find("PlayerF").transform;
        }
    }

    void Update()
    {
        Vector3 temp = transform.position;
        if (player.position.x >= 37.75 && player.position.x <= 77.3)
        {
            temp.x = player.position.x;
            transform.position = temp;
        }
    }
}
