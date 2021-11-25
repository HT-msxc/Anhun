using System.Collections.Generic;
using UnityEngine;
public delegate Vector3 GetPositionMethod(int order);
public delegate void UpdateObserver(GetPositionMethod method);
public class FollowPointMovement : SubjectBase
{
    public UpdateObserver onPositionChanged;
    public Vector3 starPosition;
    float timeCount;
    public float existTime;
    public List<Vector3> elementPositionOffsets;
    private void Start()
    {
        starPosition = transform.localPosition;
    }
    void FixedUpdate()
    {
        if (timeCount < existTime)
        {
            timeCount += Time.fixedDeltaTime;
        }
        else
        {
            timeCount = 0;
            NotifyObserver();
        }
    }

    public Vector3 GetTargetPosition(int order)
    {
        float multiplier;
        if (transform.parent.localRotation.eulerAngles.y == 0)
        {
            multiplier = 1;
        }
        else
            multiplier = -1;
        if (order < 4)
        {
            return transform.position + new Vector3(elementPositionOffsets[order].x * multiplier, elementPositionOffsets[order].y,elementPositionOffsets[order].z);
        }
        return Vector3.zero;
    }

    public override void AddObserver(IObserver observer)
    {
        ElementBase elementBase;
        if (observer is ElementBase)
        {
            elementBase = observer as ElementBase;
            onPositionChanged += ((ElementBase)observer).UpdateTargetPosition;
        }
    }

    public override void RemoveObserver(IObserver observer)
    {
        ElementBase elementBase;
        if (observer is ElementBase)
        {
            elementBase = observer as ElementBase;
            onPositionChanged -= ((ElementBase)observer).UpdateTargetPosition;
        }
    }

    public override void NotifyObserver()
    {
        if (onPositionChanged != null)
            onPositionChanged(GetTargetPosition);
    }
}