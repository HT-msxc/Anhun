using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leve01Plot2 : BaseUIPanel
{
    static string path = "UI/Level1Plot2";
    public Leve01Plot2() : base(new UIPanelType(path)){}
    public override void OnEntry()
    {
        base.OnEntry();
    }

    public override void OnExit()
    {
        base.OnExit();
         GameObject trigger = GameObject.Find("Plot2Trigger");
        if(trigger != null)
            GameObject.Destroy(trigger);
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
