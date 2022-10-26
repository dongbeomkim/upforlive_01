using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /* * * * * * * * ū ���� ���� ���� * * * * * * * */
    Player player;

    private void Start()
    {
        player = transform.GetComponentInParent<Player>();
    }

    /* * * * * * * * Ʈ���ŷ� ���� ���� * * * * * * * */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(player.Strength, player.transform.position);
        }
        else if (collision.CompareTag("RangeEnemy"))
        {
            collision.GetComponent<RangeEnemy>().TakeDamage(player.Strength, player.transform.position);
        }
        else if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<Boss>().TakeDamage(player.Strength, transform.position);
        }
    }
}
