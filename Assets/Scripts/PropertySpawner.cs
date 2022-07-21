using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawn
{
    public GameObject gameObject;
    public float spawnInterval = 0f; // Zero disables periodic spawn
    public Transform transform;

    private float lastSpawnTime = 0f;

    public float getLastSpawnTime()
    {
        if (lastSpawnTime == 0f) lastSpawnTime = Time.time;
        return lastSpawnTime;
    }

    public GameObject spawnObject()
    {
        lastSpawnTime = Time.time;
        return Object.Instantiate(gameObject, transform.position, transform.rotation);
    }
}

public class PropertySpawner : MonoBehaviour
{
    public Spawn[] spawns;

    private List<GameObject> spawnedObjects;

    public void destroyAllSpawnedObjects()
    {
        foreach (GameObject o in spawnedObjects)
        {
            Destroy(o);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        spawnedObjects = new List<GameObject>();
    }

    // Update is called once per frame
    private void Update()
    {
        float time = Time.time;
        foreach (Spawn s in spawns)
        {
            if (s.spawnInterval != 0f && time - (s.getLastSpawnTime() + s.spawnInterval) >= 0f)
            {
                spawnedObjects.Add(s.spawnObject());
            }
        }
    }
}
