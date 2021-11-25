using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToNextLevel : MonoBehaviour
{
    public string NextLevelName = null;
    public bool PlayerIn;
    bool startLoad = false;
    GameObject player;
    private void Update()
    {
        if(PlayerIn && !startLoad)
        {
            SceneLoadManager.Instence.LoadSceneName = NextLevelName;
            UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            startLoad = true;
            player.GetComponent<Player>().CanOperate = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerIn = true;
            player = other.gameObject;
        }
    }
}
