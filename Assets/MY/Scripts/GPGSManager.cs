using Firebase.Auth;
using Firebase.Extensions;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using UnityEngine;

public class GPGSManager : MonoBehaviour
{
    private FirebaseAuth auth;

    void Start()
    {
        // Firebase ���� �ν��Ͻ� ��������
        auth = FirebaseAuth.DefaultInstance;

        // GPGS �ʱ�ȭ
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        // GPGS �α���
        SignInWithGooglePlay();
    }

    private void SignInWithGooglePlay()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                string idToken = PlayGamesPlatform.Instance.GetIdToken();
                Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

                auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
                {
                    if (task.IsCompleted && !task.IsFaulted)
                    {
                        Debug.Log("Firebase ���� ����");
                        string userId = auth.CurrentUser.UserId;
                        // GPGSManager�� userId�� �ٸ� ��ũ��Ʈ�� ����
                  //      Gold.Instance.SetUserId(userId);
                 //       InventoryManager.Instance.SetUserId(userId);
                    }
                    else
                    {
                        Debug.LogError("Firebase ���� ����: " + task.Exception);
                    }
                });
            }
            else
            {
                Debug.LogError("GPGS �α��� ����");
            }
        });
    }
}
