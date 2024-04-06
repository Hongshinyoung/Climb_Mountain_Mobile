using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Icecream_Man : MonoBehaviour
{
    public GameObject iceMan;
    public GameObject menu;
    private bool isAppeared;
    public Button baBamBa;
    public RepeatMap map;
    public PlayerMove move;

    void Appeared()
    {
        iceMan.SetActive(true);
        isAppeared = true;
        menu.gameObject.SetActive(true);
        Invoke("Disappeared", 7); //7초간 아이스크림 아저씨 등장
    }

    void Disappeared()
    {
        iceMan.SetActive(false);
        isAppeared=false;
        menu.gameObject.SetActive(false);
    }


    void EatBabamba()
    {
        StartCoroutine(babambaing());
    }

    IEnumerator babambaing()
    {
        move.animator.SetBool("realFast", true);
        map.moveSpeed = 30;
        Disappeared();
        yield return new WaitForSeconds(10);
        move.ResetToWalk();
    }
    // Start is called before the first frame update
    void Start()
    {
        iceMan.SetActive(false);
        isAppeared = false;
        menu.gameObject.SetActive(false);
        //InvokeRepeating("Appeared", 3f, Random.Range(200f, 400f));
        baBamBa.onClick.AddListener(EatBabamba);
        InvokeRepeating("Appeared",Random.Range(100,200), Random.Range(200, 400));
    }

}
