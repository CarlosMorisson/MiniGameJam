using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformController : MonoBehaviour
{
    public GameObject Plataform; 
    public float spawnInterval = 1.0f; 
    private float spawnTimer = 0.0f;
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnObject();
            spawnTimer = 0.0f; 
        }
    }

    private void SpawnObject()
    {
        Instantiate(Plataform, transform.position, Quaternion.identity);
    }
}
