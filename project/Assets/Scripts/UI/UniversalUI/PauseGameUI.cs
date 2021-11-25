using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameUI : BaseUIPanel
{
    static string path = "UI/PauseGame";
    public PauseGameUI() : base(new UIPanelType(path)){}
    public override void OnEntry()
    {
        base.OnEntry();
        //Back to Game
        GameObject backGameBtn = UITool.FindChildGameObject(CurrentActiveUI,"BackGame");
        UITool.GetComponent<Button>(backGameBtn.transform).onClick.RemoveAllListeners();
        UITool.GetComponent<Button>(backGameBtn.transform).onClick.AddListener(
            ()=>
            {
                InputManager.Instence.EscCounter = 0;
                UIManager.Instence.PopUI();
            }
        );

        GameObject LoadGameBtn = UITool.FindChildGameObject(CurrentActiveUI,"SelectGame");
        UITool.GetComponent<Button>(LoadGameBtn.transform).onClick.RemoveAllListeners();
        UITool.GetComponent<Button>(LoadGameBtn.transform).onClick.AddListener(
            ()=>
            {
                //TODO: LoadGame UI
                UIManager.Instence.PushUI(new SelectGame(), "Canvas");
            }
        );

        //Back to main
        GameObject backToMainBtn = UITool.FindChildGameObject(CurrentActiveUI,"BackToMain");
        UITool.GetComponent<Button>(backToMainBtn.transform).onClick.RemoveAllListeners();
        UITool.GetComponent<Button>(backToMainBtn.transform).onClick.AddListener(
            ()=>
            {
                SceneLoadManager.Instence.LoadSceneName = "MainScene";
                UIManager.Instence.PushUI(new LoadNextLevel(),"Canvas");
                InputManager.Instence.EscCounter = 0;
                Time.timeScale = 1.0f;
                GameObject.Destroy(InputManager.Instence.gameObject);
            }
        );


    }

    public override void OnExit()
    {
        base.OnExit();
        Time.timeScale = 1.0f;
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
