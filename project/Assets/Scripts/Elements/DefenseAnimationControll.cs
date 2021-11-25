using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseAnimationControll : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Animator animator;
    private void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        animator.enabled = true;
        animator.Play("OpenEffect");
    }

    public void StopAnimation()
    {
        animator.enabled = false;
        spriteRenderer.sprite = null;
    }
}
