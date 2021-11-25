using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Battle : MonoBehaviour
{
    public PushingWallControll pushingWallControll;
    public SeaTick seaTick;
    public SharkTick sharkTick;
    public bool isStart;

    public void GetIntoNextState()
    {
        seaTick.SetSeaNextStageTick();
        sharkTick.SetSharkNextStageTick();
    }

    public void StartBattle()
    {
        pushingWallControll.SetStartTick();
        GetIntoNextState();
        isStart = true;
    }
}