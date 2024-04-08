using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public static Gold Instance { get; private set; }

    private int gold;
    private Coroutine autoIncreaseCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // �ٸ� GoldManager�� �̹� �ִٸ� �� ������Ʈ�� �ı�
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
