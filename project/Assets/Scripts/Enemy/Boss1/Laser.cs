using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            other.GetComponent<IGetHurt>().GetHurt(transform);
        }
    }
}
