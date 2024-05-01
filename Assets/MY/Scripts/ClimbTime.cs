using UnityEngine;
using UnityEngine.UI;

public class ClimbTime : MonoBehaviour
{
    public Text timerText;
    public float elapsedTime = 0f;
    private bool isRunning = false;
    public StageManager stageManager;

    private void Awake()
    {
        StartStopwatch();
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = (int)(elapsedTime / 60f);
        int seconds = (int)(elapsedTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // 등급 결정 및 별 표시 요청
        stageManager.DetermineGrade();
    }

    public void StartStopwatch()
    {
        isRunning = true;
    }

    public void StopStopwatch()
    {
        isRunning = false;
    }

    public void ResetStopwatch()
    {
        elapsedTime = 0f;
        UpdateTimerUI();
    }
}
