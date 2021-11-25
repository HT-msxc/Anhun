using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelT3 : MonoBehaviour
{
    public GameObject ImageObj;
    public GameObject TextObj;
    //public float showTime = 0.5f;
    public float endTime = 12f;
    public float speed = 0.01f;
    Image image;
    Text text;
    bool playerIn = false;
    bool startShow = false;
    bool endShow = false;
    float timer = 0;
    float alph = 1;
    private void Start()
    {
        image = ImageObj.GetComponent<Image>();
        text = TextObj.GetComponent<Text>();
        ImageObj.SetActive(false);
        TextObj.SetActive(false);
    }
    private void Update()
    {
        if(playerIn)
        {
            ImageObj.SetActive(true);
            TextObj.SetActive(true);
            image.color = new Color(255,255, 255, 1f);
            text.color = new Color(0,0, 0, 1f);
            timer = 0;
            alph = 1;
        }
        else
        {
            timer += Time.deltaTime;
            if(alph>0)
            {
                alph -= speed;
                image.color = new Color(255,255,255,alph);
                text.color = new Color(0,0,0,alph);
            }
            if(timer >= endTime)
            {
                ImageObj.SetActive(false);
                TextObj.SetActive(false);
            }
        }
        // if(startShow && !endShow)
        // {
        //     image.DOFade(0f,endTime);
        //     text.DOFade(0f,endTime);
        //     endShow = true;
        //     //Invoke("Delay", endTime+0.5f);
        // }
    }
   private void OnTriggerEnter2D(Collider2D other)
   {
       if(other.tag == "Player")
       {
            playerIn = true;
            Debug.Log("进");
       }
   }
   private void OnTriggerExit2D(Collider2D other)
   {
       if(other.tag =="Player")
       {
           playerIn = false;
           Debug.Log("Chu");
       }
   }

   private void OnTriggerStay2D(Collider2D other)
   {
       if(other.tag == "Player")
       {
           playerIn = true;
       }
   }
}
