using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack_InvokState : Boss2StateBase
{
    public Boss2 boss2;
    Transform[] bossClones;
    Transform[] swords;
    Transform[] swordsMirroring;
    [SerializeField] float invokeTime = 3;

    float invokeTimeCount;
    bool invokePerFinish;

    [SerializeField] int rangeAttackMode = 0;

    private void Awake()
    {
        boss2 = GetComponent<Boss2>();
        bossClones = boss2.bossClones;
        swords = boss2.swords;
        swordsMirroring = boss2.swordsMirroring;
    }
    private void OnEnable()
    {
        rangeAttackMode += UnityEngine.Random.Range(1, 3);
        rangeAttackMode = rangeAttackMode%3;
        invokePerFinish = false;
        invokeTimeCount = 0;
        for (int i = 1; i < 3; i++)
        {
            swords[i].GetComponent<Sword>().StartAttack();
            swordsMirroring[i].GetComponent<SwordMirroring>().StartAttack();
        }
        if (GetComponent<SingleAttackState>().enabled)
        {
            bossClones[0].position = GetComponent<WaitState>().waitPosition;
            bossClones[0].GetComponent<Animator>().Play("Boss2Teleport");
        }
        isFinishState = false;
    }

    #region RangeAttack_Invok
    bool RangeAttack_Invok()
    {
        if(rangeAttackMode >= 2)
        {
            rangeAttackMode = 0;
        }
        else
        {
            rangeAttackMode ++;
        }
        invokeTimeCount += Time.deltaTime;
        if (invokeTimeCount > invokeTime)
        {
            for (int i = 1; i < 3; i++)
            {
                swordsMirroring[i].gameObject.SetActive(true);
                if (swordsMirroring[i].GetComponent<SwordMirroring>().Attack(rangeAttackMode) && i == 2)
                {
                    //InitAttack();
                    isFinishState = true;
                    return true;
                }
            }
        }
        if (invokePerFinish) return false;
        for (int i = 1; i < 3; i++)
        {
            swords[i].gameObject.SetActive(true);
            if (swords[i].GetComponent<Sword>().Attack(rangeAttackMode) && i == 2)
            {
                //InitAttack();
                invokePerFinish = true;
            }
        }
        return false;
    }


    #endregion

    public override void RunState()
    {
        RangeAttack_Invok();
    }
}
