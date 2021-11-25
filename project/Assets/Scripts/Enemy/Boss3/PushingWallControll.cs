using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingWallControll : MonoBehaviour
{
    public float moveSpeed = 10;
    bool isStartToCatch;
    public Vector3 size;
    private void FixedUpdate() 
    {
        if (isStartToCatch)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.fixedDeltaTime, Space.World);
        }
    }
    public void SetStartTick()
    {
        isStartToCatch = true;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}