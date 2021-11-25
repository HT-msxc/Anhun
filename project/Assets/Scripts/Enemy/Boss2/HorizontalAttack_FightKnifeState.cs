using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalAttack_FightKnifeState : Boss2StateBase
{
    [SerializeField] HorizontalAttackState horizontalAttackState;
    [SerializeField] FightKnifeState fightKnifeState;
    private void Awake() {
        horizontalAttackState = GetComponent<HorizontalAttackState>();
        fightKnifeState = GetComponent<FightKnifeState>();
    }
    private void OnEnable() {
        horizontalAttackState.enabled = true;
        fightKnifeState.enabled = true;
        isFinishState = false;
    }
    public override void RunState()
    {
        HorizontalAttack_FightKnife();
    }

    bool HorizontalAttack_FightKnife()
    {
        int count = 0;
        if(horizontalAttackState.FinishState())
        {
            count ++;
        }
        if(fightKnifeState.FinishState())
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
}
