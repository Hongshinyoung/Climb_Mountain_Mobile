
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Button[] stageButtons;
    public Image[] starImage;
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

    void SelectStage() //�� ���� ��������
    {
       // mountainInfo.textMountainName.text = stageButtons[currentStage].GetComponentInChildren<Text>().text;
        mountainInfo.EditToSelectMountainAltitude(); //���̸��� ��Ī ���� ������ �ҷ�����
    }


    // ��� ���� �Լ�
    void DetermineGrade()
    {
        // �� �κп��� ������ Ŭ���� ������ ���ϰ� ����� �����մϴ�.
        // ���� ���, Ŭ���� ������ �������� �� ����� �ο��ϰ�,
        // �׿� ���� ������ ��� �̹����� ǥ���մϴ�.

        int elapsedTimeMinutes = (int)(climbTime.elapsedTime / 60f); // ��� �ð�(��) ���
        //switch (elapsedTimeMinutes)
        //{
        //    case int n when n >= 60:
        //        ShowGrade(2); // 1�ð� �̻� �ҿ� �� "��" ��� �ο�
        //        break;
        //    case 60:
        //        ShowGrade(1); // 1�ð� �ҿ� �� "��" ��� �ο�
        //        break;
        //    default:
        //        ShowGrade(0); // 1�ð� �̸� �ҿ� �� "��" ��� �ο�
        //        break;
        //}
         //  ��� �ð��� ���� ��� �ο�
        if (elapsedTimeMinutes >= 60)
        {
            ShowGrade(2); // 1�ð� �̻� �ҿ� �� "��" ��� �ο�
        }
        else if (elapsedTimeMinutes == 60)
        {
            ShowGrade(1); // 1�ð� �ҿ� �� "��" ��� �ο�
        }
        else
        {
            ShowGrade(0); // 1�ð� �̸� �ҿ� �� "��" ��� �ο�
        }
    }



    // ��� ǥ�� �Լ�
    void ShowGrade(int grade)
    {
        starImage[grade].sprite = starSprites[grade];
        Debug.Log("��� ǥ��");
    }
}
