using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]public GameObject circle;
    [SerializeField]public GameObject ring;
    [SerializeField]public GameObject cross;
    [SerializeField]public float ratio = 0.58f;
    [SerializeField] float wraningTime;
    [SerializeField]public float attackTime = 1.5f;
    [SerializeField] public GameObject Light;
    [SerializeField] GameObject AttackEffect;
    protected int m_rangeAttackMode;
    float attackTimeCount;
    public bool enableAttack;
    bool enableWraning;
    bool attackRunning;
    bool finishAttack;
    int attackRange;
    protected bool finishFightKnife;
    protected bool isTorchPlayer;
    private void Start()
    {
        StartAttack();
    }
    private void Update()
    {
        //Attack();
        if (!GetComponentInParent<FightKnifeState>().isActiveAndEnabled)
        {
            isTorchPlayer = false;
        }
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ring.transform.localScale.x * ratio * ring.GetComponentInChildren<CircleCollider2D>().radius);
    }

    public virtual void StartAttack()
    {
        enableAttack = true;
    }
    public bool Attack(int rangeAttackMode)
    {
        m_rangeAttackMode = rangeAttackMode;
        if (enableAttack)
        {
            finishAttack = false;
            enableAttack = false;
            enableWraning = true;
            attackRunning = true;
            attackRange = rangeAttackMode;
        }
        if (attackRunning)
        {
            switch (attackRange)
            {
                case 0:
                    return CircleAttack();
                case 1:
                    return RingAttack();
                case 2:
                    return CrossAttack();
                default:
                    Debug.Log("攻击模式数超出");
                    return false;
            }
        }
        return false;
    }

    #region 圆形攻击
    bool CircleAttack()
    {
        return AttackInMode(circle);
    }
    #endregion

    #region 圆环攻击
    bool RingAttack()
    {
        return AttackInMode(ring);
    }
    #endregion

    #region 十字攻击
    bool CrossAttack()
    {
        return AttackInMode(cross);
    }
    #endregion

    bool AttackInMode(GameObject attackArea)
    {
        attackTimeCount += Time.deltaTime;
        if (enableWraning)
        {
            //TODO 。。。。。。播放预警动画
            attackArea.gameObject.SetActive(true);

            //Debug.Log(gameObject.transform.parent.parent + "open");
            enableWraning = false;
        }
        if (attackTimeCount > wraningTime)
        {
            if (!finishAttack)
            {
                finishAttack = true;
                //TODO 。。。。。。播放攻击动画
                if(attackArea = cross)
                {
                    AudioManager.Instance.PlayAudio("激光",AudioType.SoundEffect);
                }
            }
            if (attackTimeCount > attackTime)
            {
                attackArea.SetActive(false);

                attackTimeCount = 0;
                attackRunning = false;
                return true;
            }
        }
        return false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<GoldElement>() && GetComponentInParent<FightKnifeState>().isActiveAndEnabled)
        {
            //完成拼刀
            if (!isTorchPlayer)
            {
                Debug.Log("刀位接触玩家与刀碰撞");
                GetComponentInParent<FightKnifeState>().qTE.SlowDownTime = 0.5f;
                GetComponentInParent<FightKnifeState>().qTE.StartSlowDownTime();
                Instantiate(AttackEffect , transform.position ,new Quaternion());
                AudioManager.Instance.PlayAudio("弹刀",AudioType.SoundEffect);
                finishFightKnife = true;
            }
        }
        if (other.tag == "Player")
        {
            Debug.Log("玩家碰撞" + finishFightKnife);
            isTorchPlayer = true;
            if (!finishFightKnife)
            {
                if(m_rangeAttackMode == 1 && (other.transform.position - transform.position).magnitude < ring.transform.localScale.x * ratio * ring.GetComponentInChildren<CircleCollider2D>().radius)
                {
                Debug.Log("玩家si : mode:" + m_rangeAttackMode+"distence =" +(other.transform.position - transform.position).magnitude + "ringdis =" +ring.transform.localScale.x * ratio * ring.GetComponentInChildren<CircleCollider2D>().radius);
                    return;
                }
                other.GetComponent<IGetHurt>().GetHurt(this.transform);
            }
            else
            {
                finishFightKnife = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        
    }
    
}
