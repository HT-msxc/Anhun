using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8P1 : MonoBehaviour
{
    public float MoveSpeed = 0.1f;
    [Header("下一关的名字")]
    public string NextLevelName;

    [Header("结束位置")]
    public GameObject EndPosition;
    [Header("出生点")]
    public PawnPoint pawn;
    [Header("对话1")]
    public GameObject Dialog1;
    bool startDialog1 = false;
    bool loadNext = false;

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
                    Dialog1.GetComponent<Level8Plot1>().startTrigger = true;
                    startDialog1 = true;
                }
            }
        }
        if(Dialog1.GetComponent<Level8Plot1>().DialogOver && !loadNext)
        {
            GameManager.Instence.CurrentPlayer.GetComponent<Player>().CanOperate = true;
            GameManager.Instence.CurrentPlayer.SetActive(true);
            SceneLoadManager.Instence.LoadSceneName = NextLevelName;
            UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            loadNext = true;
        }
    }
}
