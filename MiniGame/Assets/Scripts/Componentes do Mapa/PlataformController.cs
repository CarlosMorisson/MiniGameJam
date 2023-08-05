using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformController : MonoBehaviour
{
    public static PlataformController instance;
    public GameObject Plataform; 
    public float spawnInterval = 1.0f; 
    public float spawnTimer = 0.0f;
    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if (StopTime.instance.stopTime == false)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            spawnTimer = 0.0f;
        }
        
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
