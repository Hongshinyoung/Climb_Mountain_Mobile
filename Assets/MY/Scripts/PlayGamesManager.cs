//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//using UnityEngine.UI;

//public class PlayGamesManager : MonoBehaviour
//{
//    public Text text;
//    // Start is called before the first frame update
//    void Start()
//    {
//        SignIn();
//    }

//    public void SignIn()
//    {
//        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
//    }

//    internal void ProcessAuthentication(SignInStatus status)
//    {
//        if (status == SignInStatus.Success)
//        {
//            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
//            string id = PlayGamesPlatform.Instance.GetUserId();
//            text.text = "환영합니다 " + name +" 님";
//            // Continue with Play Games Services
//        }
//        else
//        {
//            Debug.Log("로그인 안됨");
//            // Disable your integration with Play Games Services or show a login button
//            // to ask users to sign-in. Clicking it should call
//            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
