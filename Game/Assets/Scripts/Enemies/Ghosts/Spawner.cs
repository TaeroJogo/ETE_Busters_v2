using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ghost;
    float randX;
    Vector2 location;
    private float spawnRate = 2f;
    float nextSpawn = 0.0f;

    void Update()
    {
        if(Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(-11.5f, 4.0f);
            location = new Vector2(randX, transform.position.y);
            Instantiate(ghost, location, Quaternion.identity);
        }
    }
}
