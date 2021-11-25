using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炮台控制
/// </summary>
public class Cannon : MonoBehaviour
{
    [SerializeField] private float CD = 1;
    [SerializeField]Vector2 attackDirection = new Vector2(1 ,1);
    [SerializeField] private float ballSpeed = 1;
    [SerializeField] private float ballLiveTime = 1;
    private float cdTime;
    private GameObject cannonBall;
    private void Start() {
        //初始化子弹池
        cannonBall = Resources.Load<GameObject>("Prefab/Cannonball");
    }
    protected void Update()
    {
        cdTime += Time.deltaTime;
        if (cdTime >= CD)
        {
            Attack();
            cdTime = 0;
        }
    }

    void Attack()
    {
        //生成子弹
        GameObject ball = ObjectPoolManager.Instence.CreateObject(cannonBall ,transform.position ,new Quaternion());
        ball.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
        ball.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        ball.GetComponent<Cannonball>().MoveDirection = attackDirection * transform.localScale.x;
        ball.GetComponent<Cannonball>().moveSpeed = ballSpeed;
        ball.GetComponent<Cannonball>().liveTime = ballLiveTime;
        //TODO 播放音效
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position , transform.position + new Vector3(ballLiveTime * ballSpeed * transform.localScale.x * attackDirection.x, 0 ,0));
    }
}
