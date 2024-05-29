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
    public ClimbTime climbTime; // ���� ������ ���õ� �ٸ� ��ũ��Ʈ�� ������ ����
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
            Debug.LogError("��ŷ �����͸� �ҷ����� �� ���� �߻�");
            return;
        }

        rankingList = rankingData;
        rankingList.Sort((a, b) => b.score.CompareTo(a.score)); // ���� ������������ ����

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
