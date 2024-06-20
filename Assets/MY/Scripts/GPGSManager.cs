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
        // Firebase 인증 인스턴스 가져오기
        auth = FirebaseAuth.DefaultInstance;

        // GPGS 초기화
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        // GPGS 로그인
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
                        Debug.Log("Firebase 인증 성공");
                        string userId = auth.CurrentUser.UserId;
                        // GPGSManager의 userId를 다른 스크립트에 전달
                  //      Gold.Instance.SetUserId(userId);
                 //       InventoryManager.Instance.SetUserId(userId);
                    }
                    else
                    {
                        Debug.LogError("Firebase 인증 실패: " + task.Exception);
                    }
                });
            }
            else
            {
                Debug.LogError("GPGS 로그인 실패");
            }
        });
    }
}
