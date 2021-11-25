using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : Boss2StateBase
{
    public Boss2 boss2;
    Transform[] swords;
    [Header("RangeAttack")]
    [SerializeField] int rangeAttackMode = 0;

    private void Awake() {
        boss2 = GetComponent<Boss2>();
        swords = boss2.swords;
    }

    private void OnEnable() {
        rangeAttackMode += UnityEngine.Random.Range(1, 3);
        rangeAttackMode = rangeAttackMode%3;
        isFinishState = false;
        for (int i = 1; i < 3; i++)
        {
            swords[i].GetComponent<Sword>().StartAttack();
        }
        swords[0].parent =boss2.bossClones[0];
        boss2.bossClones[0].position = GetComponent<WaitState>().waitPosition;
        boss2.bossClones[0].GetComponent<Animator>().Play("Boss2Teleport");
    }

    
    #region RangeAttack
    bool RangeAttack()
    {
        for (int i = 1; i < 3; i++)
        {
            swords[i].gameObject.SetActive(true);
            if (swords[i].GetComponent<Sword>().Attack(rangeAttackMode) && i == 2)
            {
                //InitAttack();
                isFinishState = true;
                return true;
            }
        }
        return false;
    }

    #endregion

    public override void RunState()
    {
        RangeAttack();
    }
}
