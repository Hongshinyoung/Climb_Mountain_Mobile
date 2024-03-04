using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public string SceneName;
    public void SceneLoad()
    {
        SceneManager.LoadScene(SceneName);
    }
    public void SceneQuit()
    {
        Application.Quit();
    }
}
