using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public Image itemImage;
    public Image backgroundImage;
    public Text itemNameText;

    private EquipmentItem currentItem;
    private InventoryManager inventoryManager;

    [System.Obsolete]
    private void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void Setup(EquipmentItem item)
    {
        currentItem = item;

        if (itemImage != null && item.equipmentSprite != null)
            itemImage.sprite = item.equipmentSprite;


        if (itemNameText != null)
            itemNameText.text = item.itemName;
        if (backgroundImage != null)
        {
            switch (item.itemRarity)
            {
                case 1: 
                    backgroundImage.color = Color.white;
                    break;
                case 2:
                    backgroundImage.color = Color.green;
                    break;
                case 3: 
                    backgroundImage.color = Color.blue;
                    break;
                case 4: 
                    backgroundImage.color = new Color(0.5f, 0, 0.5f); // Purple
                    break;
                case 5: 
                    backgroundImage.color = Color.yellow;
                    break;
            }
        }
    }
    public EquipmentItem GetItem()
    {
        return currentItem;
    }
    //用作Debug
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick 触发，物品: " + (currentItem != null ? currentItem.itemName : "null"));
        if (currentItem != null && inventoryManager != null && inventoryManager.equipmentManager != null)
        {
            inventoryManager.equipmentManager.EquipItem(currentItem);
            Debug.Log("Equipped item: " + currentItem.itemName);
        }
        else
        {
            Debug.LogWarning("Failed to equip item. currentItem: " + (currentItem == null) +
                            ", inventoryManager: " + (inventoryManager == null) +
                            ", equipmentManager: " + (inventoryManager?.equipmentManager == null));
        }
    }
}