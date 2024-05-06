using UnityEngine;

public class RepeatMap : MonoBehaviour
{
    public Transform player;
    public Transform[] lanes;
    public float moveSpeed = 1.5f;
    public Transform[] startPoint;
    public Transform[] endPoint;
    public StageManager stageManager;
    public Transform enemyStartPos;
    private bool isMoving = true;
    private Vector2 touchStartPos; // 레인 이동을 위한 터치 시작 위치
    private Vector2 touchEndPos;
    private int currentLaneIndex = 1; // 플레이어가 현재 있는 레인 인덱스
    private bool isSwipe = false;

    private void Start()
    {
        player.transform.position = startPoint[stageManager.currentStage].transform.position;
        //모든 맵 비활성화
        for (int i = 0; i < stageManager.stage.Length; i++)
        {
            stageManager.stage[i].SetActive(false);
        }

        // 현재 스테이지 활성화
        stageManager.stage[stageManager.currentStage].SetActive(true);
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (isMoving)
        {
            if (player.position.x <= endPoint[stageManager.currentStage].position.x)
            {
                ResetMap();
            }

        }
        DoSwipe();
    }

    void ResetMap()
    {
        player.transform.position = startPoint[stageManager.currentStage].transform.position;
        isMoving = true;
    }

    void DoSwipe()
    {
        // 모바일 환경에서 스크롤 또는 드래그 입력 처리
        if (Input.touchCount > 0 && !isSwipe && isMoving)
        {
            Touch touch = Input.GetTouch(0); // 첫 번째 터치만 처리
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                float swipeDistance = touchEndPos.x - touchStartPos.x;

                // 스와이프 거리에 따라 플레이어를 이동시킴
                if (Mathf.Abs(swipeDistance) > Screen.width * 0.1f) // 스와이프 거리가 일정값 이상일 때만 처리
                {
                    int direction = swipeDistance > 0 ? 1 : -1; // 오른쪽으로 스와이프하면 1, 왼쪽으로 스와이프하면 -1
                    currentLaneIndex = Mathf.Clamp(currentLaneIndex + direction, 0, lanes.Length - 1);
                    isSwipe = true;
                }
            }
        }

        // 현재 레인으로 플레이어를 이동시킴
        Vector3 targetPosition = lanes[currentLaneIndex].position;
        targetPosition.y = player.position.y; //위 아래로 고정
        targetPosition.x = player.position.x; //앞 뒤로 고정
        if (isSwipe)
        {
            player.position = Vector3.MoveTowards(player.position, targetPosition, 5 * Time.deltaTime);
        }
        else targetPosition.z = player.position.z; //스와이프 할 때만 z축 이동하며 아닐땐 고정
       

        if (player.position == targetPosition)
        {
            isSwipe = false;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        isMoving = true; // 부딪히면 다시 맵을 이동하도록 설정
    //        Debug.Log("플레이어와 부딪힘");
    //    }
    //}
}
