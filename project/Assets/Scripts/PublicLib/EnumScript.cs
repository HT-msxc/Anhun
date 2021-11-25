using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region 状态枚举
/// <summary>
/// 金木水火土
/// </summary>
public enum NatureState
{
    Gold,
    Wood,
    Water,
    Fire,
    Soil,
    Normal
}

public enum PlayerSize
{
    Big,
    Middle,
    Small,
}

public enum PlayerMoveState
{
    Idle,
    Moving,
    Jumping,
    Damping,
}
#endregion