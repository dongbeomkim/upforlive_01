using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;

    Item item;
    public Item Item
    {
        get => item;
        set
        {
            item = value;
            if (item != null)
            {
                image.sprite = Item.itemImage;
                image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }

    Inventory inventory;


    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    //장비아이템 슬롯을 오른쪽 클릭했을 때
    //우선 빈 슬롯인지 확인하고 아니면 해제하기 위해
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameObject go = eventData.pointerCurrentRaycast.gameObject;
            if (go.name == "EquipmentSlotItem")
            {
                EquipmentSlot slot = eventData.pointerCurrentRaycast.gameObject.GetComponent<EquipmentSlot>();
                Item Eslot = slot.Item;
                if (Eslot == null)
                {
                    Debug.Log("빈 장비 슬롯 입니다.");
                }
                else
                {
                    inventory.EquipSlotItem(Eslot);
                }
            }
        }
    }
}
