using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    public GameObject NPC;
    public GameObject ScenePlayer;
    public GameObject timeLine;
    public GameObject cm1;
    public GameObject cm2;
    public GameObject plot1;
    GameObject player;
    bool playerIn;
    bool over = false;
    bool startdia = false;
    private void Start()
    {
        cm1.SetActive(false);
        cm2.SetActive(false);
        ScenePlayer.SetActive(false);
    }
    private void Update()
    {
        if(playerIn && !startdia)
        {
            player.SetActive(false);
            timeLine.SetActive(true);
            NPC.SetActive(true);
            cm2.SetActive(true);
            cm1.SetActive(true);
            ScenePlayer.SetActive(true);
            startdia = true;
        }
        if(plot1.GetComponent<Level4Plot1>().DialogOver && !over)
        {
            over = true;
            //player.SetActive(true);
            GameManager.Instence.CurrentPlayer.SetActive(true);
            GameManager.Instence.CurrentPlayer.transform.position = ScenePlayer.transform.position;
            GameManager.Instence.CurrentPlayer.GetComponent<Player>().CanOperate = true;
            ScenePlayer.SetActive(false);
            timeLine.SetActive(false);
            cm1.SetActive(false);
            cm2.SetActive(false);
            UIManager.Instence.PopAllUI();
            NPC.SetActive(false);
            //
            GameObject.Destroy(this.gameObject);
            Debug.Log("OVer");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerIn = true;
            player = other.gameObject;
            player.GetComponent<Player>().CanOperate = false;
        }
    }
    public void StartDialog()
    {
        plot1.GetComponent<Level4Plot1>().startTrigger = true;
    }
}
