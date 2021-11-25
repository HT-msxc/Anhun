using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void OnElementSelected(int index);
public class BattleElementInfo : SubjectBase
{
    public OnElementSelected onElementSelected;
    [SerializeField] int goldNatureNum;
    public int goldNatureMaxNum = 3;
    [SerializeField] int soilNatureNum;
    public int soilNatureMaxNum = 1;
    int selectedOrder;
    public int GoldNatureNum
    {
        get=>goldNatureNum;
        set=>goldNatureNum = value;
    }

    public int SoilNatureNum
    {
        get=>soilNatureNum;
        set=>soilNatureNum = value;
    }

    // 仅当设置选择的序号时才通知漂浮的元素群
    public int SelectedOrder
    {
        get=>selectedOrder;
        set
        {
            selectedOrder = value;
            NotifyObserver();
        }
    }
    
    public override void AddObserver(IObserver observer)
    {
        ElementBase targetElement;
        if (observer is ElementBase)
        {
            targetElement = observer as ElementBase;
            onElementSelected += targetElement.UpdateOrder;
        }
    }

    public override void NotifyObserver()
    {
        if (onElementSelected != null)
        {
            onElementSelected(selectedOrder);
        }
    }

    public override void RemoveObserver(IObserver observer)
    {
        ElementBase targetElement;
        if (observer is ElementBase)
        {
            targetElement = observer as ElementBase;
            onElementSelected -= targetElement.UpdateOrder;
        }
    }
}
