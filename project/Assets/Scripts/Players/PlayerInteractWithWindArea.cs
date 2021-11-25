using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractWithWindArea : MonoBehaviour
{
    Player player;
    PlayerMovement movement;
    Rigidbody2D rigid;
    [Header("进入风场的速度控制")]
    public float maxDownSpeed;
    public float maxUpSpeed;
    private void Start() 
    {
        player = GetComponent<Player>();
        movement = GetComponent<PlayerMovement>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        WindArea area = other.GetComponent<WindArea>();
        if (area != null)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Clamp(rigid.velocity.y, maxDownSpeed, maxUpSpeed));
            movement.SetWindAreaInfluence();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        WindArea area = other.GetComponent<WindArea>();
        if (area != null && player.CanOperate)
        {
            movement.SetWindAreaInfluence();
        }
        else
        {
            movement.RemoveWindAreaInfluence();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        WindArea area = other.GetComponent<WindArea>();
        if (area != null)
        {
            movement.RemoveWindAreaInfluence();
        }
    }
}
