using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ReturnGameButton : MonoBehaviour
{

    GameObject MenuPanel;

    private void Awake()
    {
        MenuPanel = GameObject.Find("PauseUI").gameObject;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OffMenu()
    {
        MenuPanel.SetActive(false);
    }
}
