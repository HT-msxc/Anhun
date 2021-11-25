using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6MengPo : MonoBehaviour
{
    bool playerIn = false;
    bool at =false;
    bool startDialog1 = true;
    bool over = false;
    public float moveSpeed = 0.04f;
    public string NextSceneName;
    public GameObject point;
    public GameObject plot;
    GameObject player;
    private void Update()
    {
        if (playerIn)
        {
            player.GetComponent<Player>().CanOperate = false;
            if(player.transform.position.x <= point.transform.position.x && !at)
            {
                player.transform.position = new Vector2(player.transform.position.x + moveSpeed , player.transform.position.y);
            }
            else
            {
                at = true;
            }
        }

        if(at && startDialog1)
        {
            plot.GetComponent<Level6Plot1>().startTrigger = true;
            startDialog1 = false;
        }

        if(plot.GetComponent<Level6Plot1>().DialogOver && !over)
        {
            UIManager.Instence.PopAllUI();
            SceneLoadManager.Instence.LoadSceneName = NextSceneName;
            UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            over = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerIn = true;
            player = other.gameObject;
        }
    }
}
