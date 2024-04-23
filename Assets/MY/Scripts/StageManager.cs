using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Button[] stageButtons;
    public Image starImage;
    public Sprite[] starSprites; // 0: ��, 1: ��, 2: ��
    public PlayerMove player;
    public GameObject[] stage;
    public GameObject[] stageStartPos;
    public MountainInfo mountainInfo;
    public ClimbTime climbTime;
    public  int currentStage = 0;

    void Start()
    {
        // �������� ��ư�� Ŭ�� �̺�Ʈ �߰�
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageIndex = i;
            stageButtons[i].onClick.AddListener(() => SceneTrans(stageIndex));
            stageButtons[i].onClick.AddListener(() => climbTime.ResetStopwatch());
            stageButtons[i].onClick.AddListener(() => climbTime.StartStopwatch());

        }
    }

    // �������� �ε� �Լ�
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


        //stageStartPos[i] ��ġ�� �̵�

        //DetermineGrade();
    }

    void SelectStage()
    {
       // mountainInfo.textMountainName.text = stageButtons[currentStage].GetComponentInChildren<Text>().text;
        mountainInfo.EditToSelectMountainAltitude();
    }


    // ��� ���� �Լ�
    void DetermineGrade()
    {
        // �� �κп��� ������ Ŭ���� ������ ���ϰ� ����� �����մϴ�.
        // ���� ���, Ŭ���� ������ �������� �� ����� �ο��ϰ�,
        // �׿� ���� ������ ��� �̹����� ǥ���մϴ�.
        int grade = CalculateGrade(); // ��� ��� �Լ� ȣ��
        ShowGrade(grade); // ��� ǥ�� �Լ� ȣ��
    }

    // ��� ��� �Լ� (�ӽ÷� ���� ��� ��ȯ)
    int CalculateGrade()
    {
        return Random.Range(0, 3); // 0, 1, 2 �߿��� �������� ��� ��ȯ
    }

    // ��� ǥ�� �Լ�
    void ShowGrade(int grade)
    {
        starImage.sprite = starSprites[grade];
    }
}
