using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldAltar : AltarBase
{
    protected override void Awake() {
        base.Awake();
        thisNatureState = NatureState.Gold;
    }
}
