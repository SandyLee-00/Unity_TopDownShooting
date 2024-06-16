using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string prefabName;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.prefabName, objectPool);
        }
    }

    public GameObject SpawnFromPool(string prefabName)
    {
        if (!poolDictionary.ContainsKey(prefabName))
        {
            Debug.LogWarning("Pool with prefabName " + prefabName + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[prefabName].Dequeue();
        poolDictionary[prefabName].Enqueue(objectToSpawn);

        objectToSpawn.SetActive(true);
        return objectToSpawn;
    }
}
