using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {
    public Transform trigger;
    public Transform maxYPoint;
    
    private ObjectPooler objectPooler;
    private int platformSpawned = 0;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    public Transform SpawnPlatform(float x, float y, float distanceBetween = 10f, bool leaveBlank = false)
    {
        GameObject platform = objectPooler.GetPooledObject("Platform");

        platform.transform.position = new Vector2(x, y);
        platform.GetComponent<ObjectDestroyer>().offsetX = 8f;

        if (!leaveBlank)
        {
            if (Random.Range(0f, 1f) < 0.5f)
                SpawnObject("Spikes", platform);

            if (Random.Range(0f, 1f) < 0.5f)
            {
                SpawnObject("Mummy", platform);
            }
        }

        platform.SetActive(true);
        platformSpawned++;
        transform.position = new Vector3(transform.position.x + distanceBetween, transform.position.y, transform.position.z);
        return platform.transform;
    }

    public void SpawnObject(string tag, GameObject platform)
    {
        GameObject spawnObj = objectPooler.GetPooledObject(tag);

        float spawnObjX = platform.transform.position.x;
        float spawnObjY = platform.transform.position.y;
        if (tag == "Spikes")
        {
            float[] offsetXs = { 2.35f, 4.6f, 6.9f };
            spawnObjX += offsetXs[Random.Range(offsetXs.Length - 1, offsetXs.Length)];
            
            spawnObjY += 0.5f;
        }
        else if(tag == "Mummy")
        {
            float[] offsetXs = { 1.5f, 3.8f, 6f };
            spawnObjX += offsetXs[Random.Range(0, offsetXs.Length)];

            spawnObjY += 1.8f;
        }

        spawnObj.transform.position = new Vector2(spawnObjX, spawnObjY);
        spawnObj.transform.rotation = platform.transform.rotation;
        
        spawnObj.SetActive(true);
    }


    private void Update()
    {
        if (transform.position.x <= trigger.position.x)
        {
            float platformX = transform.position.x;
            float platformY = transform.position.y + Random.Range(0f, 1f);
            float platformDistance = 12f;

            bool leaveBlank = platformSpawned < 1;
            SpawnPlatform(platformX, platformY, platformDistance, leaveBlank);

        }
    }
}
