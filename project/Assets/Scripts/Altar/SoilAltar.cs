using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilAltar : AltarBase
{
    protected override void Awake() {
        base.Awake();
        thisNatureState = NatureState.Soil;
    }
}
