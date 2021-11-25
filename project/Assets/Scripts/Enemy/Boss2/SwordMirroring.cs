using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMirroring : Sword
{
    [SerializeField]Sword mirroring;
    [SerializeField]InsertSwordState insertSwordState;
    [SerializeField]public GameObject circleMirroring;
    [SerializeField]public GameObject ringMirroring;
    [SerializeField]public GameObject crossMirroring;
    private void Awake() {
        ratio = mirroring.ratio;
        attackTime = mirroring.attackTime;
        circleMirroring.transform.position = transform.position - new Vector3(0,insertSwordState.groundThickness);
        ringMirroring.transform.position = transform.position - new Vector3(0,insertSwordState.groundThickness);
        crossMirroring.transform.position = transform.position - new Vector3(0,insertSwordState.groundThickness);
    }
    protected override void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(mirroring.transform.position, ringMirroring.transform.localScale.x * ratio * ringMirroring.GetComponentInChildren<CircleCollider2D>().radius);
    }
    
    public override void StartAttack()
    {
        enableAttack = true;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<GoldElement>() && GetComponentInParent<FightKnifeState>().isActiveAndEnabled)
        {
            //完成拼刀
            if (!isTorchPlayer)
            {
                Debug.Log("刀位接触玩家与刀碰撞");
                GetComponentInParent<FightKnifeState>().qTE.StartSlowDownTime();
                finishFightKnife = true;
            }
        }
        if (other.tag == "Player")
        {
            Debug.Log("玩家碰撞" + finishFightKnife);
            isTorchPlayer = true;
            if (!finishFightKnife)
            {
                if(m_rangeAttackMode == 1 && (other.transform.position - mirroring.transform.position).magnitude < ringMirroring.transform.localScale.x * ratio * ringMirroring.GetComponentInChildren<CircleCollider2D>().radius)
                {
                    return;
                }
                other.GetComponent<IGetHurt>().GetHurt(this.transform);
                // Debug.Log(ring.transform.localScale.x );
                // Debug.Log(ratio );
                // Debug.Log(ring.GetComponentInChildren<CircleCollider2D>().radius);
                // Debug.Log("玩家si : mode:" + m_rangeAttackMode+"distence =" +(other.transform.position - transform.position).magnitude + "ringdis =" +ring.transform.localScale.x * ratio * ring.GetComponentInChildren<CircleCollider2D>().radius);
            }
            else
            {
                finishFightKnife = false;
            }
        }
    }
}
