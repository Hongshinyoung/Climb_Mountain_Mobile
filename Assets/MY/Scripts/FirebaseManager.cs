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
                Debug.Log("골드 데이터 저장 성공");
            }
            else
            {
                Debug.LogError("골드 데이터 저장 실패: " + task.Exception);
            }
        });
    }

    public void LoadGoldData(string userId, Action<int> onGoldLoaded)
    {
        reference.Child("users").Child(userId).Child("gold").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("골드 데이터 로드 실패: " + task.Exception);
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
                    Debug.LogError("골드 데이터를 찾을 수 없습니다.");
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
                Debug.Log("인벤토리 데이터 저장 성공");
            }
            else
            {
                Debug.LogError("인벤토리 데이터 저장 실패: " + task.Exception);
            }
        });
    }

    public void LoadInventoryData(string userId, Action<InventoryManager.ItemListWrapper> onInventoryLoaded)
    {
        reference.Child("users").Child(userId).Child("inventory").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("인벤토리 데이터 로드 실패: " + task.Exception);
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
                    Debug.LogError("인벤토리 데이터를 찾을 수 없습니다.");
                    onInventoryLoaded?.Invoke(null);
                }
            }
        });
    }
    public void SaveRankingData(string userId, string userName, int userScore)
    {
        User user = new User(userName, "", userScore); // 이메일은 여기서 생략 가능
        string json = JsonUtility.ToJson(user);
        reference.Child("rankings").Child(userId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("랭킹 데이터 저장 성공");
            }
            else
            {
                Debug.LogError("랭킹 데이터 저장 실패: " + task.Exception);
            }
        });
    }

    public void LoadRankingData(Action<List<User>> onRankingDataLoaded)
    {
        reference.Child("rankings").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("랭킹 데이터 로드 실패: " + task.Exception);
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
                    Debug.LogError("랭킹 데이터를 찾을 수 없습니다.");
                    onRankingDataLoaded?.Invoke(null);
                }
            }
        });
    }
}
