using System.Collections;
using System.IO;
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
    public PlayerMove player;
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
        // �κ��丮 â�� ������Ʈ�� ������ ��� �������� �����ϰ� �ٽ� �߰�
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
            ClickItem.onClick.AddListener(() => player.SRun());
            // ClickItem.onClick.AddListener(() => controller.RemoveItem());
           // ClickItem.onClick.AddListener(() => player.SRun());

            itemName.text = item.itemName;
            itemIcon.sprite = item.itemIcon;
        }
    }

    public void SaveInventoryToCloud()
    {
        // �κ��丮 ������ ������ JSON���� ��ȯ
        string jsonData = JsonUtility.ToJson(items);

        // GPGS�� ���� Ŭ���忡 ����
        GPGSBinder.Inst.SaveCloud("inventoryData", jsonData, success =>
        {
            if (success)
            {
                Debug.Log("�κ��丮 �����͸� Ŭ���忡 �����߽��ϴ�.");
            }
            else
            {
                Debug.LogError("�κ��丮 �����͸� Ŭ���忡 �����ϴµ� �����߽��ϴ�.");
            }
        });

        // JSON ���Ͽ��� ���� (���������� ����� �� ����)
        SaveInventoryToJson();
    }

    public void LoadInventoryFromCloud()
    {
        // GPGS�� ���� Ŭ���忡�� ������ �ҷ�����
        GPGSBinder.Inst.LoadCloud("inventoryData", (success, jsonData) =>
        {
            if (success)
            {
                // JSON �����͸� �κ��丮 ������ ����Ʈ�� ��ȯ
                items = JsonUtility.FromJson<List<Item>>(jsonData);

                Debug.Log("Ŭ���忡�� �κ��丮 �����͸� �ҷ��Խ��ϴ�.");

                // �κ��丮 ȭ�� ������Ʈ
                ListItems();
            }
            else
            {
                Debug.LogError("Ŭ���忡�� �κ��丮 �����͸� �ҷ����µ� �����߽��ϴ�.");
            }
        });
    }

    void SaveInventoryToJson()
    {
        // �κ��丮 ������ ������ JSON ���ڿ��� ��ȯ
        string jsonData = JsonUtility.ToJson(items);

        // JSON ���� ��� ����
        string filePath = Application.persistentDataPath + "/inventoryData.json";

        // JSON ���Ͽ� ����
        File.WriteAllText(filePath, jsonData);

        Debug.Log("�κ��丮 �����͸� JSON ���Ϸ� �����߽��ϴ�.");
    }

    void LoadInventoryFromJson()
    {
        // JSON ���� ��� ����
        string filePath = Application.persistentDataPath + "/inventoryData.json";

        // JSON ���Ͽ��� ������ �ҷ�����
        if (File.Exists(filePath))
        {
            // JSON ���Ͽ��� ������ �б�
            string jsonData = File.ReadAllText(filePath);

            // JSON �����͸� �κ��丮 ������ ����Ʈ�� ��ȯ
            items = JsonUtility.FromJson<List<Item>>(jsonData);

            Debug.Log("JSON ���Ͽ��� �κ��丮 �����͸� �ҷ��Խ��ϴ�.");

            // �κ��丮 ȭ�� ������Ʈ
            ListItems();
        }
        else
        {
            Debug.LogError("JSON ������ �������� �ʽ��ϴ�. �κ��丮 �����͸� �ҷ����µ� �����߽��ϴ�.");
        }
    }
}
