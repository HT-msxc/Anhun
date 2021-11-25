using System.Transactions;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleInSkyState : StateBase<SoulPiece>
{
    public IdleInSkyState(SoulPiece soulPiece, int id):base(soulPiece, id){}
    float timeCount;
    public bool addTick;
    public override void OnStateEnter()
    {
        timeCount = 0;
        addTick = false;
    }

    public override void OnStateStay()
    {
        if (timeCount < stateOwner.coolingTime)
        {
            timeCount += Time.fixedDeltaTime;
        }
        else if (!addTick)
        {
            addTick = true;
            stateOwner.playerInteractionWithSoulPiece.AddSoulPieceObserverToOutside(stateOwner.gameObject);
        }
    }
    public override void OnStateExit(){}
}

public class DraggingState : StateBase<SoulPiece>
{
    public DraggingState(SoulPiece soulPiece, int id):base(soulPiece, id){}
    float timeCount;
    public override void OnStateEnter()
    {
        stateOwner.playerInteractionWithSoulPiece.RemoveOutsideSoulPieceObserver(stateOwner.gameObject);
        stateOwner.IsAccessible = true;
        timeCount = 0;
    }

    public override void OnStateStay()
    {
        if ((stateOwner.playerTransform.position - stateOwner.transform.position).magnitude > stateOwner.detectingDistance)
        {
            stateOwner.transform.position = Vector3.MoveTowards(stateOwner.transform.position, stateOwner.playerTransform.position, stateOwner.movingCurve.Evaluate(Mathf.Clamp(timeCount / stateOwner.FlyingTime, 0, 1)) * stateOwner.movingSpeed);
            timeCount += Time.fixedDeltaTime;
        }
    }
    public override void OnStateExit(){}
}

public class AbsorbedByPlayerState : StateBase<SoulPiece>
{
    public AbsorbedByPlayerState(SoulPiece soulPiece, int id):base(soulPiece, id){}
    public override void OnStateEnter()
    {
        stateOwner.IsAccessible = false;
        stateOwner.playerInteractionWithSoulPiece.AddSoulPieceObserverToInside(stateOwner.gameObject);
    }
    public override void OnStateStay(){}
    public override void OnStateExit(){}
}
public class PushingState : StateBase<SoulPiece>
{
    public PushingState(SoulPiece soulPiece, int id):base(soulPiece, id){}
    float timeCount;
    Vector3 endPosition;
    public override void OnStateEnter()
    {
        stateOwner.playerInteractionWithSoulPiece.RemoveInsideSoulPieceObserver(stateOwner.gameObject);
        stateOwner.IsAccessible = false;
        float x = UnityEngine.Random.Range(-stateOwner.floatXRange, stateOwner.floatXRange);
        float y = stateOwner.floatHeight + UnityEngine.Random.Range(-stateOwner.floatYRange, stateOwner.floatYRange);
        endPosition = new Vector3(x + stateOwner.transform.position.x , y, 0);
        timeCount = 0;
    }

    public override void OnStateStay()
    {
        if ((endPosition - stateOwner.transform.position).magnitude > stateOwner.detectingDistance)
        {
            stateOwner.transform.position = Vector3.MoveTowards(stateOwner.transform.position, endPosition, stateOwner.movingCurve.Evaluate(Mathf.Clamp(timeCount / stateOwner.FlyingTime, 0, 1)) * stateOwner.movingSpeed);
            timeCount += Time.fixedDeltaTime;
        }
        else
        {
            stateOwner.soulPieceStateMachine.TransitionTo(0);
        }
    }
    public override void OnStateExit(){}
}

public class SoulPiece : MonoBehaviour
{
    public AnimationCurve movingCurve;
    public Transform playerTransform;
    public StateMachine<SoulPiece> soulPieceStateMachine;
    public PlayerInteractionWithSoulPiece playerInteractionWithSoulPiece;
    [SerializeField]
    bool isAccessible;
    public float coolingTime = 2f;
    public float FlyingTime = 1f;
    public float floatHeight = 10;
    public float floatXRange = 10;
    public float floatYRange = 5f;
    public float movingSpeed = 5f;
    public float detectingDistance = 10;
    public float limitTime = 5f;
    private void Awake()
    {
        floatXRange = 30f;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerInteractionWithSoulPiece = playerTransform.GetComponent<PlayerInteractionWithSoulPiece>();

        #region stateMachine Initialization
        IdleInSkyState idleInSkyState = new IdleInSkyState(this, 0);
        DraggingState draggingState = new DraggingState(this, 1);
        AbsorbedByPlayerState absorbedByPlayerState = new AbsorbedByPlayerState(this, 2);
        PushingState pushingState = new PushingState(this, 3);
        soulPieceStateMachine = new StateMachine<SoulPiece>(this, idleInSkyState);
        soulPieceStateMachine.AddState(draggingState);
        soulPieceStateMachine.AddState(absorbedByPlayerState);
        soulPieceStateMachine.AddState(pushingState);
        #endregion
    }
    private void FixedUpdate() 
    {
        soulPieceStateMachine.currentState.OnStateStay();
    }

    public void MovingTowardsPlayer()
    {
        soulPieceStateMachine.TransitionTo(1);
    }

    public void MovingAwayFromPlayer()
    {
        soulPieceStateMachine.TransitionTo(3);
    }

    public bool IsAccessible
    {
        get=>isAccessible;
        set=>isAccessible = value;
    }

    public void RefreshSoulPiece()
    {
        StateBase<SoulPiece> soulState = soulPieceStateMachine.currentState;
        switch(soulPieceStateMachine.currentState.stateID)
        {
            case 0:
                if ((soulState as IdleInSkyState).addTick)
                {
                    if (playerInteractionWithSoulPiece != null)
                        playerInteractionWithSoulPiece.RemoveOutsideSoulPieceObserver(gameObject);
                }
                break;
            case 1:
                break;
            case 2:
                if (playerInteractionWithSoulPiece != null)
                    playerInteractionWithSoulPiece.RemoveInsideSoulPieceObserver(gameObject);
                break;
            case 3:
                break;
        }
    }
}