using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotType
{
    private string plotPath;
    private string plotName;
    private string canvasName;
    private string textName;
    private string imageName;
    private BaseUIPanel uIPanel;

    public string PlotName{get => plotName; set{plotName = value;}}
    public string PlotPath{get => plotPath; set{plotPath = value;}}
    public string CanvasName{get => canvasName; set{plotName = value;}}
    public string TextName{get => textName; set{textName = value;}}
    public string ImageName{get => imageName; set{imageName = value;}}

    public BaseUIPanel PlotUI{get => uIPanel; set{uIPanel = value;}}
    public PlotType(string plotname, string plotPath,string canvasName, string textName, string imageName, BaseUIPanel uIPanel)
    {
        this.plotName = plotname;
        this.plotPath = plotPath;
        this.canvasName = canvasName;
        this.textName = textName;
        this.imageName= imageName;
        this.uIPanel = uIPanel;
    }
}
