using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolFruits : MonoBehaviour
{
    public static PoolFruits Instance { get; private set; }
	
    [SerializeField] private List<Fruits> pooledPrefabs = new List<Fruits>();
    
    private List<Fruits> pooledObjects = new List<Fruits>();

    public List<Fruits> PooledPrefabs { get => pooledPrefabs; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Fruits Get(FruitsType fruitsType)
    {
        var fruct = pooledObjects.Find(x => x.FruitsType == fruitsType);
        if (fruct == null)
            fruct = AddObjects(fruitsType);

        fruct.gameObject.SetActive(true);
        return fruct;
    }

    public Fruits Get(FruitsType fruitsType, Vector3 position, Transform parent)
    {
        var fruct = Get(fruitsType);
        fruct.gameObject.transform.position = position;
        fruct.transform.parent = parent;
        return fruct;
    }

    public void ReturnToPool(Fruits objectReturn)
    {
        objectReturn.gameObject.SetActive(false);
        pooledObjects.Add(objectReturn);
    }
    private Fruits AddObjects(FruitsType fruitsType)
    {
        var fruct = pooledPrefabs.Find(x => x.FruitsType == fruitsType);
        var newObject = Instantiate(fruct);
       // newObject.gameObject.SetActive(false);
        return newObject;
       // pooledObjects.Add(newObject);
    }
}
