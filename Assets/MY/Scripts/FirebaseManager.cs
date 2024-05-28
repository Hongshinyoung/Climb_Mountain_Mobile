using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine;
public class User
{
    public string username; // �г���
    public string email;
    public int score; // ����

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
                Debug.Log("������ ���� ����");
            }
            else
            {
                Debug.LogError("������ ���� ����: " + task.Exception);
            }
        });
    }

    private void updateUser(string userId, string newName, string newEmail, int score)
    {
        // ������� ���� �����͸� �ε��մϴ�
        reference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("������ �ε� ����: " + task.Exception);
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

                    // ������ �����͸� �ٽ� �����մϴ�
                    string updatedJson = JsonUtility.ToJson(existingUser);
                    reference.Child("users").Child(userId).SetRawJsonValueAsync(updatedJson).ContinueWithOnMainThread(saveTask =>
                    {
                        if (saveTask.IsCompleted)
                        {
                            Debug.Log("������ ������Ʈ ����");
                        }
                        else
                        {
                            Debug.LogError("������ ������Ʈ ����: " + saveTask.Exception);
                        }
                    });
                }
                else
                {
                    Debug.LogError("����� �����͸� ã�� �� �����ϴ�.");
                }
            }
        });
    }

    public void SaveUserItems()
    {
        DataManager dataManager = DataManager.Instance;
        dataManager.SaveInventoryData();
        dataManager.SaveGoldData();

        Debug.Log("������ �� ��� ������ ���� �Ϸ�");
    }

    public void LoadUserItems()
    {
        DataManager dataManager = DataManager.Instance;
        dataManager.LoadInventoryData();
        dataManager.LoadGoldData();
        Debug.Log("������ �� ��� ������ �ε� �Ϸ�");
    }

    public void SaveRankingData(string userId, string userName, int userScore)
    {
        reference.Child("ranking").Child(userId).SetRawJsonValueAsync(JsonUtility.ToJson(new User(userName, "", userScore))).ContinueWithOnMainThread(task =>
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

    public void LoadRankingData(System.Action<List<User>> onLoaded)
    {
        reference.Child("ranking").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("��ŷ ������ �ε� ����: " + task.Exception);
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
                Debug.Log("��ŷ ������ �ε� ����");
            }
        });
    }
}
