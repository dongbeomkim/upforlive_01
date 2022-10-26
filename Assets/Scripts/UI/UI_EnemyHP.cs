using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyHP : MonoBehaviour
{

    Image hp;
    Enemy enemy;

    void Start()
    {
        enemy = transform.GetComponentInParent<Enemy>();
        hp = transform.GetChild(0).GetComponent<Image>();
    }


    void Update()
    {
        hp.fillAmount = enemy.EnemyHP / 15;
        transform.position = enemy.transform.position + new Vector3(0, -1, 0);
    }
}
