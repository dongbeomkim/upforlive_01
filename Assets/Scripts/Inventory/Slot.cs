using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
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

    //인벤토리 슬롯을 오른쪽 클릭했을 때
    //우선 빈 슬롯인지 확인하고 아니면 사용 혹은 장착 위해
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameObject go = eventData.pointerCurrentRaycast.gameObject;
            if (go.name == "SlotItem")
            {
                Slot slot = eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>();
                Item Uslot = slot.Item;
                if (Uslot == null)
                {
                    Debug.Log("빈 인벤토리 슬롯 입니다.");
                }
                else
                {
                    inventory.UseItem(Uslot);
                }
            }
        }
    }
}
