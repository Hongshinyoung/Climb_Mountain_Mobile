using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMap : MonoBehaviour
{
    public Transform player;
    public Transform startPoint;
    public Transform endPoint;
    public float moveSpeed = 1.5f;

    void Update()
    {
        // 플레이어가 끝 지점에 도달하면 시작 지점으로 이동
        if (player.position.x < endPoint.position.x) //플레이어 위치 고정(x=0)
        {
            ResetMap();
        }
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    void ResetMap()
    {
        
        player.position = startPoint.position;
    }
}
