using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPlatform : MonoBehaviour
{
    [Header("加速度")]
    public float AccelerateSpeed = 10f;
    [Header("上下移动的距离")]
    public float TopPosition = 5.0f;
    public float BottomPosition = 5.0f;

    float _speed = 0f;
    //int exitCount = 0;
    float startPos;
    float _toIdlePos;
    float posOffset;
    bool toIdle = true;
    bool playerIn = false;
    GameObject playerPrefab;
    Player player;
    PlayerSize playerSize;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        startPos = this.transform.position.y;

    }
    private void Update()
    {
        if(playerIn)
        {
            toIdle = false;
            if(playerSize == PlayerSize.Big) //acc  move bottom
            {
                animator.SetBool("Accunulate", true);
                StartMoveBottom();
                _toIdlePos = startPos - BottomPosition;
            }
            else if(playerSize == PlayerSize.Middle) //Normal
            {
                animator.SetBool("Normal", true);
            }
            else //split  move  up
            {
                animator.SetBool("Split", true);
                StartMoveTop();
                _toIdlePos = startPos + TopPosition;
            }
        }
        else
        {
            animator.SetBool("Accunulate", false);
            animator.SetBool("Normal", false);
            animator.SetBool("Split", false);
            toIdle = true;
        }
        if(toIdle)
        {
            StartToIdle(_toIdlePos);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            playerSize = player.GetPlayerSize();
            playerPrefab = other.gameObject;
            posOffset = playerPrefab.transform.position.y - transform.position.y - 0.03f;
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerIn = false;
        }
    }

    void StartMoveTop()
    {
        _speed += Time.deltaTime * AccelerateSpeed;
        if(this.transform.position.y <= startPos + TopPosition)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + _speed * _speed, this.transform.position.z);
            playerPrefab.transform.position =new Vector3(playerPrefab.transform.position.x, this.transform.position.y + posOffset, playerPrefab.transform.position.z);
        }
        else
        {
            _speed = 0f;
        }
    }
    void StartMoveBottom()
    {
        _speed += Time.deltaTime * AccelerateSpeed;
        if(this.transform.position.y >= startPos - BottomPosition)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - _speed * _speed, this.transform.position.z);
            playerPrefab.transform.position =new Vector3(playerPrefab.transform.position.x, this.transform.position.y + posOffset, playerPrefab.transform.position.z);
        }
        else
        {
            _speed = 0f;
        }
    }
    void StartToIdle(float currentPos)
    {
        //is at bottom ----need to move up
        if(currentPos <= startPos)
        {
            if(this.transform.position.y <= startPos)
            {
                _speed += Time.deltaTime * AccelerateSpeed;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + _speed, this.transform.position.z);
            }
            else
            {
                toIdle = false;
            }
        }
        else
        {
            //is at top  ---- need to move bottom
            if(this.transform.position.y >= startPos)
            {
                _speed += Time.deltaTime * AccelerateSpeed;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - _speed, this.transform.position.z);
            }
            else
            {
                toIdle = false;
            }
        }
    }
}
