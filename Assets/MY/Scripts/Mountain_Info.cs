using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MountainInfo : MonoBehaviour
{
    public bool IsCleared = false;
    public Text textMountainName; // �� �̸� ��� �ؽ�Ʈ
    public Text textMountainAltitude; // �� ���� ��� �ؽ�Ʈ
    public Text textCurrentMoveDistance; // ���� �̵��Ÿ� �ؽ�Ʈ
    public float currentPosition = 300;
    public RepeatMap map;
    public StageManager stageManager;

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
        currentPosition += map.moveSpeed * Time.deltaTime * 0.5f;
        textCurrentMoveDistance.text = currentPosition.ToString("F2") + "m"; // �Ҽ��� 2�ڸ�����

        // ���� �̵��Ÿ��� ���� ���� ���̿� ���ų� ������ ���� ���� ���� ���
        if (currentPosition >= mountainDataList[currentMountainIndex].altitude)
        {
            currentMountainIndex++;
            currentPosition = 0;
            IsCleared = true;
            DisplayMountainInfo();
            stageManager.DetermineGrade(); // �� Ŭ���� �� ��� �ο�
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

    public void DisplayMountainInfo()
    {
        // ���� ���� �̸��� ���� ���
        textMountainName.text = mountainDataList[currentMountainIndex].name;
        textMountainAltitude.text = mountainDataList[currentMountainIndex].altitude.ToString() + "m";
    }
    public void EditToSelectMountainAltitude()
    {
        textMountainName.text = mountainDataList[stageManager.currentStage].name;
        textMountainAltitude.text = mountainDataList[stageManager.currentStage].altitude.ToString() + "m";
    }

    // �� Ŭ���� ���� Ȯ��
    public void CheckStageClear()
    {
        // ���� Ŭ���� �Ǿ��ٸ�
        //if (IsCleared)
        //{
        //    // �ش� ���������� �� ǥ��
        //    stageManager.ShowStars(currentMountainIndex + 1);
        //}
    }

    [System.Serializable]
    public class MountainDataWrapper
    {
        public List<MountainData> mountains;
    }
}
