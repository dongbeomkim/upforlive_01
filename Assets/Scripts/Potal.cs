using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Potal : MonoBehaviour
{

    ParticleSystem pS;
    public int mapName;

    private void Start()
    {
        pS = transform.GetComponentInChildren<ParticleSystem>();
        InvokeRepeating("OnPotalEffect",4,4);
    }

    void OnPotalEffect()
    {
        pS.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            SceneLoad.LoadScene(mapName);
    }
}
