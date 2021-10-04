using System;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// EDITABLE BY CLIENT
public enum PoolType
{
    NONE = 0,
    
}
// --------------------

public class PoolManager : SceneSingleton<PoolManager>
{

    [SerializeField]
    Pool[] initialPools;
    public Dictionary<PoolType, Pool> pools = new Dictionary<PoolType, Pool>();

    public bool init { get; private set; }
    
    public void Init()
    {
        if (init)
            return;
        
        for (int i = 0; i < initialPools.Length; i++)
            AddPool(initialPools[i]);

        init = true;
    }

    public GameObject GetObject(PoolType poolType, Vector3 position, Quaternion rotation)
    {
        GameObject poolObject = null;
        if (pools.ContainsKey(poolType))
            poolObject = pools[poolType].GetObject(position, rotation);
        
        return poolObject;
    }
    
    public Pool AddPool(Pool pool)
    {
        if (pools.ContainsKey(pool.poolType))
            return pools[pool.poolType];
        
        pools.Add(pool.poolType, pool);
        pool.InitPooledObjects();
        
        return pool;
    }
    
    public Pool AddPool(PoolType poolType, GameObject poolablePrefab, int pooledNumber)
    {
        if (pools.ContainsKey(poolType))
            return pools[poolType];
        
        Pool pool = new Pool(poolType, poolablePrefab, pooledNumber);
        pools.Add(poolType, pool);
        pool.InitPooledObjects();
        
        return pool;
    }

    public bool RemovePool(PoolType poolType)
    {
        if (pools.ContainsKey(poolType))
        {
            pools[poolType].DestroyPooledObjects ();
            pools.Remove(poolType);

            return true;
        }

        return false;
    }

}

[Serializable]
public class Pool
{
    public PoolType poolType;
    public GameObject poolablePrefab;
    public int pooledNumber;
    
    Queue<GameObject> pooledObjects;

    public Pool(PoolType poolType, GameObject poolablePrefab, int pooledNumber)
    {
        this.poolType = poolType;
        this.poolablePrefab = poolablePrefab;
        this.pooledNumber = pooledNumber;
    }

    public void InitPooledObjects()
    {
        pooledObjects = new Queue<GameObject>(pooledNumber);

        AddPooledObjects(pooledNumber);
    }
    
    public void DestroyPooledObjects()
    {
        for (int i = pooledObjects.Count-1; i >= 0; i--)
        {
            GameObject pooledObject = pooledObjects.Dequeue();
            GameObject.Destroy(pooledObject);
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        GameObject pooledObject = null;
        int i = 0;
        do
        {
            i++;
            if (pooledObjects.Count > 0)
                pooledObject = pooledObjects.Dequeue();
            if (pooledObject != null)
                pooledObjects.Enqueue(pooledObject);
        }
        while ((pooledObject == null || pooledObject.activeInHierarchy) && i < pooledNumber);
        
        if (pooledObject == null || pooledObject.activeInHierarchy)
            pooledObject = AddPooledObjects()[0];

        pooledObject.transform.position = position;
        pooledObject.transform.rotation = rotation;
        pooledObject.GetComponent<IPoolable>()?.PoolEnable();
        pooledObject.SetActive(true);
        
        return pooledObject;
    }

    public GameObject[] AddPooledObjects(int numberToAdd = 1)
    {
        if (numberToAdd <= 0)
            return null;

        GameObject[] result = new GameObject[numberToAdd];
        for (int i = 0; i < numberToAdd; i++)
        {
            GameObject pooledObject = GameObject.Instantiate(poolablePrefab);
            result[i] = pooledObject;
            pooledObject.SetActive(false);
            pooledObjects.Enqueue(pooledObject);
        }

        return result;
    }
}

public interface IPoolable
{
    void PoolEnable();
    void PoolDisable();
}