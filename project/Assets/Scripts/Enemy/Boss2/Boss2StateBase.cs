using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss2StateBase : MonoBehaviour
{
    public bool isFinishState;

    public void Update() {
        RunState();
    }
    public abstract void RunState();
    public bool FinishState()
    {
        if(isFinishState)
        {
            this.enabled = false;
            return true;
        }
        return false;
    }
}
