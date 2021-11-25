using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeDetector : MonoBehaviour
{
    public LayerMask layerMask;
    public bool GetBlockValue()
    {
        return GetComponent<Collider2D>().IsTouchingLayers(layerMask);
    }   
}
