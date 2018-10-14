using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public Transform objectPool;
    public bool shouldExpand = true;
}

public class ObjectPooler : MonoBehaviour {

    #region Singleton
    public static ObjectPooler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;

    // Use this for initialization
    private void Start ()
    {
        pooledObjects = new List<GameObject>();
        foreach(ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate<GameObject>(item.objectToPool, item.objectPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
	}

    public bool FindItem(string tag)
    {
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
                return true;
        }

        return false;
    }

    public int CountItems(string tag)
    {
        int count = 0;

        foreach (GameObject item in pooledObjects)
        {
            if (item.tag == tag && item.activeSelf)
                count++;
        }

        return count;
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }

        foreach(ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate<GameObject>(item.objectToPool, item.objectPool);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }
}
