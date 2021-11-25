using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodAltar : AltarBase
{
    private GameObject fujiman;
    [SerializeField] private Transform fujimanCreator;
    [SerializeField] private float size;
    [SerializeField] private int Length;
    [SerializeField] private int currentNode;
    [SerializeField] private float step;
    [SerializeField] private float createCD;
    private float createCDTimeCount;
    [SerializeField] private int windAreaNum = 1;
    [SerializeField] private bool createRunning;
    bool hasFujiman = false;
    protected override void Awake()
    {
        base.Awake();
        thisNatureState = NatureState.Wood;
        fujiman = Resources.Load<GameObject>("Prefab/Fujiman");
    }
    protected override void Update()
    {
        base.Update();
        if (createRunning)
        {
            createCDTimeCount += Time.deltaTime;
            if (createCDTimeCount >= createCD)
            {
                var fjm = ObjectPoolManager.Instence.CreateObject(fujiman, fujimanCreator.position + new Vector3(step * (currentNode + 0.5f)*transform.localScale.x, 0, 0), new Quaternion());
                fjm.transform.GetChild(0).GetComponent<Fujiman>().SetSize(size);
                fjm.transform.GetChild(0).GetComponent<Fujiman>().SetWindAreaNum(windAreaNum);
                fjm.transform.GetChild(0).GetComponent<Fujiman>().SetCDTime(createCD);
                fjm.transform.localScale = new Vector3(transform.localScale.x,fjm.transform.localScale.y,fjm.transform.localScale.z);
                fjm.transform.parent = fujimanCreator.transform;
                currentNode++;
                createCDTimeCount = 0;
                if (currentNode >= Length)
                {
                    currentNode = 0;
                    createRunning = false;
                }
            }
        }
        if (hasFujiman)
        {
            CloseAltar();
        }
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < Length; i++)
        {
            Gizmos.DrawWireCube(fujimanCreator.position + new Vector3(step * (i + 0.5f), 0, 0)*transform.localScale.x, new Vector3(size, size));
            for (int j = 0; j < windAreaNum; j++)
            {
                if (i % 2 == 1)
                {
                    Gizmos.DrawWireCube(fujimanCreator.position + new Vector3((step * (i + 0.5f) - 0.5f*size)*transform.localScale.x, size * (j + 1)*2), new Vector3(size, size) * 2);
                }
            }
        }
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var playerScript = other.GetComponent<Player>();
            if (playerScript == null)
            {
                Debug.Log("not PlayerScript");
                return;
            }
            //playerScript.SetNatureState(NatureState.Water);
            if (playerScript.GetNatureState() == NatureState.Water && !m_animator.GetBool("IsCD"))
            {
                CloseAltar();
                //创建藤曼
                AudioManager.Instance.PlayAudio("藤曼生长（剪掉一半",AudioType.SoundEffect,gameObject);
                if (!hasFujiman)
                {
                    createRunning = true;
                    hasFujiman = true;
                }
                // TODO播放音效
                if(WoodAltar_Fujiman_Wind_Audio.Instance.GetFinishCreateWind())
                {
                    WoodAltar_Fujiman_Wind_Audio.Instance.isFinishCreateWind = false;
                }
            }
        }
    }

    
}
