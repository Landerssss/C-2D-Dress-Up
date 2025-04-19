using UnityEngine;

[System.Serializable]
public class EquipmentItem
{
    public string itemName;
    public string characterType; // 法师男, 海盗男, etc.
    public EquipmentManager.EquipmentType equipmentType;
    public Sprite equipmentSprite;
    public int itemRarity = 1; //稀有度（1=普通，2=精良，3=稀有度）
}
