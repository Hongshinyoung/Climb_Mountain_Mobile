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
    RankingSystemData rankingSystemData;
    ClimbTime climbTime;
    //public Text[] rankingIndex;
    //public Text[] rankingName;
    //public Text[] rankingScore;

    private List<RankingSystemData> rankingSystemDataList = new List<RankingSystemData>();

    void SaveRankingData()
    {

    }

    void LoadRankingData()
    {
        //���⼭ �α��� ���̵� ���

        GPGSBinder.Inst.Login((success, userId) =>
        {
            if (success)
            {
                rankingSystemData.txtNickName[0].text = userId.ToString();
            }
            else
            {
                Debug.Log("�α��ν���");
            }
            SortScoreList();
        });
    }

    void AddNickName(RankingSystemData name)
    {
        rankingSystemDataList.Add(name);
    }
   
    void ScoreUpdate(string score)
    {
        climbTime.UpdateRankingData(score);
        SortScoreList();
    }

    void SortScoreList()
    {
        rankingSystemDataList.Sort((a,b) => int.Parse(b.txtScore[0].text).CompareTo(int.Parse(a.txtScore[0].text))); //������������ ����
    }


}
