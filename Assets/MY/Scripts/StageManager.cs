using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Button[] stageButtons;
    public Image starImage;
    public Sprite[] starSprites; // 0: 하, 1: 중, 2: 상

    private int currentStage;

    void Start()
    {
        // 스테이지 버튼에 클릭 이벤트 추가
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageIndex = i + 1; // 스테이지 번호는 1부터 시작
            stageButtons[i].onClick.AddListener(() => LoadStage(stageIndex));
        }
    }

    // 스테이지 로드 함수
    void LoadStage(int stageIndex)
    {
        currentStage = stageIndex;
        Debug.Log("Loading Stage " + stageIndex);
        // 여기에 스테이지를 로드하는 코드를 추가
        // 스테이지 클리어 후 등급 결정 함수 호출
        DetermineGrade();
    }

    // 등급 결정 함수
    void DetermineGrade()
    {
        // 이 부분에서 실제로 클리어 조건을 평가하고 등급을 결정합니다.
        // 예를 들어, 클리어 조건을 만족했을 때 등급을 부여하고,
        // 그에 따라 적절한 등급 이미지를 표시합니다.
        int grade = CalculateGrade(); // 등급 계산 함수 호출
        ShowGrade(grade); // 등급 표시 함수 호출
    }

    // 등급 계산 함수 (임시로 랜덤 등급 반환)
    int CalculateGrade()
    {
        return Random.Range(0, 3); // 0, 1, 2 중에서 랜덤으로 등급 반환
    }

    // 등급 표시 함수
    void ShowGrade(int grade)
    {
        starImage.sprite = starSprites[grade];
    }
}
