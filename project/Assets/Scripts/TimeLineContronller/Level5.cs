using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour
{
    bool playerIn;
    bool yingzuAt = false;
    bool startDialog1 = true;
    bool startDialog2 = true;
    bool over = false;
    public string NextLevelName;
    public float speed = 1f;
    public GameObject Yingzu;
    public GameObject plot1;
    public GameObject plot2;
    public GameObject Point1;
    //public GameObject point2;
    GameObject player;
    private void Start()
    {

    }
    private void Update()
    {
        if(playerIn)
        {
            player.GetComponent<Player>().CanOperate = false;
            Yingzu.SetActive(true);
            if(Yingzu.transform.position.x <= Point1.transform.position.x && !yingzuAt)
            {
                Yingzu.transform.position = new Vector2(Yingzu.transform.position.x + speed * Time.deltaTime, Yingzu.transform.position.y);
            }
            else
            {
                yingzuAt = true;
            }
        }

        if(yingzuAt && startDialog1)
        {
            plot1.GetComponent<Level5Plot1>().startTrigger = true;
            startDialog1 = false;
        }
        if(plot1.GetComponent<Level5Plot1>().DialogOver && startDialog2)
        {
            UIManager.Instence.PopAllUI();
            plot2.GetComponent<Level5Plot2>().startTrigger = true;
            Debug.Log("oVer1");
            startDialog2 = false;
        }
        if(plot2.GetComponent<Level5Plot2>().DialogOver && !over)
        {
            player.GetComponent<Player>().CanOperate = true;
            Yingzu.SetActive(false);
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
