using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            other.GetComponent<PlayerBattle>().SetDeath();
        }
    }
}