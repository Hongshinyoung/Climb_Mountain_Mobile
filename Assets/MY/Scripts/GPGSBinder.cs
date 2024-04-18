using GooglePlayGames;
using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.BasicApi.SavedGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPGSBinder : MonoBehaviour
{
    static GPGSBinder instance = new GPGSBinder();
    public static GPGSBinder Instance => instance;

    ISavedGameClient savedGame => 
        PlayGamesPlatform.Instance.SavedGame;
    IEventsClient Events =>
        PlayGamesPlatform.Instance.Events;

    void Init()
    {

    }

}
