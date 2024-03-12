using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class MountainInfo : MonoBehaviour
{
    public Text textMountainName; // �� �̸� ��� �ؽ�Ʈ
    public Text textMountainAltitude; // �� ���� ��� �ؽ�Ʈ
    public Text textCurrentMoveDistance; // ���� �̵��Ÿ� �ؽ�Ʈ
    private float currentPosition = 0;
    public RepeatMap map;

    private List<MountainData> mountainDataList = new List<MountainData>();

    private int currentMountainIndex = 0; // ���� ���� �ε���

    [System.Serializable]
    public class MountainData
    {
        public string name;
        public float altitude;
    }

    void Start()
    {
        LoadMountainDataFromJson();
        DisplayMountainInfo();
    }

    private void Update()
    {
        currentPosition += map.moveSpeed * Time.deltaTime;
        textCurrentMoveDistance.text = currentPosition.ToString("F2") + "m"; // �Ҽ��� 2�ڸ�����

        // ���� �̵��Ÿ��� ���� ���� ���̿� ���ų� ������ ���� ���� ���� ���
        if (currentPosition >= mountainDataList[currentMountainIndex].altitude)
        {
            currentMountainIndex++;
            currentPosition = 0;
            DisplayMountainInfo();
        }
    }

    void LoadMountainDataFromJson()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Mountain_data.json");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            MountainDataWrapper dataWrapper = JsonUtility.FromJson<MountainDataWrapper>(jsonData);
            mountainDataList = dataWrapper.mountains;
        }
        else
        {
            Debug.LogError("�� ���� ������ ã�� �� ����");
        }
    }

    void DisplayMountainInfo()
    {
        // ���� ���� �̸��� ���� ���
        textMountainName.text = mountainDataList[currentMountainIndex].name;
        textMountainAltitude.text = mountainDataList[currentMountainIndex].altitude.ToString() + "m";
    }

    [System.Serializable]
    public class MountainDataWrapper
    {
        public List<MountainData> mountains;
    }
}
