using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    public Vector2Int platformWidths;
    public Vector2Int platformHeights;

    public int platformWidthShrinkingRate;

    [Range(0f, 1f)]
    public float enemyChance;
    [Range(0f, 1f)]
    public float spawnObjChance;

    public Transform trigger;
    public Transform maxYPoint;
    
    private ObjectPooler objectPooler;
    private int enemiesSpawned = 0, spawnObjsSpawned = 0;
    private Transform lastPlatform;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    public Transform SpawnPlatform(float x, float y, float width, float height, float distanceBetween = 0f)
    {
        GameObject platform = objectPooler.GetPooledObject("Platform");
        
        platform.name = "Platform " + width + "x" + height;
        platform.transform.position = new Vector2(x, y);
        platform.transform.localScale = new Vector2(width, height);

        platform.GetComponent<BoxCollider2D>().offset = new Vector2(0.5f, 0f);
        platform.GetComponent<ObjectDestroyer>().offsetX = width;

        platform.SetActive(true);
        transform.position = new Vector3(transform.position.x + width + distanceBetween, transform.position.y, transform.position.z);
        return platform.transform;
    }

    public void SpawnObject(string tag, GameObject platform, float x)
    {
        GameObject spawnObj = objectPooler.GetPooledObject(tag);

        float spawnObjX = platform.transform.position.x + x;
        float spawnObjY = platform.transform.position.y;

        spawnObj.transform.position = new Vector2(spawnObjX, spawnObjY);
        spawnObj.transform.rotation = platform.transform.rotation;
        
        spawnObj.SetActive(true);

        spawnObjsSpawned++;
        spawnObj.name = "spawnObj " + spawnObjsSpawned;
    }


    private void Update()
    {
        if (transform.position.x <= trigger.position.x)
        {
            int platformWidth = Random.Range(platformWidths.x, platformWidths.y);
            int platformHeight = Random.Range(platformHeights.x, platformHeights.y);
            float platformX = transform.position.x;
            float platformY = transform.position.y + Random.Range(0f, platformHeight);
            float platformDistance = 4f;
            SpawnPlatform(platformX, platformY, platformWidth, platformHeight, platformDistance);

        }
    }
}
