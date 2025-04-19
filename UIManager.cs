using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject inventoryPanel;

    [Header("Navigation Buttons")]
    public Button openInventoryButton;
    public Button closeInventoryButton;

    private void Start()                     //≥ı ºªØ
    {
        if (mainPanel != null)
            mainPanel.SetActive(true);

        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);

        if (openInventoryButton != null)
            openInventoryButton.onClick.AddListener(OpenInventory);

        if (closeInventoryButton != null)
            closeInventoryButton.onClick.AddListener(CloseInventory);
    }

    private void OpenInventory()
    {
        if (mainPanel != null)
            mainPanel.SetActive(true);                        //True

        if (inventoryPanel != null)
            inventoryPanel.SetActive(true);
    }

    private void CloseInventory()
    {
        if (mainPanel != null)
            mainPanel.SetActive(true);

        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
    }
}
