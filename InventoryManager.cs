using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory UI")]
    public GameObject inventoryPanel;
    public Transform itemsContainer;
    public GameObject itemSlotPrefab;

    [Header("Category Buttons")]
    public Button allButton;
    public Button hatButton;
    public Button topButton;
    public Button bottomButton;
    public Button innerButton;
    public Button outerButton;
    public Button shoesButton;
    public Button accessoryButton;
    public Button toolButton;

    [Header("Pagination")]
    public TextMeshProUGUI pageText;
    public Button prevPageButton;
    public Button nextPageButton;
    public int itemsPerPage = 30;

    [Header("Equipment Manager Reference")]
    public EquipmentManager equipmentManager;

    private List<EquipmentItem> allItems = new List<EquipmentItem>();
    private List<EquipmentItem> filteredItems = new List<EquipmentItem>();
    private int currentPage = 1;
    private int totalPages = 1;
    private EquipmentManager.EquipmentType currentFilter = EquipmentManager.EquipmentType.Hat;
    private bool showingAllItems = true;


    private void Start()
    {
        LoadInventoryItems();
        InitializeButtons();
        ShowAllItems();
    }

    private void InitializeButtons()
    {
        if (allButton != null)
            allButton.onClick.AddListener(ShowAllItems);

        if (hatButton != null)
            hatButton.onClick.AddListener(() => FilterByType(EquipmentManager.EquipmentType.Hat));

        if (topButton != null)
            topButton.onClick.AddListener(() => FilterByType(EquipmentManager.EquipmentType.Top));

        if (bottomButton != null)
            bottomButton.onClick.AddListener(() => FilterByType(EquipmentManager.EquipmentType.Bottom));

        if (innerButton != null)
            innerButton.onClick.AddListener(() => FilterByType(EquipmentManager.EquipmentType.Inner));

        if (outerButton != null)
            outerButton.onClick.AddListener(() => FilterByType(EquipmentManager.EquipmentType.Outer));

        if (shoesButton != null)
            shoesButton.onClick.AddListener(() => FilterByType(EquipmentManager.EquipmentType.Shoes));

        if (accessoryButton != null)
            accessoryButton.onClick.AddListener(() => FilterByType(EquipmentManager.EquipmentType.Accessory));

        if (toolButton != null)
            toolButton.onClick.AddListener(() => FilterByType(EquipmentManager.EquipmentType.Tool));

        if (prevPageButton != null)
            prevPageButton.onClick.AddListener(PreviousPage);

        if (nextPageButton != null)
            nextPageButton.onClick.AddListener(NextPage);
    }

    private void LoadInventoryItems()
    {
        // ����Դ�ļ��м�������װ������
        LoadItemsFromCategory("��ʦ��", EquipmentManager.EquipmentType.Hat, "tou_");
        LoadItemsFromCategory("��ʦ��", EquipmentManager.EquipmentType.Top, "shang_");
        LoadItemsFromCategory("��ʦ��", EquipmentManager.EquipmentType.Bottom, "xia_");
        LoadItemsFromCategory("��ʦ��", EquipmentManager.EquipmentType.Outer, "wai_");
        LoadItemsFromCategory("��ʦ��", EquipmentManager.EquipmentType.Shoes, "xie_");

        LoadItemsFromCategory("������", EquipmentManager.EquipmentType.Hat, "tou_");
        LoadItemsFromCategory("������", EquipmentManager.EquipmentType.Top, "shang_");
        LoadItemsFromCategory("������", EquipmentManager.EquipmentType.Bottom, "xia_");
        LoadItemsFromCategory("������", EquipmentManager.EquipmentType.Outer, "wai_");
        LoadItemsFromCategory("������", EquipmentManager.EquipmentType.Shoes, "xie_");

        LoadItemsFromCategory("ţ����", EquipmentManager.EquipmentType.Hat, "tou_");
        LoadItemsFromCategory("ţ����", EquipmentManager.EquipmentType.Top, "shang_");
        LoadItemsFromCategory("ţ����", EquipmentManager.EquipmentType.Bottom, "xia_");
        LoadItemsFromCategory("ţ����", EquipmentManager.EquipmentType.Outer, "wai_");
        LoadItemsFromCategory("ţ����", EquipmentManager.EquipmentType.Shoes, "xie_");

        LoadItemsFromCategory("������", EquipmentManager.EquipmentType.Hat, "tou_");
        LoadItemsFromCategory("������", EquipmentManager.EquipmentType.Top, "shang_");
        LoadItemsFromCategory("������", EquipmentManager.EquipmentType.Bottom, "xia_");
        LoadItemsFromCategory("������", EquipmentManager.EquipmentType.Outer, "wai_");
        LoadItemsFromCategory("������", EquipmentManager.EquipmentType.Shoes, "xie_");
    }

    private void LoadItemsFromCategory(string characterType, EquipmentManager.EquipmentType equipType, string prefix)
    {
        string path = $"��װ/{characterType}"; // �Ƴ� prefix��ֱ�Ӽ��ؽ�ɫ�ļ���
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        Debug.Log("��·�� " + path + " ������ " + sprites.Length + " ������");

        string typePrefix = prefix; // ����ԭ�е� prefix���� "tou_"1
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name.StartsWith(typePrefix))
            {
                EquipmentItem newItem = new EquipmentItem
                {
                    itemName = $"{characterType}_{equipType}_{i + 1}",
                    characterType = characterType,
                    equipmentType = equipType,
                    equipmentSprite = sprites[i],
                    itemRarity = 1
                };
                allItems.Add(newItem);
                Debug.Log("�����Ʒ: " + newItem.itemName);
            }
        }
    }


    private void FilterByType(EquipmentManager.EquipmentType type)
    {
        showingAllItems = false;
        currentFilter = type;
        //����ѡ����ɸѡ��Ʒ
        filteredItems = allItems.FindAll(item => item.equipmentType == type);

        currentPage = 1;
        UpdateTotalPages();
        DisplayCurrentPage();
    }

    private void UpdateTotalPages()
    {
        totalPages = Mathf.CeilToInt((float)filteredItems.Count / itemsPerPage);
        if (totalPages < 1) totalPages = 1;

        UpdatePageText();
    }

    private void UpdatePageText()
    {
        if (pageText != null)
            pageText.text = currentPage + "/" + totalPages;
    }

    private void PreviousPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
            DisplayCurrentPage();
        }
    }

    private void NextPage()
    {
        if (currentPage < totalPages)
        {
            currentPage++;
            DisplayCurrentPage();
        }
    }
    private void ShowAllItems()
    {
        filteredItems = new List<EquipmentItem>(allItems);
        currentPage = 1;
        Debug.Log("ShowAllItems: allItems ����: " + allItems.Count + ", filteredItems ����: " + filteredItems.Count);
        UpdateTotalPages();
        DisplayCurrentPage();
    }

    private void DisplayCurrentPage()
    {
        Debug.Log("DisplayCurrentPage: �� " + currentPage + " ҳ��filteredItems ����: " + filteredItems.Count);
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }
        int startIndex = (currentPage - 1) * itemsPerPage;
        int endIndex = Mathf.Min(startIndex + itemsPerPage, filteredItems.Count);
        Debug.Log("��ʼ����: " + startIndex + ", ��������: " + endIndex);

        for (int i = startIndex; i < endIndex; i++)
        {
            EquipmentItem item = filteredItems[i];
            GameObject itemSlotObj = Instantiate(itemSlotPrefab, itemsContainer);
            ItemSlot itemSlot = itemSlotObj.GetComponent<ItemSlot>();
            if (itemSlot != null)
            {
                itemSlot.Setup(item);
                Debug.Log("��������Ʒ��: " + item.itemName);
            }
            else
            {
                Debug.LogWarning("ItemSlot ���ȱʧ");
            }
        }
        UpdatePageText();

    }
}