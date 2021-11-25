using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private void OnEnable() 
    {
        Invoke("ReturnToPool", 1);
    }
    public void ReturnToPool()
    {
        ObjectPoolManager.Instence.ReleaseObject(gameObject);
    }
}
