// InventoryManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();
    public Transform itemContent;
    public GameObject inventoryItem;
    public InventoryItemController inventoryItemController;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        // 인벤토리 창을 업데이트할 때마다 모든 아이템을 삭제하고 다시 추가
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var controller = obj.GetComponent<InventoryItemController>();
            controller.item = item;
           // var player = obj.GetComponent<PlayerMove>();
            
            var itemName = obj.transform.Find("itemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();
            var ClickItem = obj.GetComponentInChildren<Button>();

            ClickItem.onClick.AddListener(() => controller.UseItem());
            // ClickItem.onClick.AddListener(() => controller.RemoveItem());
           // ClickItem.onClick.AddListener(() => player.SRun());

            itemName.text = item.itemName;
            itemIcon.sprite = item.itemIcon;
        }
    }

}
