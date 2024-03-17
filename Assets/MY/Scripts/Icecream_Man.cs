using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Icecream_Man : MonoBehaviour
{
    public GameObject iceMan;
    public GameObject iceCart;
    public GameObject menu;
    private bool isAppeared;
    public Button baBamBa;
    public RepeatMap map;
    public PlayerMove move;

    void Appeared()
    {
        iceMan.SetActive(true);
        iceCart.SetActive(true);
        isAppeared = true;
        ShowMenu();
        Invoke("Disappeared", 10); //10초간 아이스크림 아저씨 등장
    }

    void Disappeared()
    {
        iceMan.SetActive(false);
        iceCart.SetActive(false);
        isAppeared=false;
    }

    void ShowMenu()
    {
            menu.gameObject.SetActive(true);
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
        iceCart.SetActive(false);
        isAppeared = false;
        menu.SetActive(false);
        InvokeRepeating("Appeared", 3f, Random.Range(10f, 50f));
        baBamBa.onClick.AddListener(EatBabamba);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
