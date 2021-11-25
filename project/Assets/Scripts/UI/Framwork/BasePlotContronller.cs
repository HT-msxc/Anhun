using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BasePlotContronller : MonoBehaviour
{
    public string plot;
    public GameObject CurrentUI;
    public Text text;
    public virtual void StartShowText(PlotType plotType)
    {
        UIManager.Instence.PushUI(plotType.PlotUI,plotType.CanvasName);
        CurrentUI = UITool.FindChildGameObject(plotType.PlotUI.CurrentActiveUI,plotType.TextName);
        text = UITool.GetComponent<Text>(CurrentUI.transform);
        text.DOText(plot,5f);
    }

    public virtual void EndStartUI(PlotType plotType)
    {
        text.DOFade(0,2f);
        UITool.FindChildGameObject(plotType.PlotUI.CurrentActiveUI,plotType.ImageName).GetComponent<Image>().DOFade(0,2f);
    }
    public virtual void POPUI()
    {
        UIManager.Instence.PopUI();
        //TODO: lock input
    }

}
