using System.Dynamic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void OnPlayerStateChange(NatureState playerNature, PlayerSize playerSize);
public class Player : MonoBehaviour
{
    [SerializeField] NatureState natureState;
    [SerializeField] NatureState playerBeforeNature;
    [SerializeField] PlayerSize playerSize;
    [SerializeField] PlayerSize playerBeforeSize;
    bool canOperate = true;
    public OnPlayerStateChange onPlayerStateChange = null;
    #region 


    public Player()
    {
        natureState = NatureState.Normal;
        playerBeforeNature = NatureState.Normal;
        playerSize = PlayerSize.Middle;
        playerBeforeSize = PlayerSize.Middle;
    }
    private void Start() 
    {
        if (onPlayerStateChange != null)
            onPlayerStateChange(natureState, playerSize);
    }
    public void SetNatureState(NatureState targetState)
    {
        playerBeforeNature = natureState;
        natureState = targetState;
        playerBeforeSize = playerSize;
        onPlayerStateChange(playerBeforeNature, playerBeforeSize);
    }
    public NatureState GetNatureState()
    {
        return natureState;
    }

    public void SetPlayerSize(PlayerSize size)
    {
        playerBeforeNature = natureState;
        playerBeforeSize = playerSize;
        playerSize = size;
        onPlayerStateChange(playerBeforeNature, playerBeforeSize);
    }

    public PlayerSize GetPlayerSize()
    {
        return playerSize;
    }

    public bool CanOperate
    {
        get=>canOperate;
        set=>canOperate = value;
    }
    #endregion
}