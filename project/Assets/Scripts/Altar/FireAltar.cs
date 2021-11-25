using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAltar : AltarBase
{
    protected override void Awake() {
        base.Awake();
        thisNatureState = NatureState.Fire;
    }

    
}
