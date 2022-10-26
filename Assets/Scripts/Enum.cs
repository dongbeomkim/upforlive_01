using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum으로 몬스터 상태 나중에 해보자
public enum EnemyState
{ 
    Idle = 0,
    Patrol,
    Chase,
    Attack,
    Dead
}

public enum ItemIDCode
{
    Coin_Copper = 0,
    Coin_Silver,
    Coin_Gold,
    HealingPotion,
    SmallKnife,
}