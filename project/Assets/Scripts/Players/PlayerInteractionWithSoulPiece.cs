using System.Security.Principal;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInteractionWithSoulPiece : MonoBehaviour
{
    Player player;
    PlayerSize nextBiggerSize;
    PlayerSize nextSmallerSize;
    
    [Header("灵魂碎片父物体")]
    public Transform soulPiecesCage;
    public Stack<GameObject> soulPiecesWaiting;         //被吸收的灵魂碎片
    public GameObject soulPiece;                        //soulPiece;
    public Action dragOutsidePieces;
    public Action pushInsiderPieces;

    [Header("体型检测器")]
    public Transform middleSizeDetector;
    public Transform bigSizeDetector;
    public Transform targetDetector;

    [Header("灵魂碎片数量")]
    public int soulPiecesCount;                 //计数器
    public int middleToBigNeedNum = 4;
    public int smallToMiddleNeedNum = 3;
    public int BigToMiddleReleaseNum = 4;
    public int MiddleToSmallReleaseNum = 3;
    public int releaseNum;
    public int needNum;
    [Header("碎片释放")]
    public float randomRange = 5;

    public void NotifyObserversOutside()
    {
        if (dragOutsidePieces != null)
            dragOutsidePieces();
    }

    public void NotifyObserversInside()
    {
        if (pushInsiderPieces != null)
            pushInsiderPieces();
    }

    public void AddSoulPieceObserverToInside(GameObject pieceObject)
    {
        pushInsiderPieces += pieceObject.GetComponent<SoulPiece>().MovingAwayFromPlayer;
    }

    public void AddSoulPieceObserverToOutside(GameObject pieceObject)
    {
        dragOutsidePieces += pieceObject.GetComponent<SoulPiece>().MovingTowardsPlayer;
    }

    public void RemoveOutsideSoulPieceObserver(GameObject pieceObject)
    {
        dragOutsidePieces -= pieceObject.GetComponent<SoulPiece>().MovingTowardsPlayer;
    }

    public void RemoveInsideSoulPieceObserver(GameObject pieceObject)
    {
        pushInsiderPieces -= pieceObject.GetComponent<SoulPiece>().MovingAwayFromPlayer;
    }

    private void Awake()
    {
        player = GetComponent<Player>();
        player.onPlayerStateChange += UpdateInfo;
        middleSizeDetector = transform.GetChild(0);
        bigSizeDetector = transform.GetChild(1);

        soulPiece = Resources.Load<GameObject>("Prefab/SoulPiece");
        GameObject temp = GameObject.Find("SoulPiecesCage");
        if (temp == null)
            soulPiecesCage = new GameObject("SoulPiecesCage").transform;
        else soulPiecesCage = temp.transform;
        soulPiecesCage.position = Vector3.zero;
        DontDestroyOnLoad(soulPiecesCage);
    }

    private void Start() 
    {
        soulPiecesWaiting = new Stack<GameObject>();
        soulPiecesCount = 0;
        PlayerSize startSize = player.GetPlayerSize();
        GetReleaseNum(startSize);
        UpdateInsideSupply();
    }

    private void Update()
    {
        if (player.CanOperate)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                NotifyObserversOutside();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ReleasingPieces();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        SoulPiece targetSoulPiece = other.GetComponent<SoulPiece>();
        if (targetSoulPiece != null && targetSoulPiece.IsAccessible)
        {
            InteractWithPiece(other.gameObject);
        }
    }

    // 碎片和玩家碰撞时触发该函数
    void InteractWithPiece(GameObject pieceObject)
    {
        SoulPiece piece = pieceObject.GetComponent<SoulPiece>();
        if (!player.CanOperate || targetDetector == null || (targetDetector != null && targetDetector.GetComponent<SizeDetector>().GetBlockValue()) || soulPiecesCount >= needNum)
        {
            AddSoulPieceObserverToInside(pieceObject);
            piece.soulPieceStateMachine.TransitionTo(3);
            return;
        }
        Absorb(pieceObject);
        soulPiecesCount++;
        if (soulPiecesCount == needNum)
        {
            player.SetPlayerSize(nextBiggerSize);
            AudioManager.Instance.PlayAudio("变大",AudioType.SoundEffect,gameObject);
            soulPiecesCount = 0;
        }
    }

    void Absorb(GameObject soulPieceObject)
    {
        SoulPiece targetPiece = soulPieceObject.GetComponent<SoulPiece>();
        targetPiece.soulPieceStateMachine.TransitionTo(2);
        soulPieceObject.SetActive(false);
        soulPiecesWaiting.Push(soulPieceObject);
    }

    void ReleasingPieces()
    {
        UpdateInsideSupply();
        GameObject temp;
        for(int i = 0; i < releaseNum + soulPiecesCount; i++)
        {
            temp = soulPiecesWaiting.Pop();
            temp.transform.position = transform.position;
            temp.SetActive(true);
        }
        NotifyObserversInside();
        soulPiecesCount = 0;
        player.SetPlayerSize(nextSmallerSize);
        AudioManager.Instance.PlayAudio("变小",AudioType.SoundEffect,gameObject);
    }

    void GetReleaseNum(PlayerSize size)
    {
        switch(size)
        {
            case PlayerSize.Big: releaseNum = BigToMiddleReleaseNum + MiddleToSmallReleaseNum;break;
            case PlayerSize.Middle: releaseNum = MiddleToSmallReleaseNum;break;
            case PlayerSize.Small: releaseNum = 0;break;
        }
    }

    void UpdateInsideSupply()
    {
        while(soulPiecesWaiting.Count < releaseNum + soulPiecesCount)
        {
            GameObject newPiece = Instantiate<GameObject>(soulPiece,transform.position + new Vector3(100, 100, 100) ,transform.rotation,soulPiecesCage);
            newPiece.SetActive(false);
            SoulPiece piece = newPiece.GetComponent<SoulPiece>();
            piece.playerTransform = transform;
            piece.playerInteractionWithSoulPiece = this;
            piece.soulPieceStateMachine.TransitionTo(2);
            soulPiecesWaiting.Push(newPiece);
        }
    }

    void UpdateInfo(NatureState beforeNatureState,PlayerSize beforeSizeState)
    {
        PlayerSize size = player.GetPlayerSize();
        NatureState state = player.GetNatureState();
        switch(size)
        {
            case PlayerSize.Small:
                targetDetector = middleSizeDetector;
                needNum = smallToMiddleNeedNum;
                nextBiggerSize = PlayerSize.Middle;
                nextSmallerSize = PlayerSize.Small;
                releaseNum = 0;
                break;
            case PlayerSize.Middle:
                targetDetector = bigSizeDetector;
                needNum = middleToBigNeedNum;
                nextBiggerSize = PlayerSize.Big;
                nextSmallerSize = PlayerSize.Small;
                releaseNum = MiddleToSmallReleaseNum;
                break;
            case PlayerSize.Big:
                targetDetector = null;
                needNum = 0;
                nextSmallerSize = PlayerSize.Middle;
                releaseNum = BigToMiddleReleaseNum;
                break;
            default: return;
        }
    }

    public void RefreshSoulCages()
    {
        if (soulPiecesWaiting == null)  return;
        if (soulPiecesWaiting.Count > 0)
        {
            foreach (GameObject soulPiece in soulPiecesWaiting)
            {
                soulPiece.transform.SetParent(null);
                Debug.Log("is poping Out soulPieces");
            }
        }
        
        while(soulPiecesCage.childCount != 0)
        {
            GameObject soulPiece = soulPiecesCage.GetChild(0).gameObject;
            soulPiece.transform.SetParent(null);
            SoulPiece piece = soulPiece.GetComponent<SoulPiece>();
            piece.RefreshSoulPiece();
            Debug.Log("is Deleting useless soulpieces");
            Destroy(soulPiece);
        }
        if (soulPiecesWaiting.Count > 0)
        {
            foreach (GameObject soulPiece in soulPiecesWaiting)
            {
                Debug.Log("is pushing Back soulPieces");
                soulPiece.transform.SetParent(soulPiecesCage);
            }
        }
    }
}