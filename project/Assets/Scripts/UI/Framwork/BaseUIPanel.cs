using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUIPanel
{
    public UIPanelType PanelType{get; set;}
    public GameObject CurrentActiveUI{get; set;}
    public BaseUIPanel(UIPanelType panelType)
    {
        PanelType = panelType;
    }
    public virtual void OnEntry()
    {
        CurrentActiveUI.AddComponent<CanvasGroup>();
    }

    public virtual void OnPause()
    {
        CurrentActiveUI.GetComponent<CanvasGroup>().interactable = false;
    }

    public virtual void OnResume()
    {
        CurrentActiveUI.GetComponent<CanvasGroup>().interactable = true;
    }

    public virtual void OnExit()
    {
        //
    }
}
