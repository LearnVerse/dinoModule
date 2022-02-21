using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public List<GameObject> resources;
    public Vector2 spawnRange;
    private Vector3 originSpawn;
    public int maxResources;

    void Start()
    {
        originSpawn = gameObject.transform.position;

        Vector2 randXZ = new Vector2(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y));
        int randIdx = Random.Range(0, resources.Count);

        for(int i = 0; i < maxResources; i++) {

            // Shoot ray down to determine spawn y coordinate
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(originSpawn + new Vector3(randXZ.x, 0, randXZ.y), transform.TransformDirection(Vector3.down), out hit, 500))
            {
                Instantiate(resources[randIdx], new Vector3(randXZ.x, hit.point.y, randXZ.y), Quaternion.identity);

                randIdx = Random.Range(0, resources.Count);
                randXZ = new Vector2(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y));
            }
            else
            {
                Debug.Log("No spawnable terrain detected.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
