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
//        // ������ �ʱ�ȭ
//        InitializeInventory();
//        // �κ��丮 UI ����
//        UpdateInventoryUI();
//    }

//    void InitializeInventory()
//    {
//        // �׽�Ʈ�� ������ ���� �� �߰�
//        inventory.Add(new Item("shoes", "Running faster", ItemType.equipment));
//        inventory.Add(new Item("redBull", "Speed potion", ItemType.consumable));
//    }

//    void UpdateInventoryUI()
//    {
//        // ���� �κ��丮 UI ����
//        foreach (Transform child in inventoryPanel)
//        {
//            Destroy(child.gameObject);
//        }

//        // ���ο� �κ��丮 UI ����
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
