using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkTick : MonoBehaviour
{
    public Animator animator;
    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }
    public void SetSharkNextStageTick()
    {
        animator.SetBool("SetNextStage", true);
    }
    public void CloseSharkNextStageTick()
    {
        GetComponent<Animator>().SetBool("SetNextStage", false);
    }
}
