using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool<T>
{
    private int lastIndex =0;
    private List<ObjectPoolContainer<T>> list;
    private Dictionary<T, ObjectPoolContainer<T>> poolObjectDic;
    private Func<T> factoryFunc;

    public ObjectPool(Func<T> factoryFunc, int initialSize)
    {
        this.factoryFunc = factoryFunc;
        list = new List<ObjectPoolContainer<T>>(initialSize);
        poolObjectDic = new Dictionary<T, ObjectPoolContainer<T>>(initialSize);
        InitPool(initialSize);
    }


    public void InitPool(int capacity)
    {
        for(int i = 0 ; i< capacity ; ++i)
        {
            CreateContainer();
        }
    }

    private ObjectPoolContainer<T> CreateContainer()
    {
        var container = new ObjectPoolContainer<T>();
        container.Item = factoryFunc();
        list.Add(container);
        return container;
    }

    public T GetItem()
    {
        ObjectPoolContainer<T> container = null;

        for(int i = 0; i < list.Count; ++i)
        {
            ++lastIndex;
            if(lastIndex > list.Count -1)
                lastIndex = 0;
            if(list[lastIndex].Used)
                continue;
            else
            {
                container = list[lastIndex];
                break;
            }
        }


        if(container == null)
        {
            container = CreateContainer();
        }

        container.UseThis();
        poolObjectDic.Add(container.Item, container);
        return container.Item;
    }

    public void ReleaseItem(object item)
    {
        ReleaseItem((T)item);
    }

    
    public void ReleaseItem(T item)
    {
        if(poolObjectDic.ContainsKey(item))
        {
            var container = poolObjectDic[item];
            container.Release();
            poolObjectDic.Remove(item);
        }
        else
        {
            Debug.LogWarning("此对象池没有包含提供的对象:" + item);
        }
    }
    public int Count
    {
        get{return list.Count;}
    }

    public int CountOfUsedItem
    {
        get{return poolObjectDic.Count;}
    }
}
