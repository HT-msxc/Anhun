using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteGhost : EnemyBase, IGetHurt
{
    [SerializeField] QTE qTE;
    [SerializeField] float multiSpeed = 2;

    bool finishAttack;
    [SerializeField] float attackCD = 1.5f;
    float attackCDTimeCount;
    [SerializeField] float attackDistance = 2;
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Collider2D PlatfromCollider;
    [SerializeField] GameObject AttackEffect;
    bool finishFightKnife;
    bool isTorchPlayer;

    private void Awake()
    {
        finishFightKnife = false;
    }
    private void Update()
    {
        if(playerCollider == null)
        {
            GameObject temp =  GameObject.FindWithTag("Player");
            if (temp) playerCollider = temp.GetComponent<Collider2D>();
        }
        if (isDead) return;
        //怪物按前进方向旋转
        if (currentTarget.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //触发区域检测到玩家，巡逻或追击玩家
        if (PlatfromCollider.IsTouching(playerCollider))
        {
            if (Mathf.Abs((transform.position - playerCollider.transform.position).x) < attackDistance && Attack()) return;
            ChasePlayer();
            //重置巡逻时间
            timeCount = patrolTime;
        }
        else
        {
            if (Mathf.Abs((transform.position - targets[0].position).x) < 1)
            {
                GetComponent<Animator>().SetBool("IsRun", false);
            }
            Patrol();
        }
    }


    void ChasePlayer()
    {
        currentTarget = playerCollider.transform.position;
        if (!isDead)
        {
            GetComponent<Animator>().SetBool("IsRun", true);
            Move(speed * multiSpeed);
        }
    }

    bool Attack()
    {
        if (!finishAttack)
        {
            GetComponent<Animator>().Play("EliteGhostAttack");
            finishAttack = true;
            attackCDTimeCount = 0;
        }
        attackCDTimeCount += Time.deltaTime;
        if (attackCDTimeCount >= attackCD)
        {
            finishAttack = false;
            attackCDTimeCount = 0;
        }
        if (attackCDTimeCount <= 1.3f)
        {
            return true;
        }
        else
        {
        }

        isTorchPlayer = false;
        return false;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<GoldElement>())
        {
            //完成拼刀
            if (!isTorchPlayer && !GetComponent<Collider2D>().isActiveAndEnabled)
            {
                Debug.Log("刀未接触玩家与刀碰撞");
                qTE.SlowDownTime = 0.5f;
                qTE.StartSlowDownTime();
                finishFightKnife = true;
                Invoke(nameof(Die),0.2f);
                Instantiate(AttackEffect, attackCollider.transform.position, new Quaternion());
            }
        }
        if (other.tag == "Player")
        {
            Debug.Log("玩家碰撞");
            isTorchPlayer = true;
            if (!finishFightKnife)
            {
                other.GetComponent<IGetHurt>().GetHurt(this.transform);
                Debug.Log("玩家si");
            }
            else
            {
                finishFightKnife = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
        }
    }

    void StartAttack()
    {
        qTE.SlowDownTime = 0.5f;
        qTE.StartSlowDownTime();
    }

    public override void GetHurt(Transform attacker)
    {
    }

    void Die()
    {
        for(int i=0;i<2;i ++)
        {
            GameObject piece = ObjectPoolManager.Instence.CreateObject(Resources.Load<GameObject>("Prefab/SoulPiece"), transform.position, transform.rotation);
            piece.transform.SetParent(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteractionWithSoulPiece>().soulPiecesCage);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteractionWithSoulPiece>().AddSoulPieceObserverToInside(piece);
            piece.GetComponent<SoulPiece>().soulPieceStateMachine.TransitionTo(3);
        }
        //TODO 。。。。。。播放死亡动画
        GetComponent<Animator>().Play("Die");
        GetComponent<Collider2D>().enabled = false;
        isDead = true;
        Invoke(nameof(Dead), 1.5f);
    }
}
