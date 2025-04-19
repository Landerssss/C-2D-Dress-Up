using UnityEngine;

[System.Serializable]
public class EquipmentItem
{
    public string itemName;
    public string characterType; // ��ʦ��, ������, etc.
    public EquipmentManager.EquipmentType equipmentType;
    public Sprite equipmentSprite;
    public int itemRarity = 1; //ϡ�жȣ�1=��ͨ��2=������3=ϡ�жȣ�
}
