using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /* * * * * * * * 큰 범위 변수 선언 * * * * * * * */
    Player player;

    private void Awake()
    {
        player = transform.GetComponentInParent<Player>();
    }

    private void Start()
    {
        
    }

    /* * * * * * * * 트리거로 공격 판정 * * * * * * * */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(player.deal, player.transform.position);
        }
        else if (collision.CompareTag("RangeEnemy"))
        {
            collision.GetComponent<RangeEnemy>().TakeDamage(player.deal, player.transform.position);
        }
        else if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<Boss>().TakeDamage(player.deal, transform.position);
        }
    }

    
}
