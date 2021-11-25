using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : Boss2StateBase
{
    public Transform player;
    float runTime = 8;
    float runTimeCount;
    private void OnEnable() {
        player = GameManager.Instence.CurrentPlayer.transform;
        GetComponent<Boss2>().bossClones[0].GetComponent<Animator>().Play("Boss2Start");
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
