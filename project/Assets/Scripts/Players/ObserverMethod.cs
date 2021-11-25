using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IObserver
{
    
}

public abstract class SubjectBase : MonoBehaviour
{
    public abstract void AddObserver(IObserver observer);
    public abstract void RemoveObserver(IObserver observer);
    public abstract void NotifyObserver();
}