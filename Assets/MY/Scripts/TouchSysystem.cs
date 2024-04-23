using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TouchSysystem : MonoBehaviour
{
    public RepeatMap map;
    private float plusSpeed = 1.2f;
    private float plusSpeedTime = 0.2f;
    private float nativeSpeed = 1.5f;
    public bool isTouched;
    public ParticleSystem touchEffect;

    private void Awake()
    {
        isTouched = Input.touchCount > 0;
    }

    void Update()
    {
        if (Input.touchCount >0 )
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;

                Instantiate(touchEffect, touchPosition, Quaternion.identity);
                StartCoroutine(SpeedUp());
            }
        }
        //if(isTouched)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    StartCoroutine(SpeedUp());
        //    Debug.Log("터치 발생: " + touch.position);
        //}
    }

    IEnumerator SpeedUp()
    {
        map.moveSpeed *= plusSpeed;
        yield return new WaitForSeconds(plusSpeedTime);
        map.moveSpeed = nativeSpeed;
    }

}
