using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Button[] stageButtons; // 맵 선택 버튼
    public PlayerMove player;
    public GameObject[] stage; // 맵 종류
    public GameObject[] stageStartPos; // 각 맵의 시작 포지션
    public MountainInfo mountainInfo;
    public ClimbTime climbTime;
    public int currentStage = 0; // 현재 선택된 스테이지의 인덱스

    public Image[] starImages; // 각 스테이지 버튼에 별 이미지를 표시할 Image 컴포넌트 배열
    public Sprite[] starSprite; // 별 이미지

    void Start()
    {
        // 스테이지 버튼에 클릭 이벤트 추가
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageIndex = i;
            stageButtons[i].onClick.AddListener(() => SceneTrans(stageIndex));
        }
    }

    // 스테이지 로드 함수
    public void SceneTrans(int stageIndex)
    {
        currentStage = stageIndex; // 현재 선택된 스테이지의 인덱스 설정
        mountainInfo.currentPosition = 0;
        // 이동 속도 초기화
        player.ResetToWalk();


        // 플레이어의 위치를 해당 스테이지의 시작 위치로 이동
        player.transform.position = stageStartPos[stageIndex].transform.position;

        // 모든 스테이지를 비활성화
        for (int i = 0; i < stage.Length; i++)
        {
            stage[i].SetActive(false);
        }

        // 현재 스테이지 활성화
        stage[stageIndex].SetActive(true);

        // 현재 산의 정보 표시pp
        mountainInfo.DisplayMountainInfo();
        mountainInfo.EditToSelectMountainAltitude();
        // 클리어 조건 확인
        //mountainInfo.CheckStageClear();
    }

    // 별 표시 함수
    public void ShowStars(int starCount)
    {
        if (mountainInfo.IsCleared)
        {
            mountainInfo.IsCleared = false;
            Debug.Log("현재 스테이지:" + currentStage +"별갯수: " + starCount + "클리어상태: "+mountainInfo.IsCleared );
            starImages[currentStage].sprite = starSprite[starCount -1]; // 별갯수1,2,3이 배열 0,1,2로 있으므로 별 개수에서 -1 해준다.
            starImages[currentStage].gameObject.SetActive(true);
        }
        // 클리어한 스테이지까지만 별을 표시
        //for (int i = 0; i < starImages.Length; i++)
        //{
        //    if (i < starCount)
        //    {
        //        starImages[i].sprite = starSprite[i];
        //        starImages[i].gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        starImages[i].gameObject.SetActive(false);
        //    }
        //}
    }

    // 등급 결정 및 별 표시 함수
    public void DetermineGrade()
    {
        // 산의 현재 시간 가져오기
        float mountainTime = climbTime.elapsedTime;

        // 분 단위로 변환
        int minutes = (int)(mountainTime / 60f);

        // 별 등급 결정
        int starCount = 0;
        if (minutes >= 60)
        {
            starCount = 1;
        }
        else if (minutes == 60)
        {
            starCount = 2;
        }
        else
        {
            starCount = 3;
        }

        // 별 표시
        ShowStars(starCount);
    }


    // 현재 선택된 스테이지가 클리어되었는지 확인하는 함수
    public bool IsStageCleared(int stageIndex)
    {
        // 클리어 여부를 확인하여 반환하는 로직 구현
        return mountainInfo.IsCleared;
    }
}
