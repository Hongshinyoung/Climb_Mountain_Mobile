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
        // Firebase �ʱ�ȭ
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;

            if (task.Exception != null)
            {
                Debug.LogError($"Firebase �ʱ�ȭ ����: {task.Exception}");
            }
        });
    }

    // �����ͺ��̽��� ������ ����
    public void WriteData(string key, string value)
    {
        DatabaseReference childReference = reference.Child(key);
        childReference.SetValueAsync(value).ContinueWithOnMainThread(task => {
            if (task.Exception != null)
            {
                Debug.LogError($"������ ���� ����: {task.Exception}");
            }
            else
            {
                Debug.Log("������ ���� ����!");
            }
        });
    }

    // �����ͺ��̽����� ������ �б�
    public void ReadData(string key, System.Action<string> onDataReceived)
    {
        DatabaseReference childReference = reference.Child(key);
        childReference.GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.Exception != null)
            {
                Debug.LogError($"������ �б� ����: {task.Exception}");
            }
            else if (task.Result.Exists)
            {
                string value = task.Result.GetRawJsonValue();
                onDataReceived?.Invoke(value);
            }
            else
            {
                Debug.Log("�����Ͱ� �������� �ʽ��ϴ�.");
            }
        });
    }
}