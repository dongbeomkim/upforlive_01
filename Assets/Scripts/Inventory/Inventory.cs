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
    /* * * * * * * * * * �κ��丮 * * * * * * * * * */
    //�������� ���� ����Ʈ
    public List<Item> items;

    //Slot�� �θ� �Ǵ� Bag�� ���� ��
    [SerializeField]
    Transform slotParent;

    //Bag�� ������ ��ϵ� ���Ե��� ���� ��
    [SerializeField]
    Slot[] slots;


    /* * * * * * * * * * ��� ���� * * * * * * * * * */
    //��� �������� ���� ����Ʈ
    public List<Item> equipment;

    //EquipmentSlot�� �θ� �Ǵ� StateAndEquipment�� ���� ��
    [SerializeField]
    Transform equipmentslotParent;

    //StateAndEquipment�� ������ ��ϵ� ���Ե��� ���� ��
    [SerializeField]
    public EquipmentSlot eqiupSlot;


    /* * * * * * * * * * ��ų �̹��� * * * * * * * * * */
    public Image dashicon;


    //OnValidate()�� ����Ƽ �����Ϳ��� �ٷ� �۵��� �ϴ� ����
    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
        eqiupSlot = equipmentslotParent.GetComponentInChildren<EquipmentSlot>();
    }

    /* * * * * * * * * * ��� ���� * * * * * * * * * */
    Text itemName;
    Text itemAttackPower;
    Text itemDefense;


    /* * * * * * * * * * ���� ���� * * * * * * * * * */
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

    //�������� �����ų� ������ Slot�� ������ �ٽ� �����ؼ� ȭ�鿡 �����ִ� ����
    public void FreshSlot()
    {
        //���� i ���� ����ϱ� ���� ���ο� ����
        int i = 0;

        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].Item = items[i];
        }

        for (; i < slots.Length; i++)
        {
            slots[i].Item = null;
        }


        //�ٸ� j ���� ����ؼ� ��� ���� 1���� ������ �ٽ� �����ؼ� ȭ�鿡 �����ִ� ����
        //�ҿ����� �� ����, �׷��� EquipSlotItem �ż��忡 eqiupSlot.Item = null;�� ����
        int j = 0;

        for (; j < equipment.Count; j++)
        {
            eqiupSlot.Item = equipment[j];
        }
    }

    //�������� � ������� ȹ���ؼ� �κ��丮�� �߰��ϴ� ����
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
            Debug.Log("������ ���� �� �ֽ��ϴ�.");
        }
    }

    //��� ���� ���� ��
    public void EquipSlotItem(Item item)
    {
        //������ �κ�
        equipment.Remove(item);
        GameManager.Instance.Player.OffEquip(item.power);
        eqiupSlot.Item = null;
        items.Add(item);
        FreshSlot();

        //��� UI�κ�
        itemName.text = null;
        itemAttackPower.text = null;
        itemDefense.text = null;

        //ĳ���� ���� UI�κ�
        playerAttackPower.text = GameManager.Instance.Player.deal.ToString();
        playerDefense.text = GameManager.Instance.Player.Defense.ToString();
    }

    //�κ��丮 ���� ���� ��
    public void UseItem(Item item)
    {
        if (item.equipment)
        {
            if (eqiupSlot.Item == null)
            {
                //������ �κ�
                equipment.Add(item);
                items.Remove(item);
                GameManager.Instance.Player.OnEquip(item.power);
                FreshSlot();

                //��� UI�κ�
                itemName.text = item.itemName;
                itemAttackPower.text = item.power.ToString();
                itemDefense.text = item.defense.ToString();

                //ĳ���� ���� UI�κ�
                playerAttackPower.text = (GameManager.Instance.Player.deal).ToString();
                playerDefense.text = (GameManager.Instance.Player.Defense + item.defense).ToString();
            }
            else
            {
                Debug.Log("�̹� ��� ���Կ� ��� �ֽ��ϴ�.");
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
