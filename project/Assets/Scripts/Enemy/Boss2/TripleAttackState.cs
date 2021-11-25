using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleAttackState : Boss2StateBase
{
    public Boss2 boss2;
    Transform[] bossClones;
    protected Transform[] swords;
    [Header("TripleAttack")]
    [SerializeField] protected Transform[] rangePoints3 = new Transform[3];
    [SerializeField] Transform[] AttackPoints = new Transform[8];
    [SerializeField] float TripleAttackSpeed = 1;
    [SerializeField] float TripleAttackPreTime = 1;
    [SerializeField] float TripleAttackTime = 2;
    [SerializeField] protected int TripleAttackNum = 3;
    protected bool enableTripleAttack;
    protected bool finishAttack;
    protected float TripleAttackTimeCount;

    private void OnEnable()
    {
        enableTripleAttack = true;
        TripleAttackTimeCount = 0;
        rangePoints3 = boss2.rangePoints3;
        isFinishState = false;
    }
    private void Awake()
    {
        boss2 = GetComponent<Boss2>();
        bossClones = boss2.bossClones;
        swords = boss2.swords;
    }

    #region TripleAttack
    protected bool TripleAttack(int num)
    {
        TripleAttackNum = num;
        var points = new Transform[8];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = AttackPoints[i];
        }
        if (enableTripleAttack)
        {
            //初始化攻击位置 一次
            swords[0].parent = swords[2].parent;
            var range = new int[3];
            do
            {
                range = new int[3] { UnityEngine.Random.Range(0, 8), UnityEngine.Random.Range(0, 8), UnityEngine.Random.Range(0, 8) };
            } while (range[0] == range[1] || range[1] == range[2] || range[0] == range[2]);

            if (TripleAttackNum == 1) points[range[0]] = rangePoints3[0];
            if (TripleAttackNum == 1 && boss2.statesList[boss2.currtenState.x][boss2.currtenState.y] == Boss2.state.SingleAttack) points[range[0]] = AttackPoints[0];
            //Debug.Log(points[0]);
            for (int i = 0; i < TripleAttackNum; i++)
            {
                bossClones[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < TripleAttackNum; i++)
            {
                bossClones[i].gameObject.SetActive(true);
                bossClones[i].position = points[range[i]].position;
                if (TripleAttackNum == 1 && boss2.statesList[boss2.currtenState.x][boss2.currtenState.y] == Boss2.state.SingleAttack) bossClones[0].position = new Vector3(bossClones[0].position.x,boss2.player.position.y);
                float rotaZ = points[range[i]].eulerAngles.z - 90;
                swords[i].position = bossClones[i].position + new Vector3(Mathf.Cos(rotaZ / 180 * Mathf.PI), Mathf.Sin(rotaZ / 180 * Mathf.PI), 0) * 4;
                swords[i].rotation = Quaternion.Euler(0, 0, points[range[i]].eulerAngles.z);

                swords[i].GetComponent<Afterimage>().StartAfterImage();
                Debug.Log(swords[i].rotation.eulerAngles);
                //bossClones[i].localScale = new Vector3(points[range[i]].localScale.x, 1, 1);
                bossClones[i].GetComponent<Animator>().Play("Boss2Teleport");
            }
            enableTripleAttack = false;
        }
        TripleAttackTimeCount += Time.deltaTime;
        for (int i = 0; i < TripleAttackNum; i++)
        {
            if (bossClones[i].gameObject.activeInHierarchy)
            {
                if (TripleAttackTimeCount > TripleAttackPreTime)
                {
                    if (Assault(i))
                    {
                        //InitAttack();
                        swords[0].GetComponent<Afterimage>().CloseAfterImage();
                        swords[1].GetComponent<Afterimage>().CloseAfterImage();
                        swords[2].GetComponent<Afterimage>().CloseAfterImage();
                        finishAttack = false;
                        isFinishState = true;
                        return true;
                    }
                }
            }
        }
        return false;
    }

    bool Assault(int swordNO_)
    {
        if (!finishAttack)
        {
            for (int i = 0; i < TripleAttackNum; i++)
            {
                AudioManager.Instance.PlayAudio("刀光攻击", AudioType.SoundEffect);
            }
            finishAttack = true;
        }
        if (TripleAttackTimeCount > TripleAttackTime)
        {
            //结束冲刺攻击
            TripleAttackTimeCount = 0;
            return true;
        }
        var rota = swords[swordNO_].rotation.eulerAngles - new Vector3(0, 0, 90);
        bossClones[swordNO_].position = Vector3.MoveTowards(bossClones[swordNO_].position, bossClones[swordNO_].position + new Vector3(Mathf.Cos(rota.z / 180 * Mathf.PI), Mathf.Sin(rota.z / 180 * Mathf.PI), 0), TripleAttackSpeed * Time.deltaTime);
        float rotaZ = swords[swordNO_].rotation.eulerAngles.z - 90;
        swords[swordNO_].position = bossClones[swordNO_].position + new Vector3(Mathf.Cos(rotaZ / 180 * Mathf.PI), Mathf.Sin(rotaZ / 180 * Mathf.PI), 0) * 4;
        swords[swordNO_].GetComponent<Afterimage>().ShowAfterImage();
        return false;
    }


    #endregion


    public override void RunState()
    {
        TripleAttack(3);
    }

    void PlayTripleAttackVideo()
    {
        AudioManager.Instance.PlayAudio("激光", AudioType.SoundEffect);
    }
}
