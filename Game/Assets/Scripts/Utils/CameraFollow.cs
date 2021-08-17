using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        //faz a camera seguir o player, mas ela vai ate certo ponto, para nao sair da cena
        Vector3 temp = transform.position;
        if (player.position.x >= 37.75 && player.position.x <= 77.3)
        {
            temp.x = player.position.x;
            transform.position = temp;
        }
    }
}
