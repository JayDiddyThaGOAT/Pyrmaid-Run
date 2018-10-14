using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSpawner : MonoBehaviour {
    public Transform trigger;

    private ObjectPooler objectPooler;

    public void SpawnBoulder(GameObject platform)
    {
        GameObject boulder = objectPooler.GetPooledObject("Boulder");

        float boulderX = platform.transform.position.x + platform.GetComponent<ObjectDestroyer>().offsetX;
        float boulderY = platform.transform.position.y + 1.6f;

        boulder.transform.position = new Vector2(boulderX, boulderY);
        boulder.transform.rotation = platform.transform.rotation;

        boulder.SetActive(true);
    }

    public Transform SpawnPlatform(float x, float y, float distanceBetween = 10f)
    {
        GameObject ceiling = objectPooler.GetPooledObject("Ceiling");

        ceiling.transform.position = new Vector2(x, y);

        SpawnBoulder(ceiling);

        ceiling.SetActive(true);
        transform.position = new Vector3(transform.position.x + distanceBetween, transform.position.y, transform.position.z);
        return ceiling.transform;
    }



    // Use this for initialization
    void Start () {
        objectPooler = ObjectPooler.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x <= trigger.position.x)
        {
            float ceilingX = transform.position.x;
            float ceilingY = transform.position.y + Random.Range(0f, 1f);
            float ceilingDistance = 19.8f * Random.Range(1f, 2f);
            SpawnPlatform(ceilingX, ceilingY, ceilingDistance);

        }
    }
}
