using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAltar : AltarBase
{
    protected override void Awake() {
        base.Awake();
        thisNatureState = NatureState.Water;
    }
}
