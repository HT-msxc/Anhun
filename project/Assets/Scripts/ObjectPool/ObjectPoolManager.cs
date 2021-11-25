using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public bool logStatus;
    private bool dirty = false;
    public Transform root;
    private Dictionary<GameObject, ObjectPool<GameObject>> prefabDic;
    private Dictionary<GameObject, ObjectPool<GameObject>> instanceDic;

    protected override void Awake()
    {
        base.Awake();
        prefabDic = new Dictionary<GameObject, ObjectPool<GameObject>>();
        instanceDic = new Dictionary<GameObject, ObjectPool<GameObject>>();
    }

    private void Update() {
        if(logStatus && dirty)
        {
            PrintStatus();
            dirty = false;
        }
    }

    public void InitPool(GameObject prefab, int size)
    {
        if(prefabDic.ContainsKey(prefab))
        {
            throw new Exception("Pool for this" + prefab.name + "has been created");
        }

        var pool = new ObjectPool<GameObject>(()=>{return Instantiate(prefab);},size);
        prefabDic[prefab] = pool;
        dirty = true;
    }
    
    public GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if(!prefabDic.ContainsKey(prefab))
        {
            InitPool(prefab,1);
        }
        var pool = prefabDic[prefab];
        var clone = pool.GetItem();
        clone.transform.SetPositionAndRotation(position,rotation);
        clone.SetActive(true);

        instanceDic.Add(clone,pool);
        dirty = true;
        return clone;
    }
    public void ReleaseObject(GameObject clone)
    {
        clone.SetActive(false);
        if(instanceDic.ContainsKey(clone))
        {
            instanceDic[clone].ReleaseItem(clone);
            instanceDic.Remove(clone);
            dirty = true;
        }
        else
        {
            Debug.LogWarning("池子里面没有这个物品:" + clone.name);
        }
    }
    
    private GameObject InstantiatePrefab(GameObject prefab)
    {
        var temp = Instantiate(prefab) as GameObject;
        if(root != null)
        {
            temp.transform.parent = root;
        }
        return temp;
    }
    public void PrintStatus()
    {
        foreach(KeyValuePair<GameObject, ObjectPool<GameObject>> keyVal in prefabDic)
        {
            Debug.Log(string.Format("Object Pool for prefab:{0} In Use: {1} Total {2}",keyVal.Key.name,keyVal.Value.CountOfUsedItem,keyVal.Value.Count));
        }
    }

}
