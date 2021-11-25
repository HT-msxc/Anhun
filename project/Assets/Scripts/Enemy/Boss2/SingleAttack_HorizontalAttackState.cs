using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttack_HorizontalAttackState : Boss2StateBase
{
    private void OnEnable() {
        GetComponent<HorizontalAttackState>().enabled = true;
        GetComponent<SingleAttackState>().enabled = true;
        isFinishState = false;
    }

    bool SingleAttack_HorizontalAttack()
    {
        int count = 0;
        if(GetComponent<HorizontalAttackState>().FinishState())
        {
            count ++;
        }
        if(GetComponent<SingleAttackState>().FinishState())
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
        SingleAttack_HorizontalAttack();
    }
}
