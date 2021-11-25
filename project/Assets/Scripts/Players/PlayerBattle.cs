using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player))]
public class PlayerBattle : MonoBehaviour, IGetHurt
{
    Player player;
    PlayerMovement playerMovement;
    public BattleElementInfo battleElementInfo;
    public FollowPointMovement followPointMovement;
    public Transform attackPlace;
    [SerializeField] bool isInInvincibleTime;
    bool playerIsInFightKnife;
    float timeCounter1;
    [Header("无敌时间")]
    public float invincibleTime = 10;
    public float hitForce = 2000;
    bool isUsingSoilElement;
    bool isInIceEffect;
    Animator animator;
    Rigidbody2D rigid;
    public List<Transform> ownElementList;
    public DefenseAnimationControll soilAnimationControll;
    public DefenseAnimationControll iceAnimationControll;
    private void Awake() {
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
        rigid = GetComponent<Rigidbody2D>(); 
        battleElementInfo = GetComponent<BattleElementInfo>();
        followPointMovement = GetComponentInChildren<FollowPointMovement>();
        animator = GetComponent<Animator>();
        ownElementList = new List<Transform>();
        
    }
    void Start()
    {
        attackPlace = transform.Find("AttackPoint");
        soilAnimationControll = transform.Find("SoilEffect").GetComponent<DefenseAnimationControll>();
        iceAnimationControll = transform.Find("IceEffect").GetComponent<DefenseAnimationControll>();
    }
    private void Update()
    {
        if ((player.CanOperate || playerIsInFightKnife) && battleElementInfo.GoldNatureNum > 0 && Input.GetKeyDown(KeyCode.Z))
        {
            AttackOrDefend();
        }
    }

    void FixedUpdate()
    {
        if (isInInvincibleTime && isUsingSoilElement && timeCounter1 < invincibleTime)
        {
            timeCounter1 += Time.fixedDeltaTime;
            if (timeCounter1 >= invincibleTime)
            {
                isInInvincibleTime = false;
                soilAnimationControll.StopAnimation();
            }
        }
        else
        {
            isInInvincibleTime = false;
        }
    }
    void AttackOrDefend()
    {
        int i = 0;
        foreach(Transform element in ownElementList)
        {
            ElementBase elementBase = element.GetComponent<ElementBase>();
            if (elementBase.NatureState == NatureState.Gold && elementBase.ElementState == ElementState.Obtained)
            {
                ownElementList.RemoveAt(i);
                followPointMovement.RemoveObserver(elementBase);
                element.transform.position = attackPlace.position;
                element.transform.rotation = attackPlace.rotation;
                battleElementInfo.SelectedOrder = i;
                element.SetParent(transform);
                battleElementInfo.RemoveObserver(elementBase);              //先去掉跟随，再修改位置，再通知观察者，再移除它，最后改数量
                battleElementInfo.GoldNatureNum--;
                animator.Play(player.GetPlayerSize().ToString() + "_" + player.GetNatureState().ToString() + "_Attack");
                AudioManager.Instance.PlayAudio("刀光攻击",AudioType.SoundEffect, gameObject);
                break;
            }
            else
                i++;
        }
    }
    public void GetHurt(Transform attacker)
    {
        if((!player.CanOperate && !playerIsInFightKnife) || isInInvincibleTime || isInIceEffect)
        {
            return;
        }
        if (battleElementInfo.SoilNatureNum > 0)
        {
            int i = 0;
            foreach(Transform element in ownElementList)
            {
                ElementBase elementBase = element.GetComponent<ElementBase>();
                if (elementBase.NatureState == NatureState.Soil && elementBase.ElementState == ElementState.Obtained)
                {
                    ownElementList.RemoveAt(i);
                    battleElementInfo.SelectedOrder = i;
                    battleElementInfo.RemoveObserver(elementBase);
                    followPointMovement.RemoveObserver(elementBase);
                    battleElementInfo.SoilNatureNum--;
                    isUsingSoilElement = true;
                    isInInvincibleTime = true;
                    timeCounter1 = 0;
                    soilAnimationControll.PlayAnimation();
                    break;
                }
                else i++;
            }
        }
        else
        {
            SetDeath();
        }
    }
    public bool IsInvincibleTime
    {
        get=>isInInvincibleTime;
        set=>isInInvincibleTime = value;
    }

    public void SetDeath()
    {
        foreach(Transform element in  ownElementList)
        {
            ElementBase elementBase = element.GetComponent<ElementBase>();
            battleElementInfo.RemoveObserver(elementBase);
            followPointMovement.RemoveObserver(elementBase);
        }
        ownElementList.Clear();
        AudioManager.Instance.PlayAudio("主角死亡",AudioType.SoundEffect, gameObject);
        player.CanOperate = false;
        GameManager.DeadCounter++;
        animator.SetTrigger("isDead");
    }
    /// <summary>
    /// dead Animation event
    /// </summary>
    public void PlayerDead()
    {
        SaveData data = GameManager.Instence.GetGameData();
        SceneLoadManager.Instence.LoadSceneName = data.GameLevel;
        UIManager.Instence.PushUI(new LoadNextLevel(),"Canvas");
    }

    public void SetRebirth()
    {
        player.CanOperate = true;
        PlayerSize size = player.GetPlayerSize();
        NatureState natureState = player.GetNatureState();
        string animationName = size.ToString() + "_" + natureState.ToString() + "_Idle";
        animator.Play(animationName);
    }

    public bool PlayerIsInFightKnife
    {
        get=>playerIsInFightKnife;
        set{
            playerIsInFightKnife = value;
            player.CanOperate = !playerIsInFightKnife;
        }
    }

    public bool IsInIceEffect
    {
        get=>isInIceEffect;
        set{
            if (isInIceEffect == value)
                return;
            isInIceEffect = value;
            if (isInIceEffect)
            {
                iceAnimationControll.PlayAnimation();
            }
            else iceAnimationControll.StopAnimation();
        }
    }
}