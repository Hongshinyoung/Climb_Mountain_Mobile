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
        currentPosition += map.moveSpeed * Time.deltaTime *0.5f;
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
        // ���ҽ� �������� ���� �ε�
        TextAsset jsonFile = Resources.Load<TextAsset>("Mountain_data");

        if (jsonFile != null)
        {
            string jsonData = jsonFile.text;
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