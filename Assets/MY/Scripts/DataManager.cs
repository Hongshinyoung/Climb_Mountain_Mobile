using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private const string saveFileName = "gold_data.json"; // JSON 파일 이름

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // DataManager 오브젝트를 다음 씬으로 전달합니다.
    }
    public void SaveInventoryData() //인벤 -> json
    {
        List<Item> invenData = new List<Item>();
        invenData = InventoryManager.Instance.items;
        string jsonData = JsonUtility.ToJson(invenData);
        File.WriteAllText(saveFileName, jsonData);
    }

    public void SaveGoldData() // gold -> json
    {
        int gold = Gold.Instance.GetGold(); // Gold 스크립트의 gold 변수에 접근합니다.
        GoldData data = new GoldData();
        data.gold = gold;

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(GetSaveFilePath(), jsonData);

        SaveToCloud(jsonData);
    }

    public void LoadGoldData() //json -> gold
    {
        string filePath = GetSaveFilePath();
        if(File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            GoldData data  = JsonUtility.FromJson<GoldData>(jsonData);
            int gold = data.gold;
            Gold.Instance.SetGold(gold);
            LoadFromCloud(jsonData);
        }
    }

    private string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, saveFileName);
    }

    private void SaveToCloud(string jsonData)
    {
        GPGSBinder.Inst.SaveCloud(saveFileName, jsonData);
    }
    private void LoadFromCloud(string saveFileName)
    {
        GPGSBinder.Inst.LoadCloud(saveFileName);
    }
}

[System.Serializable]
public class GoldData
{
    public int gold;
}
