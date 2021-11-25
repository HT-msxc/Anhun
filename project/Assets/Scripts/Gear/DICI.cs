using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DICI : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<IGetHurt>().GetHurt(this.transform);
        }
    }
}
