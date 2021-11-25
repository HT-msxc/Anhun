using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;


public class Level01StartTextUIContronller : MonoBehaviour
{
    string plot;
    int InCount = 0;
    Level01StartTextUI currentUI;
    GameObject CurrentUI;
    Text text;
    private void Start()
    {
        currentUI = new Level01StartTextUI();
        try
        {
            StreamReader reader = new StreamReader(Application.dataPath + "/Level1Plot1.txt");
            plot = reader.ReadToEnd();
        }
        catch (System.Exception)
        {
            throw new System.Exception("Cant find Level1Text1");
        }
    }
    private void Update()
    {
        if(InCount == 1)
        {
            InCount = 2;
            StartShowText();
            Invoke("EndStartUI",6f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            ++InCount;
            if(InCount > 1)
                InCount = 2; //防止溢出

        }
    }

    void StartShowText()
    {
        UIManager.Instence.PushUI(currentUI,"Plot1Canvas");
        CurrentUI = UITool.FindChildGameObject(currentUI.CurrentActiveUI,"Text");
        text = UITool.GetComponent<Text>(CurrentUI.transform);
        text.DOText(plot,5f);
    }
    void EndStartUI()
    {
        text.DOFade(0,2f);
        UITool.FindChildGameObject(currentUI.CurrentActiveUI,"Image").GetComponent<Image>().DOFade(0,2f);
        Invoke("POPUI",5f);
    }
    void POPUI()
    {
        UIManager.Instence.PopUI();
        //TODO: lock input
    }
}
