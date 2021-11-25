using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLevel3 : MonoBehaviour
{
    bool playerIn = false;
    bool splitStart =false;
    bool at = false;
    int count = 0;
    public float MoveSpeed = 0.25f;
    public string NextLevelName;
    public GameObject player;
    public GameObject EndPositon;
    public GameObject tree;
    private void Update()
    {
        if(playerIn)
        {
            player.GetComponent<Player>().CanOperate = false;
            if(player.transform.position.x <= EndPositon.transform.position.x && !at)
            {
                player.transform.position = new Vector2(player.transform.position.x + MoveSpeed * Time.deltaTime, player.transform.position.y);
            }
            else
            {
                at = true;
            }
        }
        if(at && !splitStart)
        {
            count++;
            tree.GetComponent<SplitSprite>().Split();
            Destroy(tree.gameObject);
            splitStart = true;
            if(splitStart)
            {
                Invoke("ToNext",1f);
            }
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

    void ToNext()
    {
        SceneLoadManager.Instence.LoadSceneName = NextLevelName;
        UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
    }
}
