using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMap : MonoBehaviour
{
    public Transform player;
    public Transform startPoint;
    public Transform endPoint;
    public float moveSpeed = 1.5f;
    public bool isReset;

    void Update()
    {
        // �÷��̾ �� ������ �����ϸ� ���� �������� �̵�
        if (player.position.x < endPoint.position.x) //�÷��̾� ��ġ ����(x=0)
        {
            ResetMap();
        }
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    void ResetMap()
    {
        player.position = startPoint.position;
        isReset = true;
        if(isReset == true) isReset = false;
    }
}
