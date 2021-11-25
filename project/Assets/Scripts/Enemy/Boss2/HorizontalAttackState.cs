using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalAttackState : Boss2StateBase
{
    public Boss2 boss2;
    Transform[] bossClones;
    Transform[] swords;
    [Header("HorizontalAttack")]
    Transform[] rangePoints3 = new Transform[3];
    [SerializeField] float HorizontalAttackTime = 2;
    bool enableHorizontalAttack;
    bool enableHorizontalLight;
    float HorizontalAttackTimeCount;

    private void OnEnable() {
        enableHorizontalAttack = true;
        enableHorizontalLight = false;
        HorizontalAttackTimeCount = 0;
        rangePoints3 = boss2.rangePoints3;
        swords[0].SetParent(bossClones[0]);
        isFinishState = false;
    }

    private void Awake() {
        boss2 = GetComponent<Boss2>();
        bossClones = boss2.bossClones;
        swords = boss2.swords;
    }

    #region HorizontalAttack
    public bool HorizontalAttack()
    {
        var points = rangePoints3;
        if (enableHorizontalAttack)
        {
            bossClones[0].position = GetComponent<WaitState>().waitPosition;
            for (int i = 1; i < 3; i++)
            {
                bossClones[i].gameObject.SetActive(true);
                bossClones[i].position = points[i].position;
                bossClones[i].GetComponent<Animator>().Play("Boss2Teleport");
                swords[i].position = bossClones[i].position + new Vector3(4, 0, 0);
                swords[i].rotation = Quaternion.Euler(0, 0, points[i].rotation.eulerAngles.z);
                //bossClones[i].localScale = new Vector3(points[range[i]].localScale.x, 1, 1);
            }
            enableHorizontalAttack = false;
        }
        HorizontalAttackTimeCount += Time.deltaTime;
        if (!enableHorizontalLight)
        {
            for (int i = 1; i < 3; i++)
            {
                if (boss2.bossClones[i].gameObject.activeInHierarchy)
                {
                    if (HorizontalAttackTimeCount > 1)
                    {
                        //播放激光动画
                        boss2.swords[i].GetComponent<Sword>().Light.SetActive(true);
                        boss2.swords[i].GetComponent<Animator>().Play("HorizontalAttack");
                        Debug.Log(111);
                    }
                }
                if (HorizontalAttackTimeCount > 1 && i == 2)
                {
                    AudioManager.Instance.PlayAudio("激光",AudioType.SoundEffect);
                    enableHorizontalLight = true;
                }
            }
        }
        if (HorizontalAttackTimeCount > HorizontalAttackTime)
        {
            for (int i = 1; i < 3; i++)
            {
                boss2.swords[i].GetComponent<Sword>().Light.SetActive(false);
            }
            //InitAttack();
            isFinishState = true;
            bossClones[1].gameObject.SetActive(false);
            bossClones[2].gameObject.SetActive(false);
            return true;
        }
        return false;
    }


    #endregion


    public override void RunState()
    {
        HorizontalAttack();
    }
}
