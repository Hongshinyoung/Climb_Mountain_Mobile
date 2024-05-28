using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;
using Firebase.Analytics;

[System.Serializable]
public class RankingSystemData
{
    public Text[] txtRank;
    public Text[] txtNickName;
    public Text[] txtScore;
}

public class Ranking_Manager : MonoBehaviour
{
    public RankingSystemData rankingSystemData;
    public ClimbTime climbTime;
    public Text[] rankingIndex;
    public Text[] rankingName;
    public Text[] rankingScore;

    private List<User> rankingList = new List<User>();

    public FirebaseManager firebaseManager;

    private void Start()
    {
        firebaseManager = FindObjectOfType<FirebaseManager>();
        LoadRankingData();
    }

    public void SaveRankingData(string userId, string userName, int userScore)
    {
        firebaseManager.SaveRankingData(userId, userName, userScore);
    }

    public void LoadRankingData()
    {
        firebaseManager.LoadRankingData(UpdateRankingUI);
    }

    private void UpdateRankingUI(List<User> rankingData)
    {
        rankingList = rankingData;
        rankingList.Sort((a, b) => b.score.CompareTo(a.score)); // 점수 내림차순으로 정렬

        for (int i = 0; i < rankingList.Count && i < rankingSystemData.txtRank.Length; i++)
        {
            rankingSystemData.txtRank[i].text = (i + 1).ToString();
            rankingSystemData.txtNickName[i].text = rankingList[i].username;
            rankingSystemData.txtScore[i].text = rankingList[i].score.ToString();
        }
    }

    public void ScoreUpdate(string userId, string userName, int userScore)
    {
        SaveRankingData(userId, userName, userScore);
        LoadRankingData();
    }
}
