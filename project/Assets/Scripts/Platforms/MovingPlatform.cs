using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("左右移动")]
    //public bool LeftRightORTopBottom;
    float Speed = 10f;
    public float LeftPosition =5.0f;
    public float RightPosition = 5.0f;
    public float PauseTime = 2.0f;
    bool _isMoveToPosition1 = true;
    bool _isMoveToPosition2 = false;
    bool atPos1 = false;
    bool atPos2 = false;
    float _target1;
    float _target2;
    public float _moveSpeed;
    bool _isPlayerIn = false;
    float timer1;
    float timer2;
    //float tempSpeed;
    Vector3  _position;
    Animator _animator;
    PlayerMovement playerMovement = null;
    private void Awake()
    {
        //gameObject.tag = "MovingPlatform";
    }
    void Start()
    {
        //tempSpeed = PlayerMoveSpeedOfPlatform;
        _target1 = this.transform.position.x - LeftPosition;
        _target2 = this.transform.position.x + RightPosition;
        _position.x = this.transform.position.x;
        _position.y= this.transform.position.y;
        _animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if(_isMoveToPosition1)
        {
            MoveToPosition1();
            if(_position.x <= _target1)
            {
                timer1 = 0;
                _isMoveToPosition1 = false;
                atPos1 = true;
            }
        }
        if(atPos1)
        {
            //PlayerMoveSpeedOfPlatform = 0;
            timer1 += Time.deltaTime;
            if(timer1 >= PauseTime)
            {
                _isMoveToPosition2 = true;
                atPos1 = false;
                //PlayerMoveSpeedOfPlatform = tempSpeed;
            }
        }

        if(_isMoveToPosition2)
        {
            MoveToPosition2();
            if(_position.x >= _target2)
            {
                timer2 = 0;
                _isMoveToPosition2 = false;
                atPos2 = true;
            }
        }
        if(atPos2)
        {
            //PlayerMoveSpeedOfPlatform = 0;
            timer2 += Time.deltaTime;
            if(timer2 >= PauseTime)
            {
                _isMoveToPosition1 = true;
                atPos2 = false;
                //PlayerMoveSpeedOfPlatform = tempSpeed;
            }
        }

        if(playerMovement != null)
        {
            if(_isPlayerIn)
            {
                //playerMovement.SetPlatformSpeed(_moveSpeed * PlayerMoveSpeedOfPlatform);
            }
        }
    }
    //默认开始从起点向左边pos1移动

    //FIXME: 动画的方向 反了。。。。。。。
    void MoveToPosition1()
    {
        _moveSpeed =  -Speed;
        _animator.SetBool("ToLeft", true);
        _position.x -= Speed  * Time.deltaTime;
        this.transform.position = _position;
        if (_isPlayerIn)
        playerMovement.transform.position = new Vector3(playerMovement.transform.position.x - Speed * Time.deltaTime, playerMovement.transform.position.y, playerMovement.transform.position.z);
    }

    //从pos1 向 pos2 移动
    void MoveToPosition2()
    {
        _animator.SetBool("ToLeft", false);
        _moveSpeed = Speed;
        _position.x += Speed * Time.deltaTime;
        this.transform.position = _position;
        if (_isPlayerIn)
        playerMovement.transform.position = new Vector3(playerMovement.transform.position.x + Speed * Time.deltaTime, playerMovement.transform.position.y, playerMovement.transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player in");
            playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            _isPlayerIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _isPlayerIn = false;
            //playerMovement.SetPlatformSpeed(0);
        }
    }
}
