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
        //���⼭ �α��� ���̵� ���

        GPGSBinder.Inst.Login((success, userId) =>
        {
            if (success)
            {
                rankingSystemData.txtNickName[0].text = userId.ToString(); //��ŷ �г��� = �����������̵�
            }
            else
            {
                Debug.Log("�α��ν���");
            }
        });
    }

    void AddNickName(RankingSystemData name)
    {
        rankingSystemDataList.Add(name);
    }
   
    public void ScoreUpdate(string score) //climbTime���� �ð��� ������ ��ȯ������
    {
        climbTime.UpdateRankingData(score);
        SortScoreList();
    }

    void SortScoreList()
    {
        rankingSystemDataList.Sort((a,b) => int.Parse(b.txtScore[0].text).CompareTo(int.Parse(a.txtScore[0].text))); //��ŷ ���ھ� ������������ ����
    }


}
