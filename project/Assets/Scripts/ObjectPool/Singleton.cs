using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour  where T : Singleton<T>
{
    private static T _instence;

    public static T Instence{get{return _instence;}}
    protected virtual void Awake() {
        if(_instence != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instence = (T)this;
        }
    }

    //判断实例是否实例化了
    public static bool IsInitialized
    {
        get{return _instence != null;}
    }

    protected virtual void OnDestroy() 
    {
        if(_instence == this )
        {
            _instence = null;
        }
    }
}
