using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject PlayerObject;
    Vector3 position;
    private void Awake()
    {
        if(PlayerObject == null)
        {
            throw new System.Exception("Cann't find Player");
        }
    }
    private void Update()
    {
         position = new Vector3(PlayerObject.transform.position.x, PlayerObject.transform.position.y, PlayerObject.transform.position.z);
    }
    private void LateUpdate()
    {
        this.transform.position = position;
    }
}
