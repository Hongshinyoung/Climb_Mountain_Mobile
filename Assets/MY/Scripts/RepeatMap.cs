using UnityEngine;

public class RepeatMap : MonoBehaviour
{
    public Transform player;
    public Transform[] lanes;
    public float moveSpeed = 1.5f;
    public Transform startPoint;
    public Transform endPoint;
    private bool isMoving = true;
    private Vector2 touchStartPos; // ���� �̵��� ���� ��ġ ���� ��ġ
    private Vector2 touchEndPos;
    private int currentLaneIndex = 1; // �÷��̾ ���� �ִ� ���� �ε���
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
        // ����� ȯ�濡�� ��ũ�� �Ǵ� �巡�� �Է� ó��
        if (Input.touchCount > 0 && !isSwipe && isMoving)
        {
            Touch touch = Input.GetTouch(0); // ù ��° ��ġ�� ó��
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                float swipeDistance = touchEndPos.x - touchStartPos.x;

                // �������� �Ÿ��� ���� �÷��̾ �̵���Ŵ
                if (Mathf.Abs(swipeDistance) > Screen.width * 0.05f) // �������� �Ÿ��� ������ �̻��� ���� ó��
                {
                    int direction = swipeDistance > 0 ? 1 : -1; // ���������� ���������ϸ� 1, �������� ���������ϸ� -1
                    currentLaneIndex = Mathf.Clamp(currentLaneIndex + direction, 0, lanes.Length - 1);
                    isSwipe = true;
                }
            }
        }

        // ���� �������� �÷��̾ �̵���Ŵ
        Vector3 targetPosition = lanes[currentLaneIndex].position;
        targetPosition.y = player.position.y;
        player.position = Vector3.MoveTowards(player.position, targetPosition, moveSpeed * Time.deltaTime);

        if (player.position == targetPosition)
        {
            isSwipe = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = true; // �ε����� �ٽ� ���� �̵��ϵ��� ����
            Debug.Log("�÷��̾�� �ε���");
        }
        else if (collision.gameObject.CompareTag("EndPoint"))
        {
            Debug.Log("��������Ʈ�� ����");
            transform.position = startPoint.position; // endPoint�� �����ϸ� startPoint�� �̵�
        }
    }
}
