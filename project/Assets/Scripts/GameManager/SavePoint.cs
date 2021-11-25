using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            animator.SetBool("Saving", true);
            GameManager.Instence.SaveGameData();
        }
    }
}
