using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private const string saveFileGold = "gold_data.json"; // JSON ���� �̸�
    private const string saveFileInven = "inven_data.json";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // DataManager ������Ʈ�� ���� ������ �����մϴ�.
    }

    public void SaveStageData()
    {

    }
    public void SaveInventoryData() //�κ� -> json
    {
        List<Item> invenData = new List<Item>();
        invenData = InventoryManager.Instance.items;
        string jsonData = JsonUtility.ToJson(invenData);
        File.WriteAllText(saveFileInven, jsonData);
        SaveToCloud(jsonData);
    }

    public void LoadInventoryData() //json -> �κ�
    {
        string filePath = GetSaveInvenPath();
        List<Item> invenData = new List<Item>();
        if(File.Exists(saveFileInven))
        {
            string jsonData = File.ReadAllText(filePath);
            invenData = JsonUtility.FromJson<List<Item>>(jsonData);
            LoadFromCloud(saveFileInven);
        }
    }

    public void SaveGoldData() // gold -> json
    {
        int gold = Gold.Instance.GetGold(); // Gold ��ũ��Ʈ�� gold ������ �����մϴ�.
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
        return Path.Combine(Application.persistentDataPath, saveFileGold);
    }
    private string GetSaveInvenPath()
    {
        return Path.Combine(Application.persistentDataPath + saveFileInven);
    }

    private void SaveToCloud(string jsonData)
    {
        GPGSBinder.Inst.SaveCloud(saveFileGold, jsonData);
        GPGSBinder.Inst.SaveCloud(saveFileInven, jsonData);
    }
    private void LoadFromCloud(string saveFileName)
    {
        GPGSBinder.Inst.LoadCloud(saveFileName);
        GPGSBinder.Inst.LoadCloud(saveFileInven);
    }
}

[System.Serializable]
public class GoldData
{
    public int gold;
}
