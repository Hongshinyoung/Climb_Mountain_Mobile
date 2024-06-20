using System.Collections;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using Firebase;



public class TestFirebase : MonoBehaviour
{
    DatabaseReference reference;

    void Start()
    {
        // Firebase 초기화
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;

            if (task.Exception != null)
            {
                Debug.LogError($"Firebase 초기화 오류: {task.Exception}");
            }
        });
    }

    // 데이터베이스에 데이터 쓰기
    public void WriteData(string key, string value)
    {
        DatabaseReference childReference = reference.Child(key);
        childReference.SetValueAsync(value).ContinueWithOnMainThread(task => {
            if (task.Exception != null)
            {
                Debug.LogError($"데이터 쓰기 오류: {task.Exception}");
            }
            else
            {
                Debug.Log("데이터 쓰기 성공!");
            }
        });
    }

    // 데이터베이스에서 데이터 읽기
    public void ReadData(string key, System.Action<string> onDataReceived)
    {
        DatabaseReference childReference = reference.Child(key);
        childReference.GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.Exception != null)
            {
                Debug.LogError($"데이터 읽기 오류: {task.Exception}");
            }
            else if (task.Result.Exists)
            {
                string value = task.Result.GetRawJsonValue();
                onDataReceived?.Invoke(value);
            }
            else
            {
                Debug.Log("데이터가 존재하지 않습니다.");
            }
        });
    }
}