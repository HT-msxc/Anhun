using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Trigger : MonoBehaviour
{
    public Boss1 boss1;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            boss1.enabled = true;
            Destroy(this.gameObject);
        }
    }
}
