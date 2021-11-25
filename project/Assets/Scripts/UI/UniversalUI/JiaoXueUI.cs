using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JiaoXueUI : BaseUIPanel
{
    static string path = "UI/JiaoXue";
    public JiaoXueUI() : base(new UIPanelType(path)) {}
    public override void OnEntry()
    {
        base.OnEntry();
        //开始游戏监听器
        GameObject BackGameBtn = UITool.FindChildGameObject(CurrentActiveUI,"Back");
        UITool.GetComponent<Button>(BackGameBtn.transform).onClick.RemoveAllListeners();
        UITool.GetComponent<Button>(BackGameBtn.transform).onClick.AddListener(
            ()=>
            {
                UIManager.Instence.PopUI();
            });
    }
    public override void OnPause()
    {
        base.OnPause();
    }
    public override void OnResume()
    {
        base.OnResume();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}
