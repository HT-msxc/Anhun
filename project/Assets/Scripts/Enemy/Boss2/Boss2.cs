using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boss2 : MonoBehaviour
{
    [SerializeField] public Transform player;

    [SerializeField] public Transform[] swords = new Transform[3];
    [SerializeField] public Transform[] bossClones = new Transform[3];
    [SerializeField] public Transform[] swordsMirroring = new Transform[3];
    [SerializeField] Transform[] AttackPoints = new Transform[3];
    public Transform[] rangePoints3 = new Transform[3];


    [Header("StateChange")]
    [SerializeField] List<state> states1 = new List<state>();
    [SerializeField] List<state> states2 = new List<state>();
    [SerializeField] List<state> states3 = new List<state>();
    [SerializeField] public Vector2Int currtenState;
    public List<List<state>> statesList = new List<List<state>>();

    Dictionary<state, Boss2StateBase> statesDict = new Dictionary<state, Boss2StateBase>();
    bool startRunState;
    bool finishRunStates;

    bool finishInsert;

    public enum state
    {
        SingleAttack,
        HorizontalAttack,
        TripleAttack,
        InsertSword,
        RangeAttack,
        FightKnife,
        SmallWait,
        MiddleWait,
        BigWait,
        SingleAttack_RangeAttack,
        RangeAttack_Invok,
        SingleAttack_RangeAttack_Invok,
        SingleAttack_HorizontalAttack,
        HorizontalAttack_FightKnife,
        StartState,
        ChangeState
    }
    private void Awake()
    {
        statesList.Add(states1);
        statesList.Add(states2);
        statesList.Add(states3);
        statesDict.Add(state.SingleAttack, GetComponent<SingleAttackState>());
        statesDict.Add(state.HorizontalAttack, GetComponent<HorizontalAttackState>());
        statesDict.Add(state.TripleAttack, GetComponent<TripleAttackState>());
        statesDict.Add(state.InsertSword, GetComponent<InsertSwordState>());
        statesDict.Add(state.RangeAttack, GetComponent<RangeAttackState>());
        statesDict.Add(state.FightKnife, GetComponent<FightKnifeState>());
        statesDict.Add(state.SmallWait, GetComponent<WaitState>());
        statesDict.Add(state.MiddleWait, GetComponent<WaitState>());
        statesDict.Add(state.BigWait, GetComponent<WaitState>());
        statesDict.Add(state.SingleAttack_RangeAttack, GetComponent<SingleAttack_RangeAttackState>());
        statesDict.Add(state.RangeAttack_Invok, GetComponent<RangeAttack_InvokState>());
        statesDict.Add(state.SingleAttack_RangeAttack_Invok, GetComponent<SingleAttack_RangeAttack_InvokState>());
        statesDict.Add(state.SingleAttack_HorizontalAttack, GetComponent<SingleAttack_HorizontalAttackState>());
        statesDict.Add(state.HorizontalAttack_FightKnife, GetComponent<HorizontalAttack_FightKnifeState>());
        statesDict.Add(state.StartState, GetComponent<StartState>());
        statesDict.Add(state.ChangeState, GetComponent<Boss2ChangeState>());
    }

    private void OnEnable()
    {
        player = GameManager.Instence.CurrentPlayer.transform;
    }
    void Start()
    {
        InitAttack();
    }

    void InitAttack()
    {
        rangePoints3 = RangePoint3();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameManager.Instence.CurrentPlayer.transform;
        }
        if (!finishRunStates)
        {
            Fsm();
        }
    }

    #region ChangeState
    void Fsm()
    {
        if (RunState(statesList[currtenState.x][currtenState.y]))
        {
            InitAttack();
            Debug.Log(currtenState);
            Debug.Log(statesList[currtenState.x][currtenState.y]);
            if (statesList[currtenState.x][currtenState.y] == state.InsertSword)
            {
                finishInsert = true;
            }
            currtenState.y++;
            if (currtenState.y == (statesList[currtenState.x].Count))
            {
                currtenState.y = 0;
                currtenState.x++;
                finishInsert = false;
                if (currtenState.x == (statesList.Count))
                {
                    Debug.Log("最后阶段");
                    bossClones[0].GetComponent<Animator>().Play("Die");
                    bossClones[0].position = new Vector3(77,14);
                    finishRunStates = true;
                }
            }
            startRunState = false;
        }
    }
    bool RunState(state thisState)
    {
        if (!startRunState)
        {
            statesDict[thisState].enabled = true;
            startRunState = true;
        }
        if (statesDict[thisState] is WaitState)
        {
            var wait = statesDict[thisState] as WaitState;
            if (finishInsert)
                wait.rotateSwordNum = 1;
            else
                wait.rotateSwordNum = 3;
        }
        switch (thisState)
        {
            case state.BigWait:
                var bigwait = statesDict[thisState] as WaitState;
                bigwait.waitTime = 3;
                return bigwait.FinishState();
            case state.MiddleWait:
                var midwait = statesDict[thisState] as WaitState;
                midwait.waitTime = 2;
                return midwait.FinishState();
            case state.SmallWait:
                var smallwait = statesDict[thisState] as WaitState;
                smallwait.waitTime = 1;
                return smallwait.FinishState();
            case state.SingleAttack:
                for (int i = 1; i < 3; i++)
                {
                    swords[i].gameObject.SetActive(false);
                }
                return statesDict[thisState].FinishState();
            default: return statesDict[thisState].FinishState();
        }
    }
    #endregion
    private void OnDrawGizmos()
    {
        // foreach (var i in points)
        // {
        //     Gizmos.DrawWireCube(i, Vector3.one);
        // }
        //
        // for (int i = 0; i < 12; i++)
        // {
        //     Gizmos.DrawWireCube(RotateSwordPoint[i, 0], Vector3.one);
        // }
    }


    protected Transform[] RangePoint3()
    {
        var points = new Transform[3];
        var range = new int[3];
        do
        {
            range = new int[3] { UnityEngine.Random.Range(0, 3), UnityEngine.Random.Range(0, 3), UnityEngine.Random.Range(0, 3) };
        } while (range[0] == range[1] || range[1] == range[2] || range[0] == range[2]);
        for (int i = 0; i < 3; i++)
        {
            points[i] = AttackPoints[range[i]];
        }
        return points;
    }

    public state GetcurrentState()
    {
        return statesList[currtenState.x][currtenState.y];
    }

}
