using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum���� ���� ���� ���߿� �غ���
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