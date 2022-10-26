using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    /* * * * * * * * * * 인벤토리 * * * * * * * * * */
    //아이템을 담을 리스트
    public List<Item> items;

    //Slot의 부모가 되는 Bag를 담을 곳
    [SerializeField]
    Transform slotParent;

    //Bag의 하위에 등록된 슬롯들을 담을 곳
    [SerializeField]
    Slot[] slots;


    /* * * * * * * * * * 장비 슬롯 * * * * * * * * * */
    //장비 아이템을 담을 리스트
    public List<Item> equipment;

    //EquipmentSlot의 부모가 되는 StateAndEquipment을 담을 곳
    [SerializeField]
    Transform equipmentslotParent;

    //StateAndEquipment의 하위에 등록된 슬롯들을 담을 곳
    [SerializeField]
    public EquipmentSlot eqiupSlot;


    /* * * * * * * * * * 스킬 이미지 * * * * * * * * * */
    public Image dashicon;


    //OnValidate()는 유니티 에디터에서 바로 작동을 하는 역할
    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
        eqiupSlot = equipmentslotParent.GetComponentInChildren<EquipmentSlot>();
    }

    /* * * * * * * * * * 장비 정보 * * * * * * * * * */
    Text itemName;
    Text itemAttackPower;
    Text itemDefense;


    /* * * * * * * * * * 스탯 정보 * * * * * * * * * */
    Text playerName;
    Text playerAttackPower;
    Text playerDefense;

    private void Awake()
    {
        FreshSlot();

        itemName = transform.GetChild(1).GetChild(1).GetComponent<Text>();
        itemAttackPower = transform.GetChild(1).GetChild(3).GetComponent<Text>();
        itemDefense = transform.GetChild(1).GetChild(4).GetComponent<Text>();

        playerName = transform.GetChild(1).GetChild(5).GetComponent<Text>();
        playerAttackPower = transform.GetChild(1).GetChild(6).GetComponent<Text>();
        playerDefense = transform.GetChild(1).GetChild(7).GetComponent<Text>();

        playerName.text = DataManager.instance.nowPlayer.name;
        //playerAttackPower.text = GameManager.Instance.Player.Strength.ToString();
        playerDefense.text = GameManager.Instance.Player.Defense.ToString();

        dashicon = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>();
    }

    //아이템이 들어오거나 나가면 Slot의 내용을 다시 정리해서 화면에 보여주는 역할
    public void FreshSlot()
    {
        //같은 i 값을 사용하기 위해 위부에 선언
        int i = 0;

        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].Item = items[i];
        }

        for (; i < slots.Length; i++)
        {
            slots[i].Item = null;
        }


        //다른 j 값을 사용해서 장비 슬롯 1개의 내용을 다시 정리해서 화면에 보여주는 역할
        //불완전한 것 같음, 그래서 EquipSlotItem 매서드에 eqiupSlot.Item = null;을 넣음
        int j = 0;

        for (; j < equipment.Count; j++)
        {
            eqiupSlot.Item = equipment[j];
        }
    }

    //아이템을 어떤 방식으로 획득해서 인벤토리에 추가하는 역할
    public void AddItem(Item item, Collider2D collider)
    {
        if (items.Count < slots.Length)
        {
            items.Add(item);
            Destroy(collider.gameObject);
            FreshSlot();
        }
        else
        {
            Debug.Log("슬롯이 가득 차 있습니다.");
        }
    }

    //장비 슬롯 누를 때
    public void EquipSlotItem(Item item)
    {
        //데이터 부분
        equipment.Remove(item);
        GameManager.Instance.Player.OffEquip(item.power);
        eqiupSlot.Item = null;
        items.Add(item);
        FreshSlot();

        //장비 UI부분
        itemName.text = null;
        itemAttackPower.text = null;
        itemDefense.text = null;

        //캐릭터 스탯 UI부분
        playerAttackPower.text = GameManager.Instance.Player.deal.ToString();
        playerDefense.text = GameManager.Instance.Player.Defense.ToString();
    }

    //인벤토리 슬롯 누를 때
    public void UseItem(Item item)
    {
        if (item.equipment)
        {
            if (eqiupSlot.Item == null)
            {
                //데이터 부분
                equipment.Add(item);
                items.Remove(item);
                GameManager.Instance.Player.OnEquip(item.power);
                FreshSlot();

                //장비 UI부분
                itemName.text = item.itemName;
                itemAttackPower.text = item.power.ToString();
                itemDefense.text = item.defense.ToString();

                //캐릭터 스탯 UI부분
                playerAttackPower.text = (GameManager.Instance.Player.deal).ToString();
                playerDefense.text = (GameManager.Instance.Player.Defense + item.defense).ToString();
            }
            else
            {
                Debug.Log("이미 장비 슬롯에 장비가 있습니다.");
            }
        }
        else if (item.Expendables)
        {
            GameManager.Instance.Player.PlayerHP += item.heal;
            items.Remove(item);
            FreshSlot();
        }
        else
        {

            Debug.Log($"{item.itemName}");
        }
    }

    private void Update()
    {
        
    }
}
