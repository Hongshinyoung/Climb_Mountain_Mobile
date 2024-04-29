
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Button[] stageButtons;
    public Image[] starImage;
    public Sprite[] starSprites; // 0: 하, 1: 중, 2: 상
    public PlayerMove player;
    public GameObject[] stage;
    public GameObject[] stageStartPos;
    public MountainInfo mountainInfo;
    public ClimbTime climbTime;
    public  int currentStage = 0;

    void Start()
    {
        // 스테이지 버튼에 클릭 이벤트 추가
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageIndex = i;
            stageButtons[i].onClick.AddListener(() => SceneTrans(stageIndex));
            stageButtons[i].onClick.AddListener(() => climbTime.ResetStopwatch());
            stageButtons[i].onClick.AddListener(() => climbTime.StartStopwatch());

        }
    }

    // 스테이지 로드 함수
    public void SceneTrans(int stageIndex)
    {
        currentStage = stageIndex;
        player.transform.position -= new Vector3(transform.position.x, 1000, transform.position.z);

        for (int i = 0; i < stage.Length; i++)
        {
            stage[i].SetActive(false);
        }

        mountainInfo.currentPosition = 0;
        stage[stageIndex].SetActive(true);
        SelectStage();
      //  Debug.Log(mountainInfo.textMountainName.text + ", " + stageButtons[currentStage].GetComponentInChildren<Text>().text);


        //stageStartPos[i] 위치로 이동

        //DetermineGrade();
    }

    void SelectStage() //산 정보 가져오기
    {
       // mountainInfo.textMountainName.text = stageButtons[currentStage].GetComponentInChildren<Text>().text;
        mountainInfo.EditToSelectMountainAltitude(); //산이름과 매칭 시켜 데이터 불러오기
    }


    // 등급 결정 함수
    void DetermineGrade()
    {
        // 이 부분에서 실제로 클리어 조건을 평가하고 등급을 결정합니다.
        // 예를 들어, 클리어 조건을 만족했을 때 등급을 부여하고,
        // 그에 따라 적절한 등급 이미지를 표시합니다.

        int elapsedTimeMinutes = (int)(climbTime.elapsedTime / 60f); // 경과 시간(분) 계산
        //switch (elapsedTimeMinutes)
        //{
        //    case int n when n >= 60:
        //        ShowGrade(2); // 1시간 이상 소요 시 "상" 등급 부여
        //        break;
        //    case 60:
        //        ShowGrade(1); // 1시간 소요 시 "중" 등급 부여
        //        break;
        //    default:
        //        ShowGrade(0); // 1시간 미만 소요 시 "하" 등급 부여
        //        break;
        //}
         //  경과 시간에 따라 등급 부여
        if (elapsedTimeMinutes >= 60)
        {
            ShowGrade(2); // 1시간 이상 소요 시 "상" 등급 부여
        }
        else if (elapsedTimeMinutes == 60)
        {
            ShowGrade(1); // 1시간 소요 시 "중" 등급 부여
        }
        else
        {
            ShowGrade(0); // 1시간 미만 소요 시 "하" 등급 부여
        }
    }



    // 등급 표시 함수
    void ShowGrade(int grade)
    {
        starImage[grade].sprite = starSprites[grade];
        Debug.Log("등급 표시");
    }
}
