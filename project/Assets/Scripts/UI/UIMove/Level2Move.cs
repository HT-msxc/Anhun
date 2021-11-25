using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Move : MonoBehaviour
{
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
    [Header("对话2")]
    public GameObject Dialog2;

    bool startDialog1 = false;
    bool startDialog2 = false;
    bool loadNext = false;
    private void Start()
    {
        player = GameManager.Instence.CurrentPlayer;
    }

    private void Update()
    {
        if(pawn.ok)
        {
            player.GetComponent<Player>().CanOperate = false;
            if(!startDialog1)
            {
                Dialog1.GetComponent<Level1Plot3Contronller>().startTrigger = true;
                startDialog1 = false;
            }
        }
        if(Dialog1.GetComponent<Level1Plot3Contronller>().DialogOver)
        {
            if(player.transform.position.x <= EndPosition.transform.position.x)
            {
                player.transform.position =new Vector2(player.transform.position.x + MoveSpeed * Time.deltaTime,player.transform.position.y);
            }
            else
            {
                if(!startDialog2)
                {
                    Dialog2.GetComponent<Level1Plot4Contronller>().startTrigger = true;
                    startDialog2 = true;
                }
            }
        }

        if(Dialog2.GetComponent<Level1Plot4Contronller>().DialogOver && !loadNext)
        {
            loadNext = true;
            SceneLoadManager.Instence.LoadSceneName = NextLevelName;
            UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
        }
    }
}
