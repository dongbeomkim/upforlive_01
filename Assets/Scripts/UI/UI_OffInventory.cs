using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OffInventory : MonoBehaviour
{

    GameObject Inventory;

    void Start()
    {
        Inventory = GameObject.Find("Inventory").gameObject;
    }

    
    void Update()
    {
        
    }

    public void OffInven()
    {
        Inventory.SetActive(false);
    }
}
