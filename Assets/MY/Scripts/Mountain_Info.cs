using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Collections;

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

        if (Application.platform == RuntimePlatform.Android)
        {
            // �ȵ���̵忡���� WWW Ŭ������ ����Ͽ� ���Ͽ� ����
            StartCoroutine(LoadJsonFile(filePath));
        }
        else
        {
            // �����ͳ� �ٸ� �÷��������� �Ϲ����� ���� �б�� ó��
            string jsonData = File.ReadAllText(filePath);
            MountainDataWrapper dataWrapper = JsonUtility.FromJson<MountainDataWrapper>(jsonData);
            mountainDataList = dataWrapper.mountains;
        }
    }

    IEnumerator LoadJsonFile(string filePath)
    {
        // �ȵ���̵忡�� ���� ���� �� WWW Ŭ���� ���
        WWW www = new WWW(filePath);

        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            MountainDataWrapper dataWrapper = JsonUtility.FromJson<MountainDataWrapper>(www.text);
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
