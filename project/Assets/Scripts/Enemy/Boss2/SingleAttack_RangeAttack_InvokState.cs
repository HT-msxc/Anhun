using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttack_RangeAttack_InvokState : Boss2StateBase
{
    private void OnEnable() {
        GetComponent<SingleAttackState>().enabled = true;
        GetComponent<RangeAttack_InvokState>().enabled = true;
        isFinishState = false;
    }
    bool SingleAttack_RangeAttack_Invok()
    {
        var count = 0;
        if(GetComponent<SingleAttackState>().FinishState())
        {
            count ++;
        }
        if(GetComponent<RangeAttack_InvokState>().FinishState())
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
        SingleAttack_RangeAttack_Invok();
    }
}
