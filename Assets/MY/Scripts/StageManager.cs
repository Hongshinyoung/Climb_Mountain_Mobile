using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Button[] stageButtons; // �� ���� ��ư
    public PlayerMove player;
    public GameObject[] stage; // �� ����
    public GameObject[] stageStartPos; // �� ���� ���� ������
    public MountainInfo mountainInfo;
    public ClimbTime climbTime;
    public int currentStage = 0; // ���� ���õ� ���������� �ε���

    public Image[] starImages; // �� �������� ��ư�� �� �̹����� ǥ���� Image ������Ʈ �迭
    public Sprite[] starSprite; // �� �̹���

    void Start()
    {
        // �������� ��ư�� Ŭ�� �̺�Ʈ �߰�
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageIndex = i;
            stageButtons[i].onClick.AddListener(() => SceneTrans(stageIndex));
        }
    }

    // �������� �ε� �Լ�
    public void SceneTrans(int stageIndex)
    {
        currentStage = stageIndex; // ���� ���õ� ���������� �ε��� ����
        mountainInfo.currentPosition = 0;
        // �̵� �ӵ� �ʱ�ȭ
        player.ResetToWalk();


        // �÷��̾��� ��ġ�� �ش� ���������� ���� ��ġ�� �̵�
        player.transform.position = stageStartPos[stageIndex].transform.position;

        // ��� ���������� ��Ȱ��ȭ
        for (int i = 0; i < stage.Length; i++)
        {
            stage[i].SetActive(false);
        }

        // ���� �������� Ȱ��ȭ
        stage[stageIndex].SetActive(true);

        // ���� ���� ���� ǥ��pp
        mountainInfo.DisplayMountainInfo();
        mountainInfo.EditToSelectMountainAltitude();
        // Ŭ���� ���� Ȯ��
        //mountainInfo.CheckStageClear();
    }

    // �� ǥ�� �Լ�
    public void ShowStars(int starCount)
    {
        if (mountainInfo.IsCleared)
        {
            mountainInfo.IsCleared = false;
            Debug.Log("���� ��������:" + currentStage +"������: " + starCount + "Ŭ�������: "+mountainInfo.IsCleared );
            starImages[currentStage].sprite = starSprite[starCount -1]; // ������1,2,3�� �迭 0,1,2�� �����Ƿ� �� �������� -1 ���ش�.
            starImages[currentStage].gameObject.SetActive(true);
        }
        // Ŭ������ �������������� ���� ǥ��
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

    // ��� ���� �� �� ǥ�� �Լ�
    public void DetermineGrade()
    {
        // ���� ���� �ð� ��������
        float mountainTime = climbTime.elapsedTime;

        // �� ������ ��ȯ
        int minutes = (int)(mountainTime / 60f);

        // �� ��� ����
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

        // �� ǥ��
        ShowStars(starCount);
    }


    // ���� ���õ� ���������� Ŭ����Ǿ����� Ȯ���ϴ� �Լ�
    public bool IsStageCleared(int stageIndex)
    {
        // Ŭ���� ���θ� Ȯ���Ͽ� ��ȯ�ϴ� ���� ����
        return mountainInfo.IsCleared;
    }
}
