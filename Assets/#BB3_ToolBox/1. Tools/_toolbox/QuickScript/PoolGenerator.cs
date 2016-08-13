using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolGenerator : MonoBehaviour {

    public static List<PoolGenerator> Pools = new List<PoolGenerator>();


    public string poolName= "Default";
    public int number= 100;
    public GameObject prefabModel;
    public Transform linkedParent;
    public Pool<GameObject> generatedObject;
    public bool dontDestroyAtLoading;
    public bool hasBeenInitialized;

    public static PoolGenerator GetPool(string name) {
        if (string.IsNullOrEmpty(name)) return null;
        foreach (PoolGenerator pg in Pools)
            if (pg.poolName.Equals(name))
                return pg;
        return null;
    }
    void Awake() {
        GameObject [] allocated  = new GameObject[number];
        for (int i = 0; i < number; i++)
        {
            prefabModel.SetActive(false);
            allocated[i] = GameObject.Instantiate(prefabModel) as GameObject;
            allocated[i].name += "_"+i;
            if (linkedParent != null) allocated[i].transform.parent = linkedParent;
            allocated[i].SetActive(false);
        }
        generatedObject = new Pool<GameObject>(allocated);
        Pools.Add(this);
        if(dontDestroyAtLoading)
                 DontDestroyOnLoad(transform.gameObject);
        hasBeenInitialized = true;
    }

    public GameObject GetNextAvailable() {
        if (!hasBeenInitialized) return null;
        GameObject next = generatedObject.GetNext();
        return next;
    }

    void OnLevelWasLoaded(int level) {

        foreach (GameObject obj in generatedObject.GetAll())
            obj.SetActive(false);

    }

    void OnDestroy() {
        Pools.Remove(this);
    
    }
}


public class Pool<T> where T : new()
{
    private T[] allocatedValue;
    private int cursor;

    public Pool(T[] externalAllocation) {
        allocatedValue = externalAllocation;
    }
    public Pool(int allocated)
    {
        allocatedValue = new T[allocated];
        for (int i = 0; i < allocated; i++)
        {
            allocatedValue[i] = new T();

        }

    }
    public T [] GetAll() {
        return allocatedValue;
    }

    public T GetNext()
    {
        int c = (++cursor) % allocatedValue.Length;
        return allocatedValue[c];

    }
}

