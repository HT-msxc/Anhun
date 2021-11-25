using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChange : MonoBehaviour
{
    Player player;
    Animator animator;
    private void Awake() 
    {
        player = GetComponent<Player>();
        player.onPlayerStateChange += SetChangeAnimation;
        animator = GetComponent<Animator>();
    }

    void SetChangeAnimation(NatureState beforeState, PlayerSize beforeSize)
    {
        string key = null;
        PlayerSize size = player.GetPlayerSize();
        NatureState nature = player.GetNatureState();
        if (beforeState != nature)
        {
            key = size.ToString() + "_" + nature.ToString() + "_Idle";
        }
        else if (beforeSize != size)
        {
            key = beforeSize.ToString() + "To" + size.ToString() + "_" + nature.ToString();
        }
        if (key != null)
            animator.Play(key);
    }
}