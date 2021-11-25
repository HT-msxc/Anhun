using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(GhostShadow))]
public class PlayerMovement : MonoBehaviour
{

    # region 玩家的状态
    Player player;

    #endregion

    [Header("水平移动")]
    public bool canMove;                        //能够水平移动
    public float walkSpeed = 1000;
    public float horizontalValue;
    public float velocityX;
    float movingSizeRate = 1;
    public float icePlatformSpeedEdge = 0.5f;      

    [Tooltip("加减速时间")]
    public float dampingTime = 0.09f;

    [Header("跳跃")]
    PlayerJumpEffect playerJumpEffect;
    public bool canJump;                        //能够跳跃(isOnGround || isInGraceTime)
    public float jumpingSpeed;
    public float fallMultiplier;
    public float upMultiplier;
    public float instantUpMultiplier;
    public float jumpValue;
    public bool isJumping;                      //处于跳跃状态(!isOnGround)
    public float jumpSizeRate;

    [Header("冲刺")]
    public bool canDash;                        //能够冲刺
    public bool isPressingSecondJump;           //按下了第二次跳跃
    public bool isDashing;                      //处于冲刺状态
    public bool hasDashed;                      //已经冲刺
    public float dashTime;
    public float jumpWaitingTime;
    public float dashingSpeed;
    public GhostShadow ghostShadow;

    [Header("地面接触检测")]
    public LayerMask groundLayerMask;
    public Vector2 size;
    public Vector2 pointOffset;
    public bool isOnGround;
    Rigidbody2D rigid;
    Animator animator;
    [Header("土狼检测")]
    public bool isInSky;                    //在空中
    public float skyDetectorEdgeSpeed = 0.5f;
    public float graceTime = 0.5f;
    public bool isInGraceTime;              //处于土狼时间
    public bool hasGraceChance;             //有土狼机会

    [Header("玩家大小变化因数")]
    public float bigMovingRate = 0.75f;
    public float middleMovingRate = 1f;
    public float smallMovingRate = 1.25f;
    public float bigJumpingRate = 3f;
    public float middleJumpingRate = 2.5f;
    public float smallJumpingRate = 2f;             //并不线性地反映跳跃高度

    [Header("风场因素")]
    public bool isInWindArea;
    public float hasWindXMoveMultiplier = 0.5f;
    public float noWindXMoveMultiplier = 1f;
    public float windAreaXMoveMultiplier = 1f;
    public float windAreaYMoveAddedSpeed = 10f;

    public bool IsOnGround
    {
        get=>isOnGround;
    }

    public bool CanMove
    {
        get=>canMove;
        set=>canMove = value;
    }

    public bool CanJump
    {
        get=>canJump;
        set=>canJump = value;
    }
    private void Awake()
    {
        player = GetComponent<Player>();
        player.onPlayerStateChange += SetSizeRate;
    }
    private void Start() 
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        playerJumpEffect = GetComponent<PlayerJumpEffect>();
        SetSizeRate(player.GetNatureState(),player.GetPlayerSize());
        canMove = true;
        ghostShadow = GetComponent<GhostShadow>();
    }
    private void Update() 
    {
        if(hasDashed)
        {
            canDash = false;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            isPressingSecondJump = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isPressingSecondJump = false;
        }
        isInSky = Mathf.Abs(rigid.velocity.y) > skyDetectorEdgeSpeed;
        if (isInSky && !isJumping)
        {
            if (hasGraceChance)
            {
                isInGraceTime = true;
                hasGraceChance = false;
                SetGraceTimeFalse();
            }
            if (!isInGraceTime && !hasDashed)
            {
                canDash = true;
            }
        }
    }
    private void FixedUpdate()
    {
        isOnGround = GroundChecking();
        horizontalValue = Input.GetAxisRaw("Horizontal");
        jumpValue = Input.GetAxisRaw("Jump");
        if (isOnGround && jumpValue == 0)
        {
            isJumping = false;
            hasGraceChance = true;
            hasDashed = false;
            canDash = false;
        }
        canJump = !isJumping && (isOnGround || isInGraceTime);
        if (player.CanOperate)
        {
            //不在冲刺状态，则可以移动或跳跃
            if (!isDashing)
            {
                // 移动
                if (canMove || Mathf.Abs(rigid.velocity.x) < icePlatformSpeedEdge)
                {
                    rigid.velocity = new Vector2(Mathf.SmoothDamp(rigid.velocity.x, windAreaXMoveMultiplier * walkSpeed * Time.fixedDeltaTime * horizontalValue * movingSizeRate,ref velocityX,dampingTime), rigid.velocity.y);
                    if (horizontalValue < 0)
                        transform.localRotation = Quaternion.Euler(0,180,0);
                    if (horizontalValue > 0)
                        transform.localRotation = Quaternion.Euler(0,0,0);
                }
                
                // 不在跳跃，且在地上按下跳跃，则跳跃
                if (!isInWindArea)
                {
                    if (canJump && jumpValue == 1)
                    {
                        rigid.velocity = new Vector2(rigid.velocity.x, jumpingSpeed * jumpSizeRate);
                        isJumping = true;
                        hasGraceChance = false;
                        playerJumpEffect.PlayJumpEffect();
                        AudioManager.Instance.PlayAudio("起跳", AudioType.SoundEffect,gameObject);
                        SetCanDash();
                    }
                }
                else
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y + windAreaYMoveAddedSpeed);
                }
            }
            else
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
            }

            // 若能Dash且没有Dash，此时按下跳跃，不是长按就冲刺
            if (canDash && !isDashing &&  isPressingSecondJump)
            {
                if (horizontalValue == 0)
                {
                    if (transform.localRotation.eulerAngles.y == 180)
                        rigid.velocity = new Vector2(-dashingSpeed,0);
                    else rigid.velocity = new Vector2(dashingSpeed,0);
                }
                else
                {
                    rigid.velocity = new Vector2(dashingSpeed * horizontalValue, 0);
                }
                AudioManager.Instance.PlayAudio("冲刺", AudioType.SoundEffect,gameObject);
                ghostShadow.ShowGhostShadows();
                isDashing = true;
                hasDashed = true;
                SetDashingTick();
            }
            animator.SetFloat("moveSpeed", Math.Abs(rigid.velocity.x));
            animator.SetBool("isOnGround", isOnGround);
        }
        // 加速下降和减缓上升
        if (!isInWindArea && rigid.velocity.y < 0)
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        }
        else if (!isInWindArea && rigid.velocity.y > 0)
        {
            if (jumpValue != 1)
                rigid.velocity += Vector2.up * Physics2D.gravity.y * upMultiplier * Time.fixedDeltaTime;
            else
                rigid.velocity += Vector2.up * Physics2D.gravity.y * instantUpMultiplier * Time.fixedDeltaTime;
        }
        if (!player.CanOperate)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
    }

    bool GroundChecking()
    {
        int value;
        if (transform.localRotation.eulerAngles.y == 0)
            value = 1;
        else
            value = -1;
        Collider2D collider = Physics2D.OverlapBox((Vector2)transform.position + new Vector2(pointOffset.x * value, pointOffset.y),new Vector2(size.x * transform.localScale.x, size.y) , 0, groundLayerMask);
        if (collider != null)
        {
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        int value;
        if (transform.localRotation.eulerAngles.y == 0)
            value = 1;
        else
            value = -1;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + new Vector2(pointOffset.x * value, pointOffset.y)  ,new Vector2(size.x * transform.localScale.x, size.y));
    }

    async void SetCanDash()
    {
        await Task.Delay(TimeSpan.FromSeconds(jumpWaitingTime));
        canDash = true;
    }

    async void SetGraceTimeFalse()
    {
        await Task.Delay(TimeSpan.FromSeconds(graceTime));
        isInGraceTime = false;
    }

    async void SetDashingTick()
    {
        await Task.Delay(TimeSpan.FromSeconds(dashTime / Time.timeScale));
        rigid.velocity = new Vector2(0, 0);
        isDashing = false;
    }

    public void SetSizeRate(NatureState natureState, PlayerSize playerSize)
    {
        PlayerSize size = player.GetPlayerSize();
        switch(size)
        {
            case PlayerSize.Big: movingSizeRate = bigMovingRate; jumpSizeRate = bigJumpingRate;break;
            case PlayerSize.Middle: movingSizeRate = middleMovingRate; jumpSizeRate = middleJumpingRate;break;
            case PlayerSize.Small: movingSizeRate = smallMovingRate; jumpSizeRate = smallJumpingRate;break;
        }
    }

    public void SetWindAreaInfluence()
    {
        windAreaXMoveMultiplier = hasWindXMoveMultiplier;
        isInWindArea = true;
    }

    public void RemoveWindAreaInfluence()
    {
        windAreaXMoveMultiplier = noWindXMoveMultiplier;
        isInWindArea = false;
    }
}