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
    public void LoadScene()
    {
        if (this.name == "Restart_Button")
        {
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
