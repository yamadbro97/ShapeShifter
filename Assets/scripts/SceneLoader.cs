using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string SceneName;
    private string InputMethod;
    public void LoadScene()
    {
        if (this.name == "Restart_Button")
        {
            if (SceneLoader2.IsTouch == false) InputMethod = "Controller";
            if (SceneLoader2.IsTouch == true) InputMethod = "Touch";
            CSVManager.AppendToUserData(new string[7]
            { Scene_Manager.UserID,
                 SceneLoader2.UserDataSceneName,
                InputMethod,
                Scene_Manager.TriesAmount.ToString(),
                snaphere.UserDataMoveCounter.ToString(),
                timer.UserDataTime,
                "RESTART"
            });
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Scene_Manager.TriesAmount++;
            Debug.Log("This is Try number:"+Scene_Manager.TriesAmount);
        }
        else
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
