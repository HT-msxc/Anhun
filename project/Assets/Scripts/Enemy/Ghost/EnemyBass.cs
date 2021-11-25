using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour , IGetHurt
{
    [SerializeField]protected Transform[] targets = new Transform[2];
    protected Vector3 currentTarget;
    [SerializeField]protected float speed = 1;
    [SerializeField]protected float patrolTime = 1;
    protected float timeCount;
    [SerializeField]protected bool isDead;
    
    private void Start() {
        currentTarget = targets[0].position;
    }
    protected void Patrol()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= patrolTime)
        {
            if ((transform.position - targets[0].position).magnitude >= (transform.position - targets[1].position).magnitude)
            {
                currentTarget = targets[0].position;
            }
            else
            {
                currentTarget = targets[1].position;
            }
            timeCount = 0;
        }
        if(!isDead)
        {
            Move(speed);
        }
    }
    protected void Move(float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentTarget.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<IGetHurt>().GetHurt(transform);
        }
    }

    public virtual void GetHurt(Transform attacker)
    {
        //TODO 。。。。。。播放死亡动画
        GetComponent<Animator>().Play("Die");
        AudioManager.Instance.PlayAudio("怪物死亡",AudioType.SoundEffect,gameObject);
        GetComponent<Collider2D>().enabled = false;
        isDead = true;
        for(int i=0;i<2;i ++)
        {
            GameObject piece = ObjectPoolManager.Instence.CreateObject(Resources.Load<GameObject>("Prefab/SoulPiece"), transform.position, transform.rotation);
            piece.transform.SetParent(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteractionWithSoulPiece>().soulPiecesCage);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteractionWithSoulPiece>().AddSoulPieceObserverToInside(piece);
            piece.GetComponent<SoulPiece>().soulPieceStateMachine.TransitionTo(3);
        }
        Invoke(nameof(Dead), 1);
    }

    protected void Dead()
    {
        Destroy(this.gameObject);
    }
}
