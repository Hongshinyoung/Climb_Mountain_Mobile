using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitManager : MonoBehaviour
{
    public GameObject quitPanel;
    public Button[] yesOrNo;

    private void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                quitPanel.SetActive(true);
            }
        }
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
