using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class bulletOfenemy : MonoBehaviour
{

    float Speed = 3f;
    int bulletPower = 3;

    Player player;
    Vector2 dir;
    float pos;

    void Start()
    {
        player = FindObjectOfType<Player>();
        dir = player.transform.position - transform.position;
        Invoke("DestroyBullet", 3f);
    }

    
    void Update()
    {
        if(dir.x > 0)
        {
            pos = 2f;
        }
        else if(dir.x < 0)
        {
            pos = -2f;
        }

        transform.Translate(transform.right * pos * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.TakeDamage(bulletPower, transform.position);
            Destroy(gameObject);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
