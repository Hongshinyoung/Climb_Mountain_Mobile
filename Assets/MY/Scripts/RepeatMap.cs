using UnityEngine;

public class RepeatMap : MonoBehaviour
{
    public Transform player;
    public Transform[] lanes;
    public float moveSpeed = 1.5f;

    private bool isMoving = true;
    private Vector2 touchStartPos; // 레인 이동을 위해 스크롤 위치계산
    private Vector2 touchEndPos;
    private int currentLaneIndex = 1; // 플레이어가 현재 있는 레인 인덱스
    private bool isSwipe = false;
    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        MovePlayer();
    }

    void MovePlayer()
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
        targetPosition.y = player.position.y;
        player.position = Vector3.MoveTowards(player.position, targetPosition, moveSpeed * Time.deltaTime);

        if(player.position == targetPosition)
        {
            isSwipe = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = false;
            Debug.Log("플레이어와 부딪힘");
        }
    }
}
