using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炮弹控制
/// </summary>
public class Cannonball : MonoBehaviour ,IGetHurt
{
    [SerializeField] private Vector2 moveDirection ;
    public Vector2 MoveDirection{ get => moveDirection ;set => moveDirection = value;}
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] public float liveTime = 5;
    private float countTime;
    private void Awake() {
        ObjectPoolManager.Instence.ReleaseObject(this.gameObject);
    }
    protected void Update()
    {
        countTime += Time.deltaTime;
        if (countTime >= liveTime)
        {
            ObjectPoolManager.Instence.ReleaseObject(this.gameObject);
            countTime = 0;
        }
        if(moveDirection != Vector2.zero)
        {
            Move();
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position ,(Vector2)transform.position + moveDirection ,moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            ObjectPoolManager.Instence.ReleaseObject(this.gameObject);
            other.GetComponent<IGetHurt>().GetHurt(transform);
        }
    }

    public void GetHurt(Transform attacker)
    {
        ObjectPoolManager.Instence.ReleaseObject(this.gameObject);
    }
}
