using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Threading;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader2 : MonoBehaviour
{
   //public Scene_Manager Scene_Manager_counter;
    int counter;
    public static bool IsTouch;
    private void Start()
    {
        counter = 0;
        
    }
    public void SceneLoader()
    {
        int x;
        if (this.name == "Controller")
        {
            IsTouch = false;
            Debug.Log(this.name);
        }
        if (this.name == "Touch")
        {
            IsTouch = true;
            Debug.Log(this.name);
        }

        if (Scene_Manager.SceneCounter<3)
        {
           
            x = UnityEngine.Random.Range(0, Scene_Manager.Easy.Count);
            SceneManager.LoadScene(Scene_Manager.Easy[x]);
            Scene_Manager.Easy.Remove(Scene_Manager.Easy[x]);
            Debug.Log("Easy_Levels left:"+Scene_Manager.Easy.Count);
            Debug.Log("this is scene number:" + Scene_Manager.SceneCounter);
            

        }
        if( 2< Scene_Manager.SceneCounter && Scene_Manager.SceneCounter < 5)
        {

            x = UnityEngine.Random.Range(0, Scene_Manager.Medium.Count);
            SceneManager.LoadScene(Scene_Manager.Medium[x]);
            Scene_Manager.Medium.Remove(Scene_Manager.Medium[x]);
            Debug.Log("Medium_Levels left:" + Scene_Manager.Medium.Count);
            Debug.Log("this is scene number:" + Scene_Manager.SceneCounter);
            
        }
        if (Scene_Manager.SceneCounter == 5)
        {

            x = UnityEngine.Random.Range(0, Scene_Manager.Hard.Count);
            SceneManager.LoadScene(Scene_Manager.Hard[x]);
            Scene_Manager.Hard.Remove(Scene_Manager.Hard[x]);
            Debug.Log("Hard_Levels left:" + Scene_Manager.Hard.Count);
            Debug.Log("this is scene number:" + Scene_Manager.SceneCounter);
           

        }
        if (Scene_Manager.SceneCounter == 6)
        {
            IsTouch = !IsTouch;
            if (IsTouch)
            {
                Debug.Log("NEXT LEVEL SHOULD BE PLAYED VIA TOUCH");
            }
            else if (!IsTouch)
            {
                Debug.Log("NEXT LEVEL SHOULD BE PLAYED VIA CONTROLLER");
            }
        }
        // Pause um interaction method zu wechseln
        if (5<Scene_Manager.SceneCounter && Scene_Manager.SceneCounter< 8)
        {

            x = UnityEngine.Random.Range(0, Scene_Manager.Easy.Count);
            SceneManager.LoadScene(Scene_Manager.Easy[x]);
            Scene_Manager.Easy.Remove(Scene_Manager.Easy[x]);
            Debug.Log("Easy_Levels left:" + Scene_Manager.Easy.Count);
            Debug.Log("this is scene number:" + Scene_Manager.SceneCounter);


        }
        if (7 < Scene_Manager.SceneCounter && Scene_Manager.SceneCounter < 10)
        {

            x = UnityEngine.Random.Range(0, Scene_Manager.Medium.Count);
            SceneManager.LoadScene(Scene_Manager.Medium[x]);
            Scene_Manager.Medium.Remove(Scene_Manager.Medium[x]);
            Debug.Log("Medium_Levels left:" + Scene_Manager.Medium.Count);
            Debug.Log("this is scene number:" + Scene_Manager.SceneCounter);

        }
        if (Scene_Manager.SceneCounter == 10)
        {

            x = UnityEngine.Random.Range(0, Scene_Manager.Hard.Count);
            SceneManager.LoadScene(Scene_Manager.Hard[x]);
            Scene_Manager.Hard.Remove(Scene_Manager.Hard[x]);
            Debug.Log("Hard_Levels left:" + Scene_Manager.Hard.Count);
            Debug.Log("this is scene number:" + Scene_Manager.SceneCounter);

        }
        if(10<Scene_Manager.SceneCounter)
        {
            SceneManager.LoadScene("End_Scene");
        }
        Scene_Manager.SceneCounter++;

    }

}
