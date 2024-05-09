using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ClimbingTimeData
{
    public float[] ClearTime = new float[7]; //�������� �� Ŭ���� Ÿ��
}

public class ClimbTime : MonoBehaviour
{
    public Text timerText;
    public float elapsedTime = 0f;
    private bool isRunning = false;
    public StageManager stageManager;
    public MountainInfo mountainInfo;
    private ClimbingTimeData climbingTime;
    private const string filePath = "ClimbData";
    private void Awake()
    {
        StartStopwatch();
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    void ClearTimeUpdate()
    {
        if(mountainInfo.IsCleared)
        {
            climbingTime.ClearTime[stageManager.currentStage - 1] = elapsedTime/60;
            SaveClimbTime();
        }
    }

    public void SaveClimbTime() //��ݽð� ������ -> json
    {
        string dataPath = Application.persistentDataPath + filePath;
        ClimbingTimeData data = new ClimbingTimeData();
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, jsonData);
    }

    public void LoadClimbTime() //json -> ��ݽð� ������
    {
        string dataPath = Path.Combine(Application.persistentDataPath, filePath);
        if (File.Exists(Application.persistentDataPath+dataPath))
        {

            string jsonData = File.ReadAllText(dataPath);
            climbingTime = JsonUtility.FromJson<ClimbingTimeData>(jsonData);
        }
    }

    void UpdateTimerUI()
    {
        int minutes = (int)(elapsedTime / 60f);
        int seconds = (int)(elapsedTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // ��� ���� �� �� ǥ�� ��û
        stageManager.DetermineGrade();
    }

    public void StartStopwatch()
    {
        isRunning = true;
    }

    public void StopStopwatch()
    {
        isRunning = false;
    }

    public void ResetStopwatch()
    {
        elapsedTime = 0f;
        UpdateTimerUI();
    }
}
