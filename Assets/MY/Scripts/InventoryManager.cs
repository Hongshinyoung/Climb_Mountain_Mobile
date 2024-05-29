using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();
    public Transform itemContent;
    public GameObject inventoryItem;
    private FirebaseManager firebaseManager;
    private string userId;

    private void Awake()
    {
        Instance = this;
        firebaseManager = FindObjectOfType<FirebaseManager>();
    }

    public void SetUserId(string userId)
    {
        this.userId = userId;
        LoadInventoryData();
    }

    public void Add(Item item)
    {
        items.Add(item);
        SaveInventoryData();
        ListItems(); // Update the UI whenever an item is added
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        SaveInventoryData();
        ListItems(); // Update the UI whenever an item is removed
    }

    public void ListItems()
    {
        // Clear existing items in the UI
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        // Populate UI with current inventory items
        foreach (var item in items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var controller = obj.GetComponent<InventoryItemController>();
            controller.item = item;

            var itemName = obj.transform.Find("itemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();
            var clickItem = obj.GetComponentInChildren<Button>();

            clickItem.onClick.AddListener(() => controller.UseItem());

            itemName.text = item.itemName;
            itemIcon.sprite = item.itemIcon;
        }
    }

    private void SaveInventoryData()
    {
        if (!string.IsNullOrEmpty(userId))
        {
            string json = JsonUtility.ToJson(new ItemListWrapper { items = items });
            firebaseManager.SaveInventoryData(userId, json);
        }
    }

    private void LoadInventoryData()
    {
        if (!string.IsNullOrEmpty(userId))
        {
            firebaseManager.LoadInventoryData(userId, (loadedItems) =>
            {
                if (loadedItems != null)
                {
                    items = loadedItems.items;
                    ListItems(); // Update the UI after loading the inventory
                }
            });
        }
    }

    [System.Serializable]
    public class ItemListWrapper
    {
        public List<Item> items;
    }
}
