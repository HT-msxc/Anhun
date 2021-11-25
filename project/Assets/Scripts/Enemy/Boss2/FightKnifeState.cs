using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightKnifeState : Boss2StateBase
{
    public Boss2 boss2;
    Transform[] bossClones;
    Transform player;
    [Header("FightKnife")]
    [SerializeField] float FightKnifePreTime = 1.5f;
    float FightKnifeTimeCount;
    bool finishFightKnifeMove;
    [SerializeField] public QTE qTE;

    private void OnEnable() {
        player = GameManager.Instence.CurrentPlayer.transform;
        finishFightKnifeMove = false;
        FightKnifeTimeCount = 0;
        isFinishState = false;
        boss2.swords[0].SetParent(bossClones[0]);
        boss2.swords[0].localPosition = new Vector3(2,0);
        bossClones[0].gameObject.SetActive(false);
        bossClones[0].gameObject.SetActive(true);
    }

    private void Awake() {
        boss2 = GetComponent<Boss2>();
        bossClones = boss2.bossClones;
    }

    #region FightKnife
    public bool FightKnife()
    {
        if(player == null)
        {
            player = GameManager.Instence.CurrentPlayer.transform;
        }
        FightKnifeTimeCount += Time.deltaTime;
        if (!finishFightKnifeMove)
        {
            bossClones[0].position = player.position + new Vector3(5, 5);
            //TODO。。。。。。播放boss瞬移动画
            bossClones[0].GetComponent<Animator>().Play("Boss2Teleport");
            
            boss2.swords[0].GetComponent<Afterimage>().StartAfterImage();
            //player.GetComponent<PlayerBattle>().PlayerIsInFightKnife = true;
            Invoke(nameof(StartFightKnifeAttack) ,FightKnifePreTime);
            finishFightKnifeMove = true;
        }
        player.position = bossClones[0].position - new Vector3(5, 5);
        if (FightKnifeTimeCount > FightKnifePreTime)
        {
            //TODO。。。。。。显示案件提示UI
            boss2.swords[0].GetComponent<Afterimage>().ShowAfterImage();
        }
        if (FightKnifeTimeCount > FightKnifePreTime + qTE.SlowDownTime)
        {
            //player.GetComponent<PlayerBattle>().PlayerIsInFightKnife = false;
            FightKnifeTimeCount = 0;
            isFinishState = true;
            boss2.swords[0].GetComponent<Afterimage>().CloseAfterImage();
            boss2.swords[0].position = bossClones[0].position - new Vector3(0,-30);
            boss2.swords[0].SetParent(boss2.swords[1].parent);
            bossClones[0].gameObject.SetActive(false);
            bossClones[0].gameObject.SetActive(true);
            
            return true;
        }
        return false;
    }
    void StartFightKnifeAttack()
    {
        //TODO。。。。。。播放boss攻击动画
        bossClones[0].GetComponent<Animator>().Play("Boss2FightKnife");
        AudioManager.Instance.PlayAudio("敌将拔剑",AudioType.SoundEffect);
        //TODO。。。。。。时停
        qTE.SlowDownTime = 1;
        qTE.StartSlowDownTime();
    }

    public override void RunState()
    {
        FightKnife();
    }


    #endregion

}
