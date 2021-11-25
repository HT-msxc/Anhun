using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainButtonUI : BaseUIPanel
{
    static string path = "UI/MainButton";
    public MainButtonUI() : base(new UIPanelType(path)) {}
    public override void OnEntry()
    {
        base.OnEntry();
        //开始游戏监听器
        GameObject startGameBtn = UITool.FindChildGameObject(CurrentActiveUI,"StartGame");
        UITool.GetComponent<Button>(startGameBtn.transform).onClick.RemoveAllListeners();
        UITool.GetComponent<Button>(startGameBtn.transform).onClick.AddListener(
            ()=>
            {
                SceneLoadManager.Instence.LoadSceneName = "Level01";
                GameManager.Instence.SaveGameData();
                UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            });

        GameObject continueGameBtn = UITool.FindChildGameObject(CurrentActiveUI,"ContinueGame");
        UITool.GetComponent<Button>(continueGameBtn.transform).onClick.RemoveAllListeners();
        UITool.GetComponent<Button>(continueGameBtn.transform).onClick.AddListener(
            ()=>
            {
                SaveData data = GameManager.Instence.GetGameData();
                if(data == null)
                {
                    //FIXME: 这里没有存档是直接按 新游戏开始
                    SceneLoadManager.Instence.LoadSceneName = "Level01";
                    GameManager.Instence.SaveGameData();
                    UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
                }
                else
                {
                    SceneLoadManager.Instence.LoadSceneName = data.GameLevel;
                    UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
                    Debug.Log(data.GameLevel);
                }
            }
        );

        GameObject loadGameBtn = UITool.FindChildGameObject(CurrentActiveUI,"LoadGame");
        UITool.GetComponent<Button>(loadGameBtn.transform).onClick.RemoveAllListeners();
        UITool.GetComponent<Button>(loadGameBtn.transform).onClick.AddListener(
            ()=>
            {
                //TODO: push level select
                UIManager.Instence.PushUI(new SelectGame(), "Canvas");
            }
        );

        GameObject quitGameBtn = UITool.FindChildGameObject(CurrentActiveUI,"QuitGame");
        UITool.GetComponent<Button>(quitGameBtn.transform).onClick.RemoveAllListeners();
        UITool.GetComponent<Button>(quitGameBtn.transform).onClick.AddListener(
            ()=>
            {
                Application.Quit();
            }
        );

        GameObject DevelopBtn = UITool.FindChildGameObject(CurrentActiveUI,"Develop");
        UITool.GetComponent<Button>(DevelopBtn.transform).onClick.RemoveAllListeners();
        UITool.GetComponent<Button>(DevelopBtn.transform).onClick.AddListener(
            ()=>
            {
                Debug.Log("Load");
                SceneManager.LoadScene("AuthorList");
            }
        );

        GameObject JiaoxueBtn = UITool.FindChildGameObject(CurrentActiveUI,"JiaoXue");
        UITool.GetComponent<Button>(JiaoxueBtn.transform).onClick.RemoveAllListeners();
        UITool.GetComponent<Button>(JiaoxueBtn.transform).onClick.AddListener(
            ()=>
            {
                UIManager.Instence.PushUI(new JiaoXueUI(),"Canvas");
            }
        );
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
