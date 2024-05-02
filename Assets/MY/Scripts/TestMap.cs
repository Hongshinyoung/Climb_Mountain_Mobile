using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMap : MonoBehaviour
{
    public Transform player;
    public Transform startPoint;
    public Transform endPoint;
    public float moveSpeed = 1.5f;

    void Update()
    {

        if (player.position.x < endPoint.position.x) 
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