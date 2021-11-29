using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public Boss boss;
    public GameObject ghost;
    float randX;
    Vector2 location;
    private float spawnRate = 2f;
    float nextSpawn = 0.0f;

    public float minSpawnLocation;
    public float maxSpawnLocation;

    void Update()
    {
        if(boss.health > 0)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnRate;
                randX = Random.Range(minSpawnLocation, maxSpawnLocation);
                location = new Vector2(randX, transform.position.y);
                Instantiate(ghost, location, Quaternion.identity);
            }
        }
    }
}
