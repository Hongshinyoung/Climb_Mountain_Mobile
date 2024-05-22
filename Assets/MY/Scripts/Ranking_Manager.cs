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

    private List<RankingSystemData> rankingSystemDataList = new List<RankingSystemData>();

    void SaveRankingData()
    {

    }

    public void ShowData()
    {
        rankingSystemData.txtRank = rankingIndex;
        rankingSystemData.txtNickName = rankingName;
        rankingSystemData.txtScore = rankingScore;
    }

    public void LoadRankingData()
    {
        //여기서 로그인 아이디 출력

        GPGSBinder.Inst.Login((success, userId) =>
        {
            if (success)
            {
                rankingSystemData.txtNickName[0].text = userId.ToString(); //랭킹 닉네임 = 구글유저아이디
            }
            else
            {
                Debug.Log("로그인실패");
            }
        });
    }

    void AddNickName(RankingSystemData name)
    {
        rankingSystemDataList.Add(name);
    }
   
    public void ScoreUpdate(string score) //climbTime에서 시간을 점수로 변환가져옴
    {
        climbTime.UpdateRankingData(score);
        SortScoreList();
    }

    void SortScoreList()
    {
        rankingSystemDataList.Sort((a,b) => int.Parse(b.txtScore[0].text).CompareTo(int.Parse(a.txtScore[0].text))); //랭킹 스코어 내림차순으로 정렬
    }


}
