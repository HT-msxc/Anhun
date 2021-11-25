using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public int state;
    private int attackNum = 2;
    [SerializeField] private Transform player;
    [SerializeField] private float AttackCD = 5;
    [SerializeField] private float closeTime = 4;
    [SerializeField] private float intervalTime = 1;
    List<Vector3> rangePoints = new List<Vector3>();
    private float attackCDCount;
    private Animator m_animator;
    private GameObject backLightLaser;
    private GameObject attackLightLaser;
    List<GameObject> backLightLasers = new List<GameObject>();
    List<GameObject> attackLightLasers = new List<GameObject>();
    bool isStartAttack;
    bool enableWarning;
    bool finishBackLight = false;
    bool finishAttack = false;
    bool finishClose = false;
    private void Awake()
    {
        backLightLaser = Resources.Load<GameObject>("Prefab/backLightLaser");
        attackLightLaser = Resources.Load<GameObject>("Prefab/attackLightLaser");
        enableWarning = true;
    }
    private void OnEnable() {
        StartAttack();
        player = GameManager.Instence.CurrentPlayer.transform;
    }

    private void Update()
    {
        if(player == null)
        {
            player = GameManager.Instence.CurrentPlayer.transform;
        }
        if (!isStartAttack)
            return;
        Attack();
    }
    public void StartAttack()
    {
        isStartAttack = true;
        for (int i = 0; i < attackNum; i++)
        {
            var x = UnityEngine.Random.Range(100, 110);
            var y = UnityEngine.Random.Range(-100, 100);
            float ra = ((int)UnityEngine.Random.Range(0, 2) - 0.5f) * 2;
            if (rangePoints.Count <= i)
            {
                rangePoints.Add(new Vector3(ra * x, y));
            }
            else
            {
                rangePoints[i] = new Vector3(ra * x, y);
            }
        }

        finishBackLight = false;
        finishAttack = false;
        finishClose = false;
    }

    void Attack()
    {

        Debug.Log(123);
        attackCDCount += Time.deltaTime;
        if (!finishBackLight)
        {
            for (int i = 0; i < attackNum; i++)
            {
                var backLight = ObjectPoolManager.Instence.CreateObject(this.backLightLaser, transform.position, new Quaternion());
                backLight.transform.parent = transform;
                backLightLasers.Add(backLight);
                backLight.transform.rotation = Quaternion.Euler(0, 0, 180 * Mathf.Atan2(-rangePoints[i].y, -rangePoints[i].x) / Mathf.PI);//(player.transform.position - this.transform.position).x) / Mathf.PI);

            }

            finishBackLight = true;
        }
        if (attackCDCount > intervalTime && !finishAttack)
        {
            for (int i = 0; i < attackNum; i++)
            {
                var attackLight = ObjectPoolManager.Instence.CreateObject(attackLightLaser, (transform.position - rangePoints[i]), new Quaternion());
                attackLightLasers.Add(attackLight);
                attackLight.transform.rotation = Quaternion.Euler(0, 0, 180 * Mathf.Atan2((player.transform.position - (transform.position - rangePoints[i])).y, (player.transform.position - (transform.position - rangePoints[i])).x) / Mathf.PI);
            }
            finishAttack = true;
        }
        if (attackCDCount > closeTime && !finishClose)
        {
            for (int i = 0; i < attackNum; i++)
            {
                ObjectPoolManager.Instence.ReleaseObject(attackLightLasers[i]);
                // attackLightLasers.Remove(attackLightLasers[i]);
                ObjectPoolManager.Instence.ReleaseObject(backLightLasers[i]);
                // attackLightLasers.Remove(backLightLasers[i]);
            }
            finishClose = true;
        }
        if (attackCDCount > AttackCD)
        {
            attackCDCount = 0;
            StartAttack();
        }
    }
}
