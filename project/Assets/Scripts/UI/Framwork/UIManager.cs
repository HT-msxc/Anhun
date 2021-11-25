using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : Singleton<UIManager>
{
    //保存UI类型
    public Dictionary<UIPanelType, GameObject> UIPanelDic = new Dictionary<UIPanelType, GameObject>();
    public Dictionary<string, GameObject> UIInstance = new Dictionary<string, GameObject>();
    //UI 显示栈 永远只显示栈顶
    public Stack<BaseUIPanel> UIStack = new Stack<BaseUIPanel>();
    public BaseUIPanel CurrentUI;
    //是否为空的判断
    protected override void Awake()
    {
        base.Awake();
        if(UIPanelDic == null)
        {
            UIPanelDic = new Dictionary<UIPanelType, GameObject>();
        }
        if(UIStack == null)
        {
            UIStack = new Stack<BaseUIPanel>();
        }
        if(UIInstance == null)
        {
            UIInstance = new Dictionary<string, GameObject> ();
        }
        DontDestroyOnLoad(this);
    }
    public GameObject GetOrCreateUI(UIPanelType baseUIPanel, string canvasName)
    {
        GameObject Canvas = GameObject.Find(canvasName);
        if(Canvas != null)
        {
            GameObject UI = null;
            //if(UIInstance.TryGetValue(baseUIPanel.Path,out UI))
            if(UIPanelDic.TryGetValue(baseUIPanel,out UI))
            {
                Debug.Log(UI.name);
                UI.SetActive(true);
                return UI;
            }
            else
            {
                Debug.Log(Resources.Load(baseUIPanel.Path));
                UI = (GameObject)Instantiate(Resources.Load(baseUIPanel.Path), Canvas.transform);
                UI.name = baseUIPanel.Name;
                //UIInstance.Add(baseUIPanel.Path, UI);
                UIPanelDic.Add(baseUIPanel,UI);
                return UI;
            }
        }
        else
        {
            Debug.LogError("There is no Canvas !!! Please Check it!");
            return null;
        }
    }

    //设置当前栈顶UI pause，将要显示的UI压栈，显示
    public void PushUI(BaseUIPanel nextUI, string canvasName)
    {
        CurrentUI = nextUI;
        if(UIStack.Count > 0)
        {
            UIStack.Peek().OnPause();
            UIStack.Push(nextUI);
            GameObject ui = GetOrCreateUI(nextUI.PanelType,canvasName);
            nextUI.CurrentActiveUI = ui;
            UIStack.Peek().OnEntry();
        }
        else
        {
            UIStack.Push(nextUI);
            GameObject ui = GetOrCreateUI(nextUI.PanelType, canvasName);
            nextUI.CurrentActiveUI = ui;
            UIStack.Peek().OnEntry();
        }
    }

    //设置弹出栈顶UI
    public void PopUI()
    {
        if(UIStack.Count > 0)
        {
            BaseUIPanel uIPanel = UIStack.Pop();
            Debug.Log(uIPanel.PanelType.Path);
            uIPanel.OnExit();
            GameObject.Destroy(uIPanel.CurrentActiveUI.gameObject) ;
            UIPanelDic.Remove(uIPanel.PanelType);
            //ReleaseUI(uIPanel);
        }
        //如果还大于0  说明还有UI 那么调用栈顶UI的 Resume
        if(UIStack.Count > 0)
        {
            UIStack.Peek().OnResume();
            CurrentUI =UIStack.Peek();
        }
    }

    /// <summary>
    /// 回收栈顶UI
    /// </summary>
    /// <param name="uIPanel"> 栈顶UI类型</param>
    public void ReleaseUI(BaseUIPanel baseUIPanel)
    {
        GameObject ui;
        if(UIInstance.TryGetValue(baseUIPanel.PanelType.Path,out ui))
        {
            ui.SetActive(false);
        }
        else
        {
            Debug.Log("ReleaseUI Error");
        }
    }

    public void PopAllUI()
    {
        while (UIStack.Count > 0)
        {
            PopUI();
        }
    }

    /// <summary>
    /// 切换场景时 清除上个场景所保存的UI
    /// </summary>
    public void OnLoadDestroy()
    {
        UIPanelDic.Clear();
        UIStack.Clear();
        UIInstance.Clear();
    }
}
