using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class FirebaseManager : MonoBehaviour
{
    public class UserData
    {
        public string userName;
        public string score;
        public string gold;
        public List<Item> items;

        public UserData(string userName, string score, string gold, List<Item> items)
        {
            this.userName = userName;
            this.score = score;
            this.gold = gold;
            this.items = items;
        }
    }

    public InputField userNameField;
    public InputField scoreField;
    public InputField goldField;

    public string userName;
    public string score;
    public string gold;
    public List<Item> items; 

    private DatabaseReference reference;

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveData()
    {
        userName = userNameField.text.Trim();
        score = scoreField.text.Trim();
        gold = goldField.text.Trim();

        var data = new UserData(userName, score, gold, items);
        string jsonData = JsonUtility.ToJson(data);

        reference.Child("Users").Child(userName).SetRawJsonValueAsync(jsonData);
        Debug.Log("저장완료");
    }

    public void LoadData()
    {
        userName = userNameField.text.Trim();

        reference.Child("Users").Child(userName).GetValueAsync().ContinueWith
            (task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("로드 취소");
                }
                else if (task.IsFaulted)
                {
                    Debug.Log("로드 실패");
                }
                else
                {
                    var dataSnapshot = task.Result;

                    string dataString = "";
                    foreach (var data in dataSnapshot.Children)
                    {
                        dataString += data.Key + "" + data.Value + "\n";
                    }
                    Debug.Log(dataString);
                }
            });
    }
}
