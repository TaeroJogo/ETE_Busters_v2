using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform camera;

    void FixedUpdate() {
        if(player.position.x >= 37.75 && player.position.x <= 77.3)
        transform.position = new Vector3(player.position.x, camera.position.y, camera.position.z);
    }
}
