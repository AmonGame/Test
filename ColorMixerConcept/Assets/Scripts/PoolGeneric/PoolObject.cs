using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class PoolObject<T> : MonoBehaviour where T : Component
{
    public static PoolObject<T> Instance { get; private set; }

    [SerializeField]
    private T pooledPrefab;

    private Queue<T> pooledObjects = new Queue<T>();

    void Awake()
    {
        Instance = this;
    }

    public T Get()
    {
        if (pooledObjects.Count == 0)
            AddObjects(1);

        var obj = pooledObjects.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public T Get(Vector3 position, Transform parent)
    {
        if (pooledObjects.Count == 0)
            AddObjects(1);

        var obj = pooledObjects.Dequeue();
        obj.gameObject.SetActive(true);
        obj.gameObject.transform.position = position;
        obj.transform.parent = parent;
        return obj;
    }

    public T Get(Vector3 position, Transform parent, Quaternion rotation)
    {
        if (pooledObjects.Count == 0)
            AddObjects(1);

        var obj = pooledObjects.Dequeue();
        obj.gameObject.SetActive(true);
        obj.gameObject.transform.position = position;
        obj.gameObject.transform.rotation = rotation;
        obj.transform.parent = parent;
        return obj;
    }

    public T Get(Vector3 position, Transform parent, string name)
    {
        if (pooledObjects.Count == 0)
            AddObjects(1);

        var obj = pooledObjects.Dequeue();
        obj.gameObject.SetActive(true);
        obj.gameObject.transform.position = position;
        obj.gameObject.name = name;
        obj.transform.parent = parent;
        return obj;
    }

    public void ReturnToPool(T objectReturn)
    {
        objectReturn.gameObject.SetActive(false);
        pooledObjects.Enqueue(objectReturn);
    }
    private void AddObjects(int count)
    {
        var newObject = Instantiate(pooledPrefab);
        newObject.gameObject.SetActive(false);
        pooledObjects.Enqueue(newObject);
    }
}

