using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6P2 : MonoBehaviour
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

    bool startDialog1 = false;
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
            if(player.transform.position.x <= EndPosition.transform.position.x)
            {
                player.transform.position =new Vector2(player.transform.position.x + MoveSpeed,player.transform.position.y);
            }
            else
            {
                if(!startDialog1)
                {
                    Dialog1.GetComponent<Level6Plot2>().startTrigger = true;
                    startDialog1 = true;
                }
            }
        }
        if(Dialog1.GetComponent<Level6Plot2>().DialogOver && !loadNext)
        {
            SceneLoadManager.Instence.LoadSceneName = NextLevelName;
            UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            player.GetComponent<Player>().CanOperate = true;
            loadNext = true;
        }
    }
}
