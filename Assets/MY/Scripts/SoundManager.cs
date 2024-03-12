using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Button soundButton;
    public AudioSource soundSource;

    private bool soundPlaying = true;

    private void Start()
    {
        soundButton.onClick.AddListener(SoundOnOff);
        soundSource = GetComponent<AudioSource>();
    }

    void SoundOnOff()
    {
        if(soundPlaying)
        {
            soundSource.Pause();
            soundPlaying = false;
        }
        else
        {
            soundSource.UnPause();
            soundPlaying = true;
        }
        //soundPlaying = !soundPlaying;
        
    }
}
