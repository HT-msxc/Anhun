using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackGhost : EnemyBase 
{
    [SerializeField]float multiSpeed = 1;
    [SerializeField]private Collider2D playerCollider;
    [SerializeField]private Collider2D PlatfromCollider;

        
    private void Update() {
        if(playerCollider == null)
        {
            GameObject temp =  GameObject.FindWithTag("Player");
            if (temp) playerCollider = temp.GetComponent<Collider2D>();
        }
        if(isDead) return;
        if(currentTarget.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(1,1,1);
        }
        if(PlatfromCollider.IsTouching(playerCollider))
        {
            Attack();
            timeCount = patrolTime;
        }
        else
        {
            Patrol();
        }
    }
    

    void Attack()
    {
        currentTarget = playerCollider.transform.position;
        Move(speed * multiSpeed);
    }

    

}
