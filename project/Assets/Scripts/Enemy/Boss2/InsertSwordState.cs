using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertSwordState : Boss2StateBase
{
    public Boss2 boss2;
    Transform[] bossClones;
    Transform[] swords;
    Transform[] swordsMirroring;
    Transform player;
    
    [SerializeField]WaitState waitState;

    [SerializeField]public float groundThickness;

    [Header("InsertSword")]
    [SerializeField] Transform[] InsertSwordPoint = new Transform[3];
    [SerializeField] public float InsertSwordY = 0;
    [SerializeField] float InsertSwordSpeed = 10;
    bool enableInsert;
    bool finishInsert;

    [Header("SwordFly")]
    [SerializeField] float swordFlyTime = 1;
    [SerializeField] float swordFlyCurvature = 2;
    bool enableFly;
    float swordFlyTimeCount;
    Vector3[][] BezierPoints = new Vector3[3][];

    private void Awake()
    {
        boss2 = GetComponent<Boss2>();
        bossClones = boss2.bossClones;
        swords = boss2.swords;
        swordsMirroring = boss2.swordsMirroring;
        for (int i = 0; i < 3; i++)
        {
            BezierPoints[i] = new Vector3[4];
        }
    }
    private void OnEnable() {
        enableFly = true;
        enableInsert = false;
        swordFlyTimeCount = 0;
        swords[0].SetParent(bossClones[0]);
        swords[0].localPosition = new Vector3(2,0);
        swords[0].rotation = new Quaternion();
        bossClones[0].position = GetComponent<WaitState>().waitPosition;
        bossClones[0].GetComponent<Animator>().Play("Boss2Teleport");
        for (int i = 1; i < 3; i++)
            {
                swordsMirroring[i].gameObject.SetActive(true);
            }
        isFinishState = false;
    }

    #region InsertSword
    bool InsertSword()
    {
        if(player == null)
        {
            player = GameManager.Instence.CurrentPlayer.transform;
        }
        if (enableInsert)
        {
            var insertPoints = new Vector3[3];
            for (int i = 1; i < 3; i++)
            {
                insertPoints[i] = new Vector3(InsertSwordPoint[i].position.x, InsertSwordY, 0);
                var targ = Vector3.MoveTowards(swords[i].position, insertPoints[i], InsertSwordSpeed * Time.deltaTime);
                swords[i].rotation = new Quaternion();
                swords[i].position = targ;
                var targ1 = new Vector3(InsertSwordPoint[i].position.x, InsertSwordY, 0) - (targ - new Vector3(InsertSwordPoint[i].position.x, InsertSwordY, 0)) - new Vector3(0, groundThickness);
                swordsMirroring[i].rotation = Quaternion.Euler(0, 0, 180);
                swordsMirroring[i].position = targ1;
            }

            if (swords[2].position == insertPoints[2])
            {
                enableInsert = false;
                swordFlyTimeCount = 0;
                //InitAttack();
                finishInsert = true;
                isFinishState = true;
                AudioManager.Instance.PlayAudio("插刀",AudioType.SoundEffect);
                return true;
            }
            return false;
        }
        else
        {
            SwordFlyTo(InsertSwordPoint);
            if (swordFlyTimeCount > swordFlyTime)
            {
                Debug.Log(1);
                enableInsert = true;
                swordFlyTimeCount = 0;
                enableFly = true;
            }
            return false;
        }
    }
    #endregion

    #region SwordFly
    void SwordFlyTo(Transform[] targets)
    {
        var targetVector = new Vector3[3] { targets[0].position, targets[1].position, targets[2].position };
        SwordFlyTo(targetVector);
    }
    void SwordFlyTo(Vector3[] targets)
    {
        if (enableFly)
        {

            //var flyTime = (transform.position - player.position).magnitude * swordFlyTime;
            for (int i = 1; i < 3; i++)
            {
                BezierPoints[i] = GetBezierPoint(swords[i].position, targets[i]);
            }
            enableFly = false;
            swordFlyTimeCount = 0;
        }
        swordFlyTimeCount += Time.deltaTime;
        if (swordFlyTimeCount > swordFlyTime) return;
        for (int i = 1; i < 3; i++)
        {
            var targ = Bezier_3(BezierPoints[i][0], BezierPoints[i][1], BezierPoints[i][2], BezierPoints[i][3], swordFlyTimeCount / swordFlyTime);
            var targ1 = new Vector3(InsertSwordPoint[i].position.x, InsertSwordY, 0) - (targ - new Vector3(InsertSwordPoint[i].position.x, InsertSwordY, 0)) - new Vector3(0, groundThickness);
            var driction = (targ - swords[i].position).normalized;
            var driction1 = (targ1 - swordsMirroring[i].position).normalized;
            swords[i].rotation = Quaternion.Euler(0, 0, 180 * Mathf.Atan2(driction.y, driction.x) / Mathf.PI + 90);
            swords[i].position = targ;
            swordsMirroring[i].rotation = Quaternion.Euler(0, 0, 180 * Mathf.Atan2(driction1.y, driction1.x) / Mathf.PI + 90);
            swordsMirroring[i].position = targ1;
        }
    }
    Vector3[] GetBezierPoint(Vector3 start, Vector3 end)
    {
        Vector3 center = (transform.position + player.position) * 0.5f;
        Vector3[] points = new Vector3[4];
        points[0] = start;
        if (UnityEngine.Random.Range(-1f, 1f) > 0)
        {
            points[1] = new Vector3(UnityEngine.Random.Range(center.x, player.position.x), UnityEngine.Random.Range(center.y, start.y));
            points[2] = new Vector3(UnityEngine.Random.Range(center.x, start.x), UnityEngine.Random.Range(center.y, player.position.y));
        }
        else
        {
            points[2] = new Vector3(UnityEngine.Random.Range(center.x, player.position.x), UnityEngine.Random.Range(center.y, start.y));
            points[1] = new Vector3(UnityEngine.Random.Range(center.x, start.x), UnityEngine.Random.Range(center.y, player.position.y));
        }
        points[1] = center + (points[1] - center) * swordFlyCurvature;
        points[2] = center + (points[2] - center) * swordFlyCurvature;
        points[3] = end;
        return points;
    }
    public static Vector3 Bezier_3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return (1 - t) * ((1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2)) + t * ((1 - t) * ((1 - t) * p1 + t * p2) + t * ((1 - t) * p2 + t * p3));
    }

    #endregion

    public override void RunState()
    {
        InsertSword();
    }
}
