using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class MountainInfo : MonoBehaviour
{
    public Text textMountainName; // 산 이름 출력 텍스트
    public Text textMountainAltitude; // 산 높이 출력 텍스트
    public Text textCurrentMoveDistance; // 현재 이동거리 텍스트
    private float currentPosition = 0;
    public RepeatMap map;

    private List<MountainData> mountainDataList = new List<MountainData>();

    private int currentMountainIndex = 0; // 현재 산의 인덱스

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
        textCurrentMoveDistance.text = currentPosition.ToString("F2") + "m"; // 소숫점 2자리까지

        // 현재 이동거리가 다음 산의 높이와 같거나 높으면 다음 산의 정보 출력
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
            Debug.LogError("산 정보 파일을 찾을 수 없음");
        }
    }

    void DisplayMountainInfo()
    {
        // 현재 산의 이름과 높이 출력
        textMountainName.text = mountainDataList[currentMountainIndex].name;
        textMountainAltitude.text = mountainDataList[currentMountainIndex].altitude.ToString() + "m";
    }

    [System.Serializable]
    public class MountainDataWrapper
    {
        public List<MountainData> mountains;
    }
}
