using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    public GameObject collectiblePrefab; // The prefab of your collectible object
    public int initialPoolSize = 10;

    private List<GameObject> objectPool = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        InitializeObjectPool();
    }

    private void InitializeObjectPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject collectible = Instantiate(collectiblePrefab);
            collectible.SetActive(false);
            objectPool.Add(collectible);
        }
    }

    public GameObject GetCollectible()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // If the pool is empty, you can create a new object if needed.
        GameObject newCollectible = Instantiate(collectiblePrefab);
        newCollectible.SetActive(true);
        objectPool.Add(newCollectible);
        return newCollectible;
    }

    public void ReturnCollectible(GameObject collectible)
    {
        collectible.SetActive(false);
    }
}