using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementState
{
    FreeInWorld,
    Obtained,
    Used,
}
public class ElementBase : MonoBehaviour, IObserver
{
    protected ElementState elementState;
    protected NatureState natureState;
    [SerializeField]int currentOrder;
    public float chasingDistance = 3f;
    public float chasingSpeed;
    [SerializeField] AnimationCurve movingCurve;
    public Vector3 targetPosition;
    public Animator animator;
    [Header("计时模块")]
    float timeCount;
    public float limitTime = 0.5f;
    public void MoveTo(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance > chasingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition,chasingSpeed * movingCurve.Evaluate(timeCount / limitTime));
        }
    }
    void FixedUpdate()
    {
        if (elementState == ElementState.Obtained)
        {
            timeCount = Mathf.Clamp(timeCount + Time.fixedDeltaTime, 0, 1);
            MoveTo(targetPosition);
        }
    }

    public void UpdateTargetPosition(GetPositionMethod method)
    {
        targetPosition = method(currentOrder);
        timeCount = 0;
    }

    public void UpdateOrder(int selectedOrder)
    {
        if (currentOrder > selectedOrder)
        {
            currentOrder--;
            return;
        }
        if (currentOrder == selectedOrder)
        {
            elementState = ElementState.Used;
            HandleSelection();
        }
    }
    public int Order
    {
        get=>currentOrder;
        set=>currentOrder = value;
    }

    public ElementState ElementState
    {
        get=>elementState;
        set=>elementState = value;
    }
    public NatureState NatureState
    {
        get=>natureState;
        set=>natureState = value;
    }
    public virtual void ReturnToPool()
    {
        ObjectPoolManager.Instence.ReleaseObject(this.gameObject);
    }

    public virtual void HandleSelection(){}
}