using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : Boss2StateBase
{
    public Boss2 boss2;
    Transform[] bossClones;
    Transform[] swords;

    [SerializeField] public Vector3 waitPosition;

    [Header("SwordRotate")]
    [SerializeField] float rotareSize = 4;
    [SerializeField] float rotateSwordTimeStep = 0.2f;
    Vector3[,] RotateSwordPoint = new Vector3[12, 3];
    public float rotateSwordNum = 3;
    float rotateSwordTimeCount;
    int rotateSwordCurrentPoint;

    public float waitTime;
    float waitTimeCount;

    private void OnEnable()
    {
        rotateSwordTimeCount = 0;
        waitTimeCount = 0;
        isFinishState = false;
        bossClones[0].position = waitPosition;
        for (int i = 0; i < 3; i++)
        {
            swords[i].GetComponent<Collider2D>().enabled = false;
        }
    }

    private void Awake()
    {
        boss2 = GetComponent<Boss2>();
        bossClones = boss2.bossClones;
        swords = boss2.swords;
    }


    #region Wait
    bool Wait(float waitTime)
    {
        waitTimeCount += Time.deltaTime;
        RotateSword(bossClones[0].position, swords);
        if (waitTimeCount > waitTime)
        {
            waitTimeCount = 0;
            isFinishState = true;
            for (int i = 0; i < 3; i++)
            {
                swords[i].GetComponent<Collider2D>().enabled = true;
            }
            return true;
        }
        return false;
    }
    #endregion

    #region RotateSword
    void RotateSword(Vector3 bossPosition, Transform[] swords)
    {
        var line = new Vector3[12];
        var p = new Vector3[4];
        p[0] = new Vector3(bossPosition.x + rotareSize, bossPosition.y, 0);
        p[1] = new Vector3(bossPosition.x, bossPosition.y + 0.5f * rotareSize, 1);
        p[2] = new Vector3(bossPosition.x - rotareSize, bossPosition.y, 0);
        p[3] = new Vector3(bossPosition.x, bossPosition.y - 0.5f * rotareSize, -1);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 2; j >= 0; j--)
            {
                int ii = i - 1;
                if (ii < 0) ii = 3;
                line[i * 3 + j] = (p[i] - p[ii]) * (j + 1) / 3 + p[ii];
            }
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (j - 4 * i >= 0)
                {
                    RotateSwordPoint[j, i] = line[j - 4 * i];
                }
                else
                {
                    RotateSwordPoint[j, i] = line[j + 12 - 4 * i];
                }
            }
        }
        rotateSwordTimeCount += Time.deltaTime;
        if (rotateSwordTimeCount > rotateSwordTimeStep)
        {
            rotateSwordCurrentPoint += 1;
            if (rotateSwordCurrentPoint >= 12)
                rotateSwordCurrentPoint = 0;
            rotateSwordTimeCount = 0;
        }
        var step = ((p[0] - p[1]).magnitude / 3) / (rotateSwordTimeStep / Time.deltaTime);
        for (int i = 0; i < rotateSwordNum; i++)
        {

            //Debug.Log(rotateSwordCurrentPoint);
            var distance = (swords[i].position - RotateSwordPoint[rotateSwordCurrentPoint, i]).magnitude;
            swords[i].position = Vector3.MoveTowards(swords[i].position, RotateSwordPoint[rotateSwordCurrentPoint, i], step * distance);
            swords[i].rotation = new Quaternion();
        }
    }

    #endregion


    public override void RunState()
    {
        Wait(waitTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(waitPosition, 4);
    }
}
