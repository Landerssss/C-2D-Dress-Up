using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    [System.Serializable]
    public class EquipmentSlot
    {
        public EquipmentType type;
        public Image equipmentImage;
        public Sprite defaultSprite;
    }

    public enum EquipmentType
    {
        Hat,      // ñ��
        Top,      // ��װ
        Bottom,   // ��װ
        Inner,    // ����
        Outer,    // ����
        Shoes,    // Ь��
        Accessory, // ����
        Tool      // ����
    }

    [Header("Character Display")]
    public Image characterDisplay;
    public Image hatDisplay;
    public Image topDisplay;
    public Image bottomDisplay;
    public Image innerDisplay;
    public Image outerDisplay;
    public Image shoesDisplay;
    public Image accessoryDisplay;
    public Image toolDisplay;

    [Header("Equipment Slots")]
    public List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();

    [Header("Default Mage Equipment")]
    public Sprite defaultMageCharacter;
    public Sprite defaultMageHat;
    public Sprite defaultMageTop;
    public Sprite defaultMageBottom;
    public Sprite defaultMageInner;
    public Sprite defaultMageOuter;
    public Sprite defaultMageShoes;
    public Sprite defaultMageAccessory;
    public Sprite defaultMageTool;

    private void Start()
    {
        SetDefaultMageEquipment();       //���ý�ɫΪĬ�Ϸ�ʦװ�������½�ɫ��ۺ�װ����ͼ�ꡣ
    }

    public void SetDefaultMageEquipment()
    {
        if (characterDisplay != null && defaultMageCharacter != null)                // ����Ĭ�Ϸ�ʦ��ɫ
            characterDisplay.sprite = defaultMageCharacter;

        if (hatDisplay != null && defaultMageHat != null)                            // ����Ĭ��װ���Ӿ�Ч��
            hatDisplay.sprite = defaultMageHat;

        if (topDisplay != null && defaultMageTop != null)
            topDisplay.sprite = defaultMageTop;

        if (bottomDisplay != null && defaultMageBottom != null)
            bottomDisplay.sprite = defaultMageBottom;

        if (innerDisplay != null && defaultMageInner != null)
            innerDisplay.sprite = defaultMageInner;

        if (outerDisplay != null && defaultMageOuter != null)
            outerDisplay.sprite = defaultMageOuter;

        if (shoesDisplay != null && defaultMageShoes != null)
            shoesDisplay.sprite = defaultMageShoes;

        if (accessoryDisplay != null && defaultMageAccessory != null)
            accessoryDisplay.sprite = defaultMageAccessory;

        if (toolDisplay != null && defaultMageTool != null)
            toolDisplay.sprite = defaultMageTool;

        foreach (var slot in equipmentSlots)                                // ����װ����λͼ��
        {
            switch (slot.type)
            {
                case EquipmentType.Hat:
                    slot.equipmentImage.sprite = defaultMageHat;
                    break;
                case EquipmentType.Top:
                    slot.equipmentImage.sprite = defaultMageTop;
                    break;
                case EquipmentType.Bottom:
                    slot.equipmentImage.sprite = defaultMageBottom;
                    break;
                case EquipmentType.Inner:
                    slot.equipmentImage.sprite = defaultMageInner;
                    break;
                case EquipmentType.Outer:
                    slot.equipmentImage.sprite = defaultMageOuter;
                    break;
                case EquipmentType.Shoes:
                    slot.equipmentImage.sprite = defaultMageShoes;
                    break;
                case EquipmentType.Accessory:
                    slot.equipmentImage.sprite = defaultMageAccessory;
                    break;
                case EquipmentType.Tool:
                    slot.equipmentImage.sprite = defaultMageTool;
                    break;
            }
        }
    }

    public void EquipItem(EquipmentItem item)
    {
        switch (item.equipmentType)                                      // �������
        {
            case EquipmentType.Hat:
                if (hatDisplay != null)
                    hatDisplay.sprite = item.equipmentSprite;
                break;
            case EquipmentType.Top:
                if (topDisplay != null)
                    topDisplay.sprite = item.equipmentSprite;
                break;
            case EquipmentType.Bottom:
                if (bottomDisplay != null)
                    bottomDisplay.sprite = item.equipmentSprite;
                break;
            case EquipmentType.Inner:
                if (innerDisplay != null)
                    innerDisplay.sprite = item.equipmentSprite;
                break;
            case EquipmentType.Outer:
                if (outerDisplay != null)
                    outerDisplay.sprite = item.equipmentSprite;
                break;
            case EquipmentType.Shoes:
                if (shoesDisplay != null)
                    shoesDisplay.sprite = item.equipmentSprite;
                break;
            case EquipmentType.Accessory:
                if (accessoryDisplay != null)
                    accessoryDisplay.sprite = item.equipmentSprite;
                break;
            case EquipmentType.Tool:
                if (toolDisplay != null)
                    toolDisplay.sprite = item.equipmentSprite;
                break;
        }
        foreach (var slot in equipmentSlots)             //����װ����λͼƬ
        {
            if (slot.type == item.equipmentType)
            {
                slot.equipmentImage.sprite = item.equipmentSprite;
                break;
            }
        }
    }
}
