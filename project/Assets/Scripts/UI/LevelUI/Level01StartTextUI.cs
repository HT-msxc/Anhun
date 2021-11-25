using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01StartTextUI : BaseUIPanel
{
    static string path = "UI/Level1Plot1";
    public  Level01StartTextUI() : base(new UIPanelType(path)){}
    public override void OnEntry()
    {
        base.OnEntry();

    }

    public override void OnExit()
    {
        base.OnExit();
        GameObject trigger = GameObject.Find("Plot1Trigger");
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
