using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Scene_Manager : MonoBehaviour
{
    public static int SceneCounter=1;
    public static int TriesAmount =1;
    public static string UserID;
    public TMP_InputField inputField;
    public static List<String> Easy = new List<String> { "Level_1", "Level_2", "Level_3", "Level_4" };
    public static List<String> Medium = new List<String> { "Level_5", "Level_6", "Level_7", "Level_8" };
    public static List<String> Hard = new List<String> { "Level_9", "Level_10" };
    // public static Scene_Manager Instance;
    // Start is called before the first frame update
    public void SaveUserID()
    {
        UserID=inputField.text;
        Debug.Log("UserID is:" + UserID);
    }
         /*if (Instance == null)
         {
             Instance = this;
             DontDestroyOnLoad(gameObject);
             SceneManager.sceneLoaded += OnSceneLoaded;  // Subscribe to scene load event
         }
         else
         {
             Destroy(gameObject);  // Prevent duplicate GameManagers
         }
     }

     // Update is called once per frame
     public  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
     {

         SceneCounter++;  // Increment scene counter
         Debug.Log("Scene Counter: " + SceneCounter);
     }*/
}
