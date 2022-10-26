using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{

    public int startPoint;


    void Start()
    {
        if(startPoint == GameManager.Instance.Player.currentMap)
        {
            GameManager.Instance.Player.transform.position = this.transform.position;
        }
    }

    
    void Update()
    {
        
    }
}
