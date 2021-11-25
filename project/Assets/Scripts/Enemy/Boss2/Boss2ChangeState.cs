using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2ChangeState : Boss2StateBase
{
    public Transform player;
    float runTime = 5;
    float runTimeCount;
    private void OnEnable() {
        player = GameManager.Instence.CurrentPlayer.transform;
        GetComponent<Boss2>().bossClones[0].position = GetComponent<WaitState>().waitPosition;
        GetComponent<Boss2>().bossClones[0].GetComponent<Animator>().Play("Boss2State2");
        GetComponent<Boss2>().bossClones[0].GetComponent<Animator>().SetBool("ChangeState",true);
        GetComponent<Boss2>().swordsMirroring[1].gameObject.SetActive(false);
        GetComponent<Boss2>().swordsMirroring[2].gameObject.SetActive(false);
    }
    public override void RunState()
    {
        if(player == null)
        {
            player = GameManager.Instence.CurrentPlayer.transform;
        }
        runTimeCount += Time.deltaTime;
        player.GetComponent<Player>().CanOperate = false;
        if(runTimeCount > runTime)
        {
            
        Debug.Log(123);
            Finish();
        }
    }

    void Finish()
    {
        this.enabled = false;
        isFinishState = true;
        player.GetComponent<Player>().CanOperate = true;
    }
}
