using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Level01Plot2Contronller : BasePlotContronller
{
    int InCount = 0;
    PlotType plotType = new PlotType("Level1Plot2","/Level1Plot2.txt","Plot2Canvas","Text","Image",new Leve01Plot2());
    public virtual void Start()
    {
        try
        {
            StreamReader reader = new StreamReader(Application.dataPath + plotType.PlotPath);
            plot = reader.ReadToEnd();
        }
        catch (System.Exception)
        {
            throw new System.Exception("Cant find Level2Text2");
        }
    }
    public virtual void Update()
    {
        if(InCount == 1)
        {
            InCount = 2;
            StartShowText(plotType);
            Invoke("EndUI",6f);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            ++InCount;
            if(InCount > 1)
                InCount = 2; //防止溢出
        }
    }
    public override void EndStartUI(PlotType plotType)
    {
        base.EndStartUI(plotType);
    }
    void EndUI()
    {
        EndStartUI(plotType);
    }
}
