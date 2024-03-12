using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Setting : MonoBehaviour
{
    public Vector3 cameraPos;
    public Transform player;

    void Update()
    {
        transform.position = player.transform.position + cameraPos;
    }
}
