using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Button[] stageButtons;
    public Image starImage;
    public Sprite[] starSprites; // 0: ��, 1: ��, 2: ��

    private int currentStage;

    void Start()
    {
        // �������� ��ư�� Ŭ�� �̺�Ʈ �߰�
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageIndex = i + 1; // �������� ��ȣ�� 1���� ����
            stageButtons[i].onClick.AddListener(() => LoadStage(stageIndex));
        }
    }

    // �������� �ε� �Լ�
    void LoadStage(int stageIndex)
    {
        currentStage = stageIndex;
        Debug.Log("Loading Stage " + stageIndex);
        // ���⿡ ���������� �ε��ϴ� �ڵ带 �߰�
        // �������� Ŭ���� �� ��� ���� �Լ� ȣ��
        DetermineGrade();
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
