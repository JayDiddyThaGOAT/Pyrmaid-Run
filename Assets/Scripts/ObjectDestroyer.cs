using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour {
    public bool destroyByTimer = false;
    public float destroyTime = 1.0f;
    private float destroyTimeCounter;
    private Transform destroyTrigger;

    private ObjectPooler objectPooler;

    public float offsetX = 0f;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        destroyTimeCounter = destroyTime;
        destroyTrigger = GameObject.Find("Object Destroyer").transform;
    }

    public void TriggerDestroy()
    {
        if (objectPooler.FindItem(tag))
        {
            gameObject.SetActive(false);
        }
        else
            Destroy(gameObject);
    }

    public void Display(bool activated)
    {
        gameObject.SetActive(activated);
    }

    private void Update()
    {
        if (transform.position.x + offsetX <= destroyTrigger.position.x)
        {
            TriggerDestroy();
        }

        if (destroyByTimer)
        {
            if (destroyTimeCounter > 0f)
                destroyTimeCounter -= Time.deltaTime;
            else
            {
                destroyTimeCounter = destroyTime;
                TriggerDestroy();
            }
        }

    }
}
