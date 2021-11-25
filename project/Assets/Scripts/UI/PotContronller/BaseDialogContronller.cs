using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;

public class BaseDialogContronller : BasePlotContronller
{
    public int Incounter = 0;
    public float speed;
    public bool startTrigger = false;
    public bool DialogOver = false;
    public Queue<string> dialogQueue = new Queue<string>();
    public Text dialogName;
    public Image image;
    public GameObject Touxiang;
    public Image touxing;
    public GameObject DialogName;
    public Player player;
    public PlotType plotType;
    string dia = null;
    string substring = null;
    int index = 0;
    float textTime; //每个字时间
    float textWaitTime;//

    public virtual void CreatePlot()
    {
    }
    public virtual void Start()
    {
        CreatePlot();
        try
        {
            StreamReader reader = new StreamReader(Application.dataPath + plotType.PlotPath);
            while (reader.Peek() >=0)
            {
                dialogQueue.Enqueue(reader.ReadLine());
            }
        }
        catch (System.Exception)
        {
            throw new System.Exception("Cant find Text2");
        }
    }

    public virtual void Update()
    {
        if(startTrigger && Incounter ==0)
        {
            Incounter++;
            UIManager.Instence.PushUI(plotType.PlotUI,plotType.CanvasName);
            CurrentUI = UITool.FindChildGameObject(plotType.PlotUI.CurrentActiveUI,plotType.TextName);
            DialogName = UITool.FindChildGameObject(plotType.PlotUI.CurrentActiveUI,"Name");
            Touxiang = UITool.FindChildGameObject(plotType.PlotUI.CurrentActiveUI,"touxiang");
            touxing = UITool.GetComponent<Image>(Touxiang.transform);
            dialogName = UITool.GetComponent<Text>(DialogName.transform);
            dialogName.text = dialogQueue.Dequeue();
            switch(dialogName.text)
            {
                case "脑海之声":
                    Destroy(Touxiang);
                    break;
                case "孟婆：":
                    touxing.sprite = Resources.Load<Sprite>("UI/mengpo");
                    break;
                case "丹娘：":
                    touxing.sprite = Resources.Load<Sprite>("UI/danliang");
                    break;
                case "阎王：":
                    touxing.sprite = Resources.Load<Sprite>("UI/yanwang");
                    break;
                case "阴卒：":
                    touxing.sprite = Resources.Load<Sprite>("UI/yingzu");
                    break;
                default:
                    Destroy(Touxiang);
                    break;
            }
            text = UITool.GetComponent<Text>(CurrentUI.transform);
        }

        if(dialogQueue.Count > 0 && startTrigger && Incounter ==1)
        {
            // text.text = dialogQueue.Peek();
            // if(Input.GetKeyDown(KeyCode.Space))
            // {
            //     dialogQueue.Dequeue();
            // }


            dia = dialogQueue.Peek();
            text.text = dia.Substring(0,index);
            if(index <= dia.Length - 1)
            {
                textTime += Time.deltaTime;
                if(textTime >= 0.1f)
                {
                    text.text = dia.Substring(0, ++index);
                    textTime = 0f;
                }
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    text.text = dia;
                    index = dia.Length;
                }
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    dialogQueue.Dequeue();
                    index = 0;
                }
            }
            // if(end)
            // {
            //     dia = dialogQueue.Dequeue();
            //     textTime = dia.Length * 0.05f;
            //     //text.DOText(dia,textTime);
            //     substring = dia.Substring(0,index);
            //     text.text = substring;
            //     end = false;
            // }
            // text.text = substring;
            // waitTime += Time.deltaTime;
            // if(waitTime >= textTime)
            // {
            //     //读完了 需要空格
            //     if(Input.GetKeyDown(KeyCode.Space))
            //     {
            //         text.text = "";
            //         dialogQueue.Dequeue();
            //         waitTime = 0;
            //         dia = dialogQueue.Dequeue();
            //         textTime = dia.Length * 0.1f;
            //         text.DOText(dia,textTime);
            //     }
            // }
            // else
            // {   //没读完 按了空格
            //     if(Input.GetKeyDown(KeyCode.Space) )
            //     {
            //         text.text= dia;
            //         waitTime +=textTime;
            //     }
            // }
        }
        else if(startTrigger && dialogQueue.Count ==0 && Incounter ==1)
        {
            Incounter++;
            GameObject imageUI = UITool.FindChildGameObject(plotType.PlotUI.CurrentActiveUI,plotType.ImageName);
            image = UITool.GetComponent<Image>(imageUI.transform);
            Invoke("DelayFade", speed);
        }
    }
    public virtual void  DelayFade()
    {
        image.DOFade(0,speed);
        text.DOFade(0,speed);
        Invoke("EndDialog",1.0f);
    }
    public virtual void EndDialog()
    {
        //player.CanOperate = true;
        UIManager.Instence.PopAllUI();
        DialogOver = true;
    }
}
