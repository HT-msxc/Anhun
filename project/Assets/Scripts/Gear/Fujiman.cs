using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fujiman : MonoBehaviour
{
    [SerializeField]private float size = 1.5f;
    [SerializeField]private float cdTime = 0.1f;
    private GameObject windArea;
    private GameObject fire;
    private Animator m_animator;
    [SerializeField]private int windAreaNum = 1;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }
    private void Update() {
        m_animator.speed = 0.16f/(cdTime*0.5f);
    }
    private void Start()
    {
        windArea = Resources.Load<GameObject>("Prefab/WindArea");
        fire = Resources.Load<GameObject>("Prefab/FujimanFire");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var playerScript = other.GetComponent<Player>();
            if (playerScript == null)
            {
                Debug.Log("not PlayerScript");
                return;
            }
            //playerScript.SetNatureState(NatureState.Fire);
            if (playerScript.GetNatureState() == NatureState.Fire)
            {
                Invoke(nameof(DestoryFujiman), 0.5f);
            }
        }
    }

    public void SetSize(float size)
    {
        this.size = size;
    }
    public void SetWindAreaNum(int Num)
    {
        windAreaNum = Num;
    }
    public void SetCDTime(float cd)
    {
        cdTime = cd;
    }

    void DestoryFujiman()
    {
        for (int i = 0; i < transform.parent.parent.childCount; i++)
        {
            // 创建风场
            if (i % 2 == 1)
            {
                for (int j = 0; j < windAreaNum; j++)
                {
                    ObjectPoolManager.Instence.CreateObject(windArea, transform.parent.parent.GetChild(i).position + new Vector3(0.5f*size, size*(j+1)*2), new Quaternion());
                }
            }
            //播放销毁动画
            ObjectPoolManager.Instence.CreateObject(fire, transform.parent.parent.GetChild(i).position ,new Quaternion());
            ObjectPoolManager.Instence.ReleaseObject(transform.parent.parent.GetChild(i).gameObject);
        }
        WoodAltar_Fujiman_Wind_Audio.Instance.SetFinishCreateWind(true);
    }
}
