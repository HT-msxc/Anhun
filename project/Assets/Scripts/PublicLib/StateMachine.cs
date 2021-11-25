using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public T stateOwner;
    public StateBase<T> currentState;
    public StateBase<T> previousState;
    public StateMachine(T owner, StateBase<T> beginState)
    {
        currentState = beginState;
        previousState = null;
        stateOwner = owner;
        stateDictionary = new Dictionary<int, StateBase<T>>();
        stateDictionary.Add(beginState.stateID, beginState);
        beginState.OnStateEnter();
    }
    Dictionary<int, StateBase<T>> stateDictionary;
    public void AddState(StateBase<T> state)
    {
        if (!stateDictionary.ContainsKey(state.stateID))
        {
            stateDictionary.Add(state.stateID, state);
            return;
        }
        Debug.LogError(nameof(T) + " stateMachine is adding a non-exist state!");
    }
    public void RemoveState(StateBase<T> state)
    {
        if (stateDictionary.ContainsKey(state.stateID))
        {
            stateDictionary.Remove(state.stateID);
            return;
        }
        Debug.LogError(nameof(T) + " stateMachine cannot remove a non-exist state!");
    }
    public void TransitionTo(int stateID)
    {
        if (stateDictionary.ContainsKey(stateID))
        {
            previousState = currentState;
            previousState.OnStateExit();
            currentState = stateDictionary[stateID];
            currentState.OnStateEnter();
        }
    }
}

public abstract class StateBase<T>
{
    public int stateID;
    public T stateOwner;
    public StateBase(T owner, int id)
    {
        stateOwner = owner;
        stateID = id;
    }
    public abstract void OnStateEnter();
    public abstract void OnStateStay();
    public abstract void OnStateExit();
}