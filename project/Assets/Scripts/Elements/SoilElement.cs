using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilElement : ElementBase
{
    void Awake()
    {
        animator = GetComponent<Animator>();
        elementState = ElementState.FreeInWorld;
        natureState = NatureState.Soil;
    }
    public override void HandleSelection()
    {
        ReturnToPool();
    }
}