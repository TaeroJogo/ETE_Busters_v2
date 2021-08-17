using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

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
