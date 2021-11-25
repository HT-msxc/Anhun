using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttackState : TripleAttackState
{
    private void OnEnable()
    {
        TripleAttackNum = 1;
        enableTripleAttack = true;
        TripleAttackTimeCount = 0;
        rangePoints3 = boss2.rangePoints3;
        isFinishState = false;
    }
    public override void RunState()
    {
        TripleAttack(1);
    }
    private void OnDisable()
    {
        for (int i = 1; i < 3; i++)
        {
            swords[i].gameObject.SetActive(true);
        }
    }
}
