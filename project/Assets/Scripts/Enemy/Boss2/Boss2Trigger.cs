using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Trigger : MonoBehaviour
{
    public Boss2 boss2;
    public QTE qTE;
    public Cinemachine.CinemachineVirtualCamera m_camera;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            boss2.enabled = true;
            boss2.bossClones[0].GetComponent<Animator>().enabled = true;
            m_camera.Follow = qTE.transform;
            Destroy(this.gameObject);
        }
    }
}
