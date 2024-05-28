using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine;
public class User
{
    public string username; // 닉네임
    public string email;
    public int score; // 점수

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
    DatabaseReference reference;

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void writeNewUser(string userId, string name, string email, int score)
    {
        User user = new User(name, email, score);
        string json = JsonUtility.ToJson(user);
        reference.Child("users").Child(userId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("데이터 저장 성공");
            }
            else
            {
                Debug.LogError("데이터 저장 실패: " + task.Exception);
            }
        });
    }

    private void updateUser(string userId, string newName, string newEmail, int score)
    {
        // 사용자의 기존 데이터를 로드합니다
        reference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("데이터 로드 실패: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    User existingUser = JsonUtility.FromJson<User>(snapshot.GetRawJsonValue());
                    existingUser.username = newName;
                    existingUser.email = newEmail;
                    existingUser.score = score;

                    // 수정된 데이터를 다시 저장합니다
                    string updatedJson = JsonUtility.ToJson(existingUser);
                    reference.Child("users").Child(userId).SetRawJsonValueAsync(updatedJson).ContinueWithOnMainThread(saveTask =>
                    {
                        if (saveTask.IsCompleted)
                        {
                            Debug.Log("데이터 업데이트 성공");
                        }
                        else
                        {
                            Debug.LogError("데이터 업데이트 실패: " + saveTask.Exception);
                        }
                    });
                }
                else
                {
                    Debug.LogError("사용자 데이터를 찾을 수 없습니다.");
                }
            }
        });
    }

    public void SaveUserItems()
    {
        DataManager dataManager = DataManager.Instance;
        dataManager.SaveInventoryData();
        dataManager.SaveGoldData();

        Debug.Log("아이템 및 골드 데이터 저장 완료");
    }

    public void LoadUserItems()
    {
        DataManager dataManager = DataManager.Instance;
        dataManager.LoadInventoryData();
        dataManager.LoadGoldData();
        Debug.Log("아이템 및 골드 데이터 로드 완료");
    }

    public void SaveRankingData(string userId, string userName, int userScore)
    {
        reference.Child("ranking").Child(userId).SetRawJsonValueAsync(JsonUtility.ToJson(new User(userName, "", userScore))).ContinueWithOnMainThread(task =>
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

    public void LoadRankingData(System.Action<List<User>> onLoaded)
    {
        reference.Child("ranking").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("랭킹 데이터 로드 실패: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                List<User> rankingList = new List<User>();

                foreach (DataSnapshot userSnapshot in snapshot.Children)
                {
                    User user = JsonUtility.FromJson<User>(userSnapshot.GetRawJsonValue());
                    rankingList.Add(user);
                }

                onLoaded?.Invoke(rankingList);
                Debug.Log("랭킹 데이터 로드 성공");
            }
        });
    }
}
