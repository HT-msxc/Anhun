using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PatrolGhost : EnemyBase 
{
    private void Update() {
        if(isDead) return;
        if(currentTarget.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(1,1,1);
        }
        Patrol();
    }

    
}
