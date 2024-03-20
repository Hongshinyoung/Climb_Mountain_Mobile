using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public Camera miniGameCam;
    public Enemy enemy;
    public void StartMiniGame()
    {
        Camera cam = Camera.main;
        cam.gameObject.SetActive(false);
        miniGameCam.gameObject.SetActive(true);
        enemy.InvokeRepeating("AppearRandomly", 3f, Random.Range(1f, 5f));
    }

    public void ExitMiniGame()
    {
        miniGameCam.gameObject.SetActive(false);
        Camera.main.gameObject.SetActive(true);
        enemy.InvokeRepeating("AppearRandomly", 3f, Random.Range(100f, 500f));
    }
}
