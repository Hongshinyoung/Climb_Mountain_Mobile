using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[System.Serializable]
public class ClimbingTimeData
{
    public float[] ClearTime = new float[7]; //스테이지 별 클리어 타임
}

public class ClimbTime : MonoBehaviour
{
    public Text timerText;
    public float elapsedTime = 0f;
    private bool isRunning = false;
    public StageManager stageManager;
    public MountainInfo mountainInfo;
    private ClimbingTimeData climbingTime;
    private RankingSystemData rankingSystemData;
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

            if (climbingTime.ClearTime[stageManager.currentStage - 1] <= 30)
            {
                UpdateRankingData("1000"); // 등급에 따른 랭킹 데이터 업데이트
            }
            else if (climbingTime.ClearTime[stageManager.currentStage - 1] >= 31 && climbingTime.ClearTime[stageManager.currentStage - 1] <= 40)
            {
                UpdateRankingData("800"); // 등급에 따른 랭킹 데이터 업데이트
            }
            else if (climbingTime.ClearTime[stageManager.currentStage - 1] >= 41 && climbingTime.ClearTime[stageManager.currentStage - 1] <= 50)
            {
                UpdateRankingData("600"); // 등급에 따른 랭킹 데이터 업데이트
            }
            else if (climbingTime.ClearTime[stageManager.currentStage - 1] >= 51)
            {
                UpdateRankingData("300"); // 등급에 따른 랭킹 데이터 업데이트
            }
            SaveClimbTime();
        }
    }

    public void UpdateRankingData(string Score)
    {

        // 랭킹 시스템 데이터의 점수 업데이트
        for (int i = 0; i < rankingSystemData.txtScore.Length; i++)
        {
            rankingSystemData.txtScore[i].text = Score;
        }
    }

    public void SaveClimbTime() //등반시간 데이터 -> json
    {
        string dataPath = Application.persistentDataPath + filePath;
        ClimbingTimeData data = new ClimbingTimeData();
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, jsonData);
    }

    public void LoadClimbTime() //json -> 등반시간 데이터
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

        // 등급 결정 및 별 표시 요청
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
