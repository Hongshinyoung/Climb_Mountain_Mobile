using Firebase.Auth;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public ClimbTime climbTime; // 게임 로직과 관련된 다른 스크립트인 것으로 가정
    public Text[] rankingIndex;
    public Text[] rankingName;
    public Text[] rankingScore;

    private List<User> rankingList = new List<User>();
    private FirebaseManager firebaseManager;
    private string userId;
    private string userName;

    private void Start()
    {
        firebaseManager = FindObjectOfType<FirebaseManager>();
        userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        userName = FirebaseAuth.DefaultInstance.CurrentUser.DisplayName;
        LoadRankingData();
    }

    public void SaveRankingData(int score)
    {
        firebaseManager.SaveRankingData(userId, userName, score);
    }

    public void LoadRankingData()
    {
        firebaseManager.LoadRankingData(UpdateRankingUI);
    }

    private void UpdateRankingUI(List<User> rankingData)
    {
        if (rankingData == null)
        {
            Debug.LogError("랭킹 데이터를 불러오는 중 오류 발생");
            return;
        }

        rankingList = rankingData;
        rankingList.Sort((a, b) => b.score.CompareTo(a.score)); // 점수 내림차순으로 정렬

        for (int i = 0; i < rankingList.Count && i < rankingSystemData.txtRank.Length; i++)
        {
            rankingSystemData.txtRank[i].text = (i + 1).ToString();
            rankingSystemData.txtNickName[i].text = rankingList[i].username;
            rankingSystemData.txtScore[i].text = rankingList[i].score.ToString();
        }
    }

    public void ScoreUpdate(int score)
    {
        SaveRankingData(score);
        LoadRankingData();
    }
}
