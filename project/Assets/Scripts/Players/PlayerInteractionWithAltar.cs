using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(TimeCounter))]
public class PlayerInteractionWithAltar : MonoBehaviour
{
    Player player;
    PlayerBattle playerBattle;
    FollowPointMovement followPointMovement;
    [Header("元素侵染")]
    public bool isInfluenced;
    public float natureInfluenceTime = 10f;
    public TimeCounter timeCounter;
    public Dictionary<NatureState, Action> touchAltarActionDictionary;
    public Transform altarTransform;

    [Header("金土元素生成")]
    public GameObject goldElementPrefab;
    public GameObject soilElementPrefab;
    public Vector3 instancePositionOffset;
    void Awake()
    {
        player = GetComponent<Player>();
        playerBattle = GetComponent<PlayerBattle>();
        followPointMovement = GetComponentInChildren<FollowPointMovement>();
        touchAltarActionDictionary = new Dictionary<NatureState, Action>();
        touchAltarActionDictionary.Add(NatureState.Gold, TouchGoldAltar);
        touchAltarActionDictionary.Add(NatureState.Water, TouchWaterAltar);
        touchAltarActionDictionary.Add(NatureState.Fire, TouchFireAltar);
        touchAltarActionDictionary.Add(NatureState.Soil, TouchSoilAltar);
    }

    private void Start()
    {
        goldElementPrefab = Resources.Load<GameObject>("Prefab/Elements/GoldElement");
        soilElementPrefab = Resources.Load<GameObject>("Prefab/Elements/SoilElement");
        timeCounter = GetComponent<TimeCounter>();
        timeCounter.enabled = false;
        timeCounter.LimitTime = natureInfluenceTime;
    }

    void LateUpdate()
    {
        if (timeCounter.enabled && timeCounter.IsArrived)
        {
            player.SetNatureState(NatureState.Normal);
            timeCounter.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        # region Interact with Altar
        AltarBase tempAltar = other.GetComponent<AltarBase>();
        if (player.CanOperate && tempAltar != null && !tempAltar.IsCD)
        {
            Action action;
            altarTransform = other.transform;
            if (touchAltarActionDictionary.TryGetValue(tempAltar.ThisNatureState,out action))
            {
                touchAltarActionDictionary[tempAltar.ThisNatureState]();
            }
        }
        #endregion
    }

    public void TouchFireAltar()
    {
        altarTransform.GetComponent<AltarBase>().CloseAltar();
        AudioManager.Instance.PlayAudio("侵染", AudioType.SoundEffect, gameObject);
        NatureState currentState = player.GetNatureState();
        switch(currentState)
        {
            case NatureState.Normal:
                timeCounter.CurrentTime = 0;
                timeCounter.enabled = true;
                player.SetNatureState(NatureState.Fire);
                break;                                                  //无属性则设为火属性
            case NatureState.Water:
                timeCounter.CurrentTime = timeCounter.LimitTime;        //水属性则结束水属性
                break;
            case NatureState.Fire:
                timeCounter.CurrentTime = 0;                            //火属性则刷新持续时间
                break;
        }
    }
    public void TouchWaterAltar()
    {
        altarTransform.GetComponent<AltarBase>().CloseAltar();
        AudioManager.Instance.PlayAudio("侵染", AudioType.SoundEffect, gameObject);
        NatureState currentState = player.GetNatureState();
        switch(currentState)
        {
            case NatureState.Normal:
                timeCounter.CurrentTime = 0;
                timeCounter.enabled = true;
                player.SetNatureState(NatureState.Water);
                break;
            case NatureState.Fire:
                timeCounter.CurrentTime = timeCounter.LimitTime;
                break;
            case NatureState.Water:
                timeCounter.CurrentTime = 0;
                break;
        }
    }
    public void TouchGoldAltar()
    {
        if (playerBattle.battleElementInfo.GoldNatureNum < playerBattle.battleElementInfo.goldNatureMaxNum)
        {
            Debug.LogWarning(playerBattle.battleElementInfo.GoldNatureNum);
            altarTransform.GetComponent<AltarBase>().CloseAltar();
            AudioManager.Instance.PlayAudio("激活土元素", AudioType.SoundEffect, gameObject);
            GameObject newElement = ObjectPoolManager.Instence.CreateObject(goldElementPrefab, altarTransform.position + instancePositionOffset, altarTransform.rotation);
            GoldElement element = newElement.GetComponent<GoldElement>();
            element.Order = playerBattle.battleElementInfo.GoldNatureNum + playerBattle.battleElementInfo.SoilNatureNum;
            playerBattle.ownElementList.Add(newElement.transform);
            playerBattle.battleElementInfo.AddObserver(element);
            followPointMovement.AddObserver(element);
            element.ElementState = ElementState.Obtained;
            playerBattle.battleElementInfo.GoldNatureNum++;
        }
    }
    public void TouchSoilAltar()
    {
        if (playerBattle.battleElementInfo.SoilNatureNum < playerBattle.battleElementInfo.soilNatureMaxNum)
        {
            altarTransform.GetComponent<AltarBase>().CloseAltar();
            AudioManager.Instance.PlayAudio("激活土元素", AudioType.SoundEffect, gameObject);
            GameObject newElement = ObjectPoolManager.Instence.CreateObject(soilElementPrefab, altarTransform.position + instancePositionOffset, altarTransform.rotation);
            SoilElement element = newElement.GetComponent<SoilElement>();
            element.ElementState = ElementState.Obtained;
            element.Order = playerBattle.battleElementInfo.GoldNatureNum + playerBattle.battleElementInfo.SoilNatureNum;
            playerBattle.ownElementList.Add(newElement.transform);
            playerBattle.battleElementInfo.AddObserver(element);
            followPointMovement.AddObserver(element);
            playerBattle.battleElementInfo.SoilNatureNum++;
        }
    }
}