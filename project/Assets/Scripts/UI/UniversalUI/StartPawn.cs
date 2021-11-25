using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPawn : BaseUIPanel
{
    static string path = "UI/LevelStartPawn";
    public StartPawn() : base(new UIPanelType(path)){}
    public override void OnEntry()
    {
        base.OnEntry();
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
