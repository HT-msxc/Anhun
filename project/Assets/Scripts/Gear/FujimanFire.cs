using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FujimanFire : MonoBehaviour
{
    void CloseThis()
    {
        ObjectPoolManager.Instence.ReleaseObject(this.gameObject);
    }
}
