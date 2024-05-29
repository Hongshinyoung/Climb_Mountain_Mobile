using UnityEngine;
using System.Collections;

public class Gold : MonoBehaviour
{
    public static Gold Instance { get; private set; }
    private int gold = 130;
    private Coroutine autoIncreaseCoroutine;
    public Item item;
    private FirebaseManager firebaseManager;
    private string userId;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // 다른 GoldManager가 이미 있다면 이 오브젝트를 파괴

        firebaseManager = FindObjectOfType<FirebaseManager>();
    }

    private void Start()
    {
        autoIncreaseCoroutine = StartCoroutine(AutoIncreaseGold());
    }

    public void SetUserId(string userId)
    {
        this.userId = userId;
        LoadGoldData();
    }

    public int GetGold()
    {
        return gold;
    }

    public void SetGold(int updateGold)
    {
        gold = updateGold;
        SaveGoldData();
    }

    public void EarnGold()
    {
        gold += item.itemPrice;
        SaveGoldData();
    }

    public bool UseGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            SaveGoldData();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SaveGoldData()
    {
        if (!string.IsNullOrEmpty(userId))
        {
            firebaseManager.SaveGoldData(userId, gold);
        }
    }

    private void LoadGoldData()
    {
        if (!string.IsNullOrEmpty(userId))
        {
            firebaseManager.LoadGoldData(userId, (loadedGold) =>
            {
                if (loadedGold != -1)
                {
                    gold = loadedGold;
                }
            });
        }
    }

    private void IncreaseGold()
    {
        gold += 1;
        SaveGoldData();
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
