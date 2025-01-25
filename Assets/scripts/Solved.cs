using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Solved : MonoBehaviour
{
    public List<Range> Ranges;
    public List<SymmAngles> SymAngles;
    public SpriteShapeController Splines;
    public GameObject WinCanvas;
    public GameObject Timer;
    public GameObject LoseCanvas;
    private float time;
    private float timeduration;
    private string InputMethod;
    private bool Finished = false;
    public bool giveUp = false;
    private bool secretbool = false;
    private bool secretbool2 = false;
    



    public float[] Angles;
    public float[] SortedAngles;

    // Start is called before the first frame update
    void Start()
    {
        Angles = new float[Splines.spline.GetPointCount()];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) || !SplineController2.IsSelected)
        {
            StartCoroutine(IsSolved());
        }
        IsNotSolved();
    }
   public float CalcAngle(int i)
    {
        
            if (i == 0)
            {
              return GetAngle(Splines.spline.GetPosition(Splines.spline.GetPointCount() - 1), Splines.spline.GetPosition(i), Splines.spline.GetPosition(i + 1));

            }
            else if (i == Splines.spline.GetPointCount() - 1)
            {
               return GetAngle(Splines.spline.GetPosition(i - 1), Splines.spline.GetPosition(i), Splines.spline.GetPosition(0));

            }
            else
            {
               return GetAngle(Splines.spline.GetPosition(i - 1), Splines.spline.GetPosition(i), Splines.spline.GetPosition(i + 1));

            }
        
        
    }
    public  float GetAngle(Vector2 position1, Vector2 position2, Vector2 position3)
    {
        // Calculate direction vectors
        Vector2 dir1 = position2 - position1;
        Vector2 dir2 = position2 - position3;

        // Calculate the magnitudes of the direction vectors
        float mag1 = dir1.magnitude;
        float mag2 = dir2.magnitude;

        // Calculate the dot product
        float dotProduct = Vector2.Dot(dir1, dir2);

        // Calculate the angle in radians
        float angleRad = Mathf.Acos(dotProduct / (mag1 * mag2));

        // Convert radians to degrees
        float angleDeg = angleRad * Mathf.Rad2Deg;
        //Debug.Log(angleDeg);
        return angleDeg;

    }
    public void GaveUp(bool setvalue)
    {
        giveUp =setvalue;
    }
    IEnumerator IsSolved()
    {
        int counter = 0;
        int symcount = 0;
        int n = 0;
        if (SceneLoader2.IsTouch == false) InputMethod = "Controller";
        if (SceneLoader2.IsTouch == true) InputMethod = "Touch";
        for (int j = 1; j < Angles.Length; j++)
        {
            if (Splines.spline.GetPosition(j).x < Splines.spline.GetPosition(n).x || (Splines.spline.GetPosition(n).x == Splines.spline.GetPosition(j).x && Splines.spline.GetPosition(j).y >= Splines.spline.GetPosition(n).y))
            {
                n = j;
            }
        }
        for (int j = 0; j < Angles.Length; j++)
        {

            Angles[j] = CalcAngle((n + j) % Angles.Length);

        }
      



        for (int i = 0;i < Splines.spline.GetPointCount();i++)
        {
            if(Ranges[i].min <= Angles[i] && Angles[i] <= Ranges[i].max)
            {
                // Counter checks if all edges are in the correct Range
                counter++;
                //Debug.Log("correct edges:" + counter);
            }
        }
        for (int i = 0; i < SymAngles.Count; i++)
        {
            if (Angles[SymAngles[i].Angle_1] == Angles[SymAngles[i].Angle_2])
            {
                // counts how many Angles have the same Value
                symcount++;
                //Debug.Log("similar edges:" + symcount);

            }
        }
      if(counter == Splines.spline.GetPointCount() && symcount == SymAngles.Count && secretbool ==false)
      {
            secretbool = true;
            Finished = true;
            yield return new WaitForSeconds(0.5f);
            WinCanvas.SetActive(true);
            Timer.SetActive(false);
            if (GameObject.Find("Canvas"))
            {
                GameObject.Find("Canvas").SetActive(false);
            }

            // code to save all relevant information into a csv file here
            /*
             * .
             * .
             * .*/
           
            CSVManager.AppendToUserData(new string[7]
            { Scene_Manager.UserID,
                 SceneLoader2.UserDataSceneName,
                InputMethod,
                Scene_Manager.TriesAmount.ToString(),
                snaphere.UserDataMoveCounter.ToString(),
                timer.UserDataTime,
                Finished.ToString()
            }) ;
            /* * .
             * .
             * .
             * */
            //Reset some Values After Saving Information after every Level Here:
            Scene_Manager.TriesAmount = 1;
      }
    }
    void IsNotSolved()
    {
        if (SceneLoader2.IsTouch == false) InputMethod = "Controller";
        if (SceneLoader2.IsTouch == true) InputMethod = "Touch";
        if ((timer.time >= timer.timerDuration || giveUp == true) && secretbool2 == false)
        {
            secretbool2 = true;
            Finished = false;
            Timer.SetActive(false);
            //LoseCanvas.SetActive(true);
            if (GameObject.Find("Canvas"))
            {
                GameObject.Find("Canvas").SetActive(false);
            }
            CSVManager.AppendToUserData(new string[7]
           { Scene_Manager.UserID,
                 SceneLoader2.UserDataSceneName,
                InputMethod,
                Scene_Manager.TriesAmount.ToString(),
                snaphere.UserDataMoveCounter.ToString(),
                timer.UserDataTime,
                Finished.ToString()
           });
            Scene_Manager.TriesAmount = 1;
        }
    }

 
    [System.Serializable]
    public struct Range
    {
        public float min;
        public float max;
    }
    [System.Serializable]
    public struct SymmAngles
    {
        public int Angle_1;
        public int Angle_2;
    }
}
