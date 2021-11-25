using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6P3 : MonoBehaviour
{
    public GameObject ScenePlayer;
    public GameObject timeLine;
    public float MoveSpeed = 0.1f;
    [Header("下一关的名字")]
    public string NextLevelName;

    [Header("结束位置")]
    public GameObject EndPosition;
    [Header("出生点")]
    public PawnPoint pawn;
    public GameObject player;
    [Header("对话1")]
    public GameObject Dialog1;
    public GameObject Dialog2;
    public GameObject cm1;
    public GameObject cm2;

    bool startDialog1 = false;
    bool loadNext = false;
    bool EndLevel = false;
    private void Start()
    {
        ScenePlayer.SetActive(false);
        cm1.SetActive(false);
        cm2.SetActive(false);
    }

    private void Update()
    {
        if(pawn.ok)
        {
            GameManager.Instence.CurrentPlayer.GetComponent<Player>().CanOperate = false;
            if(GameManager.Instence.CurrentPlayer.transform.position.x <= EndPosition.transform.position.x)
            {
                GameManager.Instence.CurrentPlayer.transform.position =new Vector2(GameManager.Instence.CurrentPlayer.transform.position.x + MoveSpeed,GameManager.Instence.CurrentPlayer.transform.position.y);
            }
            else
            {
                if(!startDialog1)
                {
                    Dialog1.GetComponent<Level6Plot3>().startTrigger = true;
                    startDialog1 = true;
                }
            }
        }
        if(Dialog1.GetComponent<Level6Plot3>().DialogOver && !loadNext)
        {
            GameManager.Instence.CurrentPlayer.GetComponent<Player>().CanOperate = false;
            GameManager.Instence.CurrentPlayer.SetActive(false);
            ScenePlayer.SetActive(true);
            timeLine.SetActive(true);
            cm1.SetActive(true);
            cm2.SetActive(true);
            loadNext = true;
        }
        if(Dialog2.GetComponent<Level6Plot4>().DialogOver && !EndLevel)
        {
            ToNext();
            EndLevel = true;
        }
    }

    public void StartDialog2()
    {
        Dialog2.GetComponent<Level6Plot4>().startTrigger = true;
    }
    public void ToNext()
    {
            SceneLoadManager.Instence.LoadSceneName = NextLevelName;
            GameManager.Instence.CurrentPlayer.SetActive(true);
            UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
    }
}
