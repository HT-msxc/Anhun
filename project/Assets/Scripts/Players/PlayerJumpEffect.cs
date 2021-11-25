using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerJumpEffect : MonoBehaviour
{
    Player player;
    public List<Vector3> jumpEffectOffsetList;
    public List<GameObject> jumpEffectPrefabList;
    public Dictionary<PlayerSize, Vector3> jumpEffectOffsetsDictionary;
    public Dictionary<PlayerSize, GameObject> jumpEffectPrefabsDictionary;

    public Vector3 currentOffsetX;
    public Vector3 currentoffsetY;
    public GameObject currentPrefab;
    private void Awake()
    {
        player = GetComponent<Player>();
        player.onPlayerStateChange += UpdateJumpEffectPosition;
        jumpEffectOffsetsDictionary = new Dictionary<PlayerSize, Vector3>();
        jumpEffectPrefabsDictionary = new Dictionary<PlayerSize, GameObject>();

        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        jumpEffectOffsetsDictionary.Add(PlayerSize.Small, jumpEffectOffsetList[0]);
        jumpEffectOffsetsDictionary.Add(PlayerSize.Middle, jumpEffectOffsetList[1]);
        jumpEffectOffsetsDictionary.Add(PlayerSize.Big, jumpEffectOffsetList[2]);

        jumpEffectPrefabsDictionary.Add(PlayerSize.Small, jumpEffectPrefabList[0]);
        jumpEffectPrefabsDictionary.Add(PlayerSize.Middle, jumpEffectPrefabList[1]);
        jumpEffectPrefabsDictionary.Add(PlayerSize.Big, jumpEffectPrefabList[2]);
    }

    public void UpdateJumpEffectPosition(NatureState beforeNature, PlayerSize beforeSize)
    {
        PlayerSize size = player.GetPlayerSize();
        currentOffsetX = jumpEffectOffsetsDictionary[size];
        currentPrefab = jumpEffectPrefabsDictionary[size];
    }

    public void PlayJumpEffect()
    {
        ObjectPoolManager.Instence.CreateObject(currentPrefab, transform.position + currentOffsetX + currentoffsetY , Quaternion.Euler(0,0,0));
        ObjectPoolManager.Instence.CreateObject(currentPrefab, transform.position - currentOffsetX + currentoffsetY, Quaternion.Euler(0, 180, 0));
    }
}