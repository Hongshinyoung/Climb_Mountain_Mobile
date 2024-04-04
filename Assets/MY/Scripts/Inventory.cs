//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;

//public class InventorySystem : MonoBehaviour
//{
//    public GameObject inventorySlotPrefab;
//    public Transform inventoryPanel;

//    private List<Item> inventory = new List<Item>();

//    void Start()
//    {
//        // 아이템 초기화
//        InitializeInventory();
//        // 인벤토리 UI 생성
//        UpdateInventoryUI();
//    }

//    void InitializeInventory()
//    {
//        // 테스트용 아이템 생성 및 추가
//        inventory.Add(new Item("shoes", "Running faster", ItemType.equipment));
//        inventory.Add(new Item("redBull", "Speed potion", ItemType.consumable));
//    }

//    void UpdateInventoryUI()
//    {
//        // 기존 인벤토리 UI 제거
//        foreach (Transform child in inventoryPanel)
//        {
//            Destroy(child.gameObject);
//        }

//        // 새로운 인벤토리 UI 생성
//        foreach (Item item in inventory)
//        {
//            GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);
//            slot.transform.Find("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(item.itemName);
//            slot.transform.Find("ItemName").GetComponent<Text>().text = item.itemName;
//        }
//    }
//}

//public enum ItemType
//{
//    equipment,
//    consumable
//}

//public class Item
//{
//    public string itemName;
//    public string description;
//    public ItemType type;

//    public Item(string name, string desc, ItemType itemType)
//    {
//        itemName = name;
//        description = desc;
//        type = itemType;
//    }
//}
