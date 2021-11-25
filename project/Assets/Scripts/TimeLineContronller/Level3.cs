using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    public GameObject ScenePlayer;
    public GameObject Timeline1;
    public GameObject Timeline2;
    public GameObject Timeline3;
    public GameObject Plot1;
    public GameObject Plot2;
    public GameObject cm1;
    public GameObject cm2;
    public GameObject cm3;
    bool playerIn = false;
    private void Start()
    {
        cm1.SetActive(false);
        cm2.SetActive(false);
        cm3.SetActive(false);
    }
    private void Update()
    {
        if(playerIn)
        {
            GameManager.Instence.CurrentPlayer.SetActive(false);
            ScenePlayer.SetActive(true);
            Timeline1.SetActive(true);
            ScenePlayer.GetComponent<Player>().CanOperate = false;
        }
        if(playerIn && Plot1.GetComponent<Level3Plot1>().DialogOver)
        {
            Timeline2.SetActive(true);
        }
        if(playerIn &&  Plot2.GetComponent<Level3Plot2>().DialogOver)
        {
            Timeline3.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerIn = true;
            cm1.SetActive(false);
            cm2.SetActive(true);
            cm3.SetActive(true);
        }
    }


    public void StartDialog1()
    {
        Plot1.GetComponent<Level3Plot1>().startTrigger = true;
    }
}
