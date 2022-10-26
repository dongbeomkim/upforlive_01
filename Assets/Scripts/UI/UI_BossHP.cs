using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHP : MonoBehaviour
{

    Slider hp;
    Boss boss;

    void Start()
    {
        boss = transform.GetComponentInParent<Boss>();
        hp = transform.GetChild(0).GetComponent<Slider>();
    }

    
    void Update()
    {
        hp.value = boss.BossHP/boss.maxHP;
    }
}
