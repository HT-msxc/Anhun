using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGame : BaseUIPanel
{
    static string path = "UI/Selectlevel";
    public SelectGame() : base(new UIPanelType(path)){}

    public override void OnEntry()
    {
        base.OnEntry();
        GameObject level1Btn = UITool.FindChildGameObject(CurrentActiveUI,"Level1");
        GameObject level2Btn = UITool.FindChildGameObject(CurrentActiveUI,"Level2");
        GameObject level3Btn = UITool.FindChildGameObject(CurrentActiveUI,"Level3");
        GameObject level4Btn = UITool.FindChildGameObject(CurrentActiveUI,"Level4");
        GameObject level5Btn = UITool.FindChildGameObject(CurrentActiveUI,"Level5");
        GameObject level6Btn = UITool.FindChildGameObject(CurrentActiveUI,"Level6");
        GameObject level7Btn = UITool.FindChildGameObject(CurrentActiveUI,"Level7");
        GameObject level8Btn = UITool.FindChildGameObject(CurrentActiveUI,"Level8");

        Button btn1 = UITool.GetComponent<Button>(level1Btn.transform);
        Button btn2 = UITool.GetComponent<Button>(level2Btn.transform);
        Button btn3 = UITool.GetComponent<Button>(level3Btn.transform);
        Button btn4 = UITool.GetComponent<Button>(level4Btn.transform);
        Button btn5 = UITool.GetComponent<Button>(level5Btn.transform);
        Button btn6 = UITool.GetComponent<Button>(level6Btn.transform);
        Button btn7 = UITool.GetComponent<Button>(level7Btn.transform);
        Button btn8 = UITool.GetComponent<Button>(level8Btn.transform);

        btn1.onClick.RemoveAllListeners();
        btn2.onClick.RemoveAllListeners();
        btn3.onClick.RemoveAllListeners();
        btn4.onClick.RemoveAllListeners();
        btn5.onClick.RemoveAllListeners();
        btn6.onClick.RemoveAllListeners();
        btn7.onClick.RemoveAllListeners();
        btn8.onClick.RemoveAllListeners();

        btn1.onClick.AddListener(
            ()=>
            {
                SceneLoadManager.Instence.LoadSceneName = "Level01";
                UIManager.Instence.PopAllUI();
                UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            }
        );

        btn2.onClick.AddListener(
            ()=>
            {
                SceneLoadManager.Instence.LoadSceneName = "Level02";
                UIManager.Instence.PopAllUI();
                UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            }
        );
        btn3.onClick.AddListener(
            ()=>
            {
                SceneLoadManager.Instence.LoadSceneName = "Level03";
                UIManager.Instence.PopAllUI();
                UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            }
        );
        btn4.onClick.AddListener(
            ()=>
            {
                SceneLoadManager.Instence.LoadSceneName = "Level04";
                UIManager.Instence.PopAllUI();
                UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            }
        );
        btn5.onClick.AddListener(
            ()=>
            {
                SceneLoadManager.Instence.LoadSceneName = "Level05";
                UIManager.Instence.PopAllUI();
                UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            }
        );
        btn6.onClick.AddListener(
            ()=>
            {
                SceneLoadManager.Instence.LoadSceneName = "Level06";
                UIManager.Instence.PopAllUI();
                UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            }
        );
        btn7.onClick.AddListener(
            ()=>
            {
                SceneLoadManager.Instence.LoadSceneName = "Level07";
                UIManager.Instence.PopAllUI();
                UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            }
        );

        btn8.onClick.AddListener(
            ()=>
            {
                SceneLoadManager.Instence.LoadSceneName = "Level08";
                UIManager.Instence.PopAllUI();
                UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            }
        );
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }
}
