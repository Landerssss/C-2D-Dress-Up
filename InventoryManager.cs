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
        // 从资源文件夹加载所有装备精灵
        LoadItemsFromCategory("法师男", EquipmentManager.EquipmentType.Hat, "tou_");
        LoadItemsFromCategory("法师男", EquipmentManager.EquipmentType.Top, "shang_");
        LoadItemsFromCategory("法师男", EquipmentManager.EquipmentType.Bottom, "xia_");
        LoadItemsFromCategory("法师男", EquipmentManager.EquipmentType.Outer, "wai_");
        LoadItemsFromCategory("法师男", EquipmentManager.EquipmentType.Shoes, "xie_");

        LoadItemsFromCategory("海盗男", EquipmentManager.EquipmentType.Hat, "tou_");
        LoadItemsFromCategory("海盗男", EquipmentManager.EquipmentType.Top, "shang_");
        LoadItemsFromCategory("海盗男", EquipmentManager.EquipmentType.Bottom, "xia_");
        LoadItemsFromCategory("海盗男", EquipmentManager.EquipmentType.Outer, "wai_");
        LoadItemsFromCategory("海盗男", EquipmentManager.EquipmentType.Shoes, "xie_");

        LoadItemsFromCategory("牛仔男", EquipmentManager.EquipmentType.Hat, "tou_");
        LoadItemsFromCategory("牛仔男", EquipmentManager.EquipmentType.Top, "shang_");
        LoadItemsFromCategory("牛仔男", EquipmentManager.EquipmentType.Bottom, "xia_");
        LoadItemsFromCategory("牛仔男", EquipmentManager.EquipmentType.Outer, "wai_");
        LoadItemsFromCategory("牛仔男", EquipmentManager.EquipmentType.Shoes, "xie_");

        LoadItemsFromCategory("精灵男", EquipmentManager.EquipmentType.Hat, "tou_");
        LoadItemsFromCategory("精灵男", EquipmentManager.EquipmentType.Top, "shang_");
        LoadItemsFromCategory("精灵男", EquipmentManager.EquipmentType.Bottom, "xia_");
        LoadItemsFromCategory("精灵男", EquipmentManager.EquipmentType.Outer, "wai_");
        LoadItemsFromCategory("精灵男", EquipmentManager.EquipmentType.Shoes, "xie_");
    }

    private void LoadItemsFromCategory(string characterType, EquipmentManager.EquipmentType equipType, string prefix)
    {
        string path = $"换装/{characterType}"; // 移除 prefix，直接加载角色文件夹
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        Debug.Log("从路径 " + path + " 加载了 " + sprites.Length + " 个精灵");

        string typePrefix = prefix; // 保持原有的 prefix，如 "tou_"1
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
                Debug.Log("添加物品: " + newItem.itemName);
            }
        }
    }


    private void FilterByType(EquipmentManager.EquipmentType type)
    {
        showingAllItems = false;
        currentFilter = type;
        //按所选类型筛选物品
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
        Debug.Log("ShowAllItems: allItems 数量: " + allItems.Count + ", filteredItems 数量: " + filteredItems.Count);
        UpdateTotalPages();
        DisplayCurrentPage();
    }

    private void DisplayCurrentPage()
    {
        Debug.Log("DisplayCurrentPage: 第 " + currentPage + " 页，filteredItems 数量: " + filteredItems.Count);
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }
        int startIndex = (currentPage - 1) * itemsPerPage;
        int endIndex = Mathf.Min(startIndex + itemsPerPage, filteredItems.Count);
        Debug.Log("起始索引: " + startIndex + ", 结束索引: " + endIndex);

        for (int i = startIndex; i < endIndex; i++)
        {
            EquipmentItem item = filteredItems[i];
            GameObject itemSlotObj = Instantiate(itemSlotPrefab, itemsContainer);
            ItemSlot itemSlot = itemSlotObj.GetComponent<ItemSlot>();
            if (itemSlot != null)
            {
                itemSlot.Setup(item);
                Debug.Log("生成了物品槽: " + item.itemName);
            }
            else
            {
                Debug.LogWarning("ItemSlot 组件缺失");
            }
        }
        UpdatePageText();

    }
}