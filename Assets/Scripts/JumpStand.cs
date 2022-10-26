using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpStand : MonoBehaviour
{

    

    float jump = 15f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D go = collision.gameObject.GetComponent<Rigidbody2D>();
            go.AddForce(transform.up * jump, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        
    }
}
