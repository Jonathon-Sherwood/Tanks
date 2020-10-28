using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    public GameObject objecToSpawn;
    private GameObject spawnedObject = null;
    public float respawnTime;
    private float countdown;
    public bool spawnOnStart = true;

    // Start is called before the first frame update
    void Start()
    {
        countdown = respawnTime;

        if (spawnOnStart)
        {
            Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedObject == null)
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {
                Spawn();
            }
        }
    }

    public void Spawn()
    {
        spawnedObject = Instantiate(objecToSpawn, transform.position, transform.rotation) as GameObject;

        countdown = respawnTime;
    }
}
