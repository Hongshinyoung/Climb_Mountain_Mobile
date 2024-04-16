using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public static Gold Instance { get; private set; }

    private int gold;
    private Coroutine autoIncreaseCoroutine;
    public Item item;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // 다른 GoldManager가 이미 있다면 이 오브젝트를 파괴
    }

    private void Start()
    {
        autoIncreaseCoroutine = StartCoroutine(AutoIncreaseGold());
    }

    //private void OnDisable()
    //{
    //    if (autoIncreaseCoroutine != null)
    //        StopCoroutine(autoIncreaseCoroutine);
    //}

    public int GetGold()
    {
        return gold;
    }

    public void EarnGold() //sell
    {
        gold += item.itemPrice;
    }

    public void UseGold()
    {
        if (gold >= item.itemPrice)
        {
            gold = gold - item.itemPrice;
        }
        else Debug.Log("돈이 부족합니다.");
    }

    private void IncreaseGold()
    {
        gold += 1;
    }

    IEnumerator AutoIncreaseGold()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            IncreaseGold();
        }
    }
}
