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
            ClickItem.onClick.AddListener(() => player.SRun());
            // ClickItem.onClick.AddListener(() => controller.RemoveItem());
           // ClickItem.onClick.AddListener(() => player.SRun());

            itemName.text = item.itemName;
            itemIcon.sprite = item.itemIcon;
        }
    }

    public void SaveInventoryToCloud()
    {
        // 인벤토리 아이템 정보를 JSON으로 변환
        string jsonData = JsonUtility.ToJson(items);

        // GPGS를 통해 클라우드에 저장
        GPGSBinder.Inst.SaveCloud("inventoryData", jsonData, success =>
        {
            if (success)
            {
                Debug.Log("인벤토리 데이터를 클라우드에 저장했습니다.");
            }
            else
            {
                Debug.LogError("인벤토리 데이터를 클라우드에 저장하는데 실패했습니다.");
            }
        });

        // JSON 파일에도 저장 (선택적으로 사용할 수 있음)
        SaveInventoryToJson();
    }

    public void LoadInventoryFromCloud()
    {
        // GPGS를 통해 클라우드에서 데이터 불러오기
        GPGSBinder.Inst.LoadCloud("inventoryData", (success, jsonData) =>
        {
            if (success)
            {
                // JSON 데이터를 인벤토리 아이템 리스트로 변환
                items = JsonUtility.FromJson<List<Item>>(jsonData);

                Debug.Log("클라우드에서 인벤토리 데이터를 불러왔습니다.");

                // 인벤토리 화면 업데이트
                ListItems();
            }
            else
            {
                Debug.LogError("클라우드에서 인벤토리 데이터를 불러오는데 실패했습니다.");
            }
        });
    }

    void SaveInventoryToJson()
    {
        // 인벤토리 아이템 정보를 JSON 문자열로 변환
        string jsonData = JsonUtility.ToJson(items);

        // JSON 파일 경로 설정
        string filePath = Application.persistentDataPath + "/inventoryData.json";

        // JSON 파일에 저장
        File.WriteAllText(filePath, jsonData);

        Debug.Log("인벤토리 데이터를 JSON 파일로 저장했습니다.");
    }

    void LoadInventoryFromJson()
    {
        // JSON 파일 경로 설정
        string filePath = Application.persistentDataPath + "/inventoryData.json";

        // JSON 파일에서 데이터 불러오기
        if (File.Exists(filePath))
        {
            // JSON 파일에서 데이터 읽기
            string jsonData = File.ReadAllText(filePath);

            // JSON 데이터를 인벤토리 아이템 리스트로 변환
            items = JsonUtility.FromJson<List<Item>>(jsonData);

            Debug.Log("JSON 파일에서 인벤토리 데이터를 불러왔습니다.");

            // 인벤토리 화면 업데이트
            ListItems();
        }
        else
        {
            Debug.LogError("JSON 파일이 존재하지 않습니다. 인벤토리 데이터를 불러오는데 실패했습니다.");
        }
    }
}
