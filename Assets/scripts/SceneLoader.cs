using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string LevelName;
    public void LoadScene()
    {
        Debug.Log(UnityEngine.SceneManagement.SceneManager.sceneCount);
        if (this.name == "Restart_Button")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        else
        {
            Debug.Log(SceneManager.GetAllScenes().Length);

            SceneManager.LoadScene(LevelName);
        }
        Debug.Log(SceneManager.GetActiveScene().name);
    }
}
