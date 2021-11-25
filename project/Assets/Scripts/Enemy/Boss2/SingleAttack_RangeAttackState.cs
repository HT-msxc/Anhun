using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttack_RangeAttackState : Boss2StateBase
{
    private void OnEnable() {
        GetComponent<SingleAttackState>().enabled = true;
        GetComponent<RangeAttackState>().enabled = true;
        isFinishState = false;
    }
    bool SingleAttack_RangeAttack()
    {
        int count = 0;
        if(GetComponent<SingleAttackState>().FinishState())
        {
            count ++;
        }
        if(GetComponent<RangeAttackState>().FinishState())
        {
            count ++;
        }
        if(count == 2)
        {
            isFinishState = true;
            return true;
        }
        return false;
    }
    public override void RunState()
    {
        SingleAttack_RangeAttack();
    }
}
