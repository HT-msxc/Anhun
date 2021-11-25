using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : BaseUIPanel
{
    static string path = "UI/LoadNext";
    public LoadNextLevel() : base(new UIPanelType(path)){}

    public override void OnEntry()
    {
        base.OnEntry();
        // 这里需要在前一步将 场景名字赋值给Scenename
        SceneLoadManager.Instence.StartLoad();
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
