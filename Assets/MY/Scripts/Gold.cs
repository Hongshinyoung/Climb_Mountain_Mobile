using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public static Gold Instance { get; private set; }

    private int gold = 130;
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

    public void SetGold(int updataGold)
    {
        gold = updataGold;
    }

    public void EarnGold() //sell
    {
        gold += item.itemPrice;
    }

    public bool UseGold(int amount) //buy
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true;
        }
        else
        {
            return false;
        }
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