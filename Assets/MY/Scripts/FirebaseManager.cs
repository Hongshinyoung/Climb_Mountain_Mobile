using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using System;
using System.Collections.Generic;

public class User
{
    public string username;
    public string email;
    public int score;

    public User() { }

    public User(string username, string email, int score)
    {
        this.username = username;
        this.email = email;
        this.score = score;
    }
}


public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference reference;

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveGoldData(string userId, int gold)
    {
        reference.Child("users").Child(userId).Child("gold").SetValueAsync(gold).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("��� ������ ���� ����");
            }
            else
            {
                Debug.LogError("��� ������ ���� ����: " + task.Exception);
            }
        });
    }

    public void LoadGoldData(string userId, Action<int> onGoldLoaded)
    {
        reference.Child("users").Child(userId).Child("gold").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("��� ������ �ε� ����: " + task.Exception);
                onGoldLoaded?.Invoke(-1);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    onGoldLoaded?.Invoke(Convert.ToInt32(snapshot.Value));
                }
                else
                {
                    Debug.LogError("��� �����͸� ã�� �� �����ϴ�.");
                    onGoldLoaded?.Invoke(-1);
                }
            }
        });
    }

    public void SaveInventoryData(string userId, string json)
    {
        reference.Child("users").Child(userId).Child("inventory").SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("�κ��丮 ������ ���� ����");
            }
            else
            {
                Debug.LogError("�κ��丮 ������ ���� ����: " + task.Exception);
            }
        });
    }

    public void LoadInventoryData(string userId, Action<InventoryManager.ItemListWrapper> onInventoryLoaded)
    {
        reference.Child("users").Child(userId).Child("inventory").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("�κ��丮 ������ �ε� ����: " + task.Exception);
                onInventoryLoaded?.Invoke(null);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    InventoryManager.ItemListWrapper items = JsonUtility.FromJson<InventoryManager.ItemListWrapper>(snapshot.GetRawJsonValue());
                    onInventoryLoaded?.Invoke(items);
                }
                else
                {
                    Debug.LogError("�κ��丮 �����͸� ã�� �� �����ϴ�.");
                    onInventoryLoaded?.Invoke(null);
                }
            }
        });
    }
    public void SaveRankingData(string userId, string userName, int userScore)
    {
        User user = new User(userName, "", userScore); // �̸����� ���⼭ ���� ����
        string json = JsonUtility.ToJson(user);
        reference.Child("rankings").Child(userId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("��ŷ ������ ���� ����");
            }
            else
            {
                Debug.LogError("��ŷ ������ ���� ����: " + task.Exception);
            }
        });
    }

    public void LoadRankingData(Action<List<User>> onRankingDataLoaded)
    {
        reference.Child("rankings").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("��ŷ ������ �ε� ����: " + task.Exception);
                onRankingDataLoaded?.Invoke(null);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    List<User> rankingList = new List<User>();
                    foreach (DataSnapshot child in snapshot.Children)
                    {
                        User user = JsonUtility.FromJson<User>(child.GetRawJsonValue());
                        rankingList.Add(user);
                    }
                    onRankingDataLoaded?.Invoke(rankingList);
                }
                else
                {
                    Debug.LogError("��ŷ �����͸� ã�� �� �����ϴ�.");
                    onRankingDataLoaded?.Invoke(null);
                }
            }
        });
    }
}
