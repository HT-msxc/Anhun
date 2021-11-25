using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEffect : MonoBehaviour
{
    public void ReturnToPool()
    {
        ObjectPoolManager.Instence.ReleaseObject(gameObject);
    }
}
