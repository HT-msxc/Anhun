using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UITool
{

    /// <summary>
    /// 获取所有的子物体
    /// </summary>
    /// <param name="obj">当前物体</param>
    /// <param name="childName">寻找的组件的名字</param>
    /// <returns></returns>
    public static GameObject FindChildGameObject(this GameObject obj, string childName)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        foreach(Transform it in children)
        {
            if(it.name == childName)
            {
                return it.gameObject;
            }
        }
        Debug.LogFormat("{0} is not find!", childName);
        return null;
    }

    /// <summary>
    /// 获取当前物体的组件
    /// </summary>
    /// <param name="obj">当前物体</param>
    /// <typeparam name="T">组件名</typeparam>
    /// <returns></returns>
    public static T GetComponent<T>(this Transform obj) where T : Component
    {
        T component =null;
        if(obj.gameObject.TryGetComponent<T>(out component))
        {
            return component;
        }
        else
        {
            obj.gameObject.AddComponent<T>();
            return obj.gameObject.GetComponent<T>();
        }
    }

    /// <summary>
    /// 获取当前物体的子物体的组件
    /// </summary>
    /// <param name="obj"> 当前物体</param>
    /// <param name="name">子物体名字</param>
    /// <typeparam name="T">组件名</typeparam>
    /// <returns></returns>
    public static T GetComponentInChilderen<T>(this Transform obj, string name) where T : Component
    {
        GameObject target = FindChildGameObject(obj.gameObject, name);
        if(target == null)
        {
            return null;
        }
        T component = null;
        if(target.TryGetComponent<T>(out component))
        {
            return component;
        }
        else
        {
            target.AddComponent<T>();
            return target.GetComponent<T>();
        }

    }
}
