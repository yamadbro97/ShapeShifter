using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.U2D;

public class snaphere : MonoBehaviour
{
    public static int UserDataMoveCounter;
    public CircleCollider2D circleCollider;
    public SplineController2 splineController;
    Vector3 LastPosition;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        LastPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }
    void HandleInput()
   {
        // Check if the mouse button is released
        if ((Input.GetMouseButtonUp(0) || Input.GetButtonUp("1")) && !SplineController2.IsSelected)
        {
            GameObject[] snapPositions = GameObject.FindGameObjectsWithTag("SnapPosition");
            float closestTarget = float.MaxValue;
            GameObject closestSnapPosition = null;
            foreach (GameObject snapPosition in snapPositions)
            {
                // Calculate distance from the circle to the Target positions transform position
                float distance = Vector2.Distance(transform.position, snapPosition.transform.position);

                if (distance < closestTarget)
                {
                    closestTarget = distance;
                    closestSnapPosition = snapPosition;
                }
            }
           

            for (int i = 0; i < splineController.pointPrefabs.Length; i++)
            {
               // Debug.Log(name == "Circle_" + i);
                if (name == "Circle_" + i)
                {
                    if (circleCollider.OverlapPoint(closestSnapPosition.transform.position))
                    {
                        if(LastPosition != closestSnapPosition.transform.position) 
                        {
                             splineController.move_counter++;
                            UserDataMoveCounter = splineController.move_counter;
                            Debug.Log("Amount of moves:" + splineController.move_counter);
                        }
                        splineController.spline.SetPosition(i, closestSnapPosition.transform.position);
                        splineController.spriteShapeController.BakeMesh();
                        splineController.UpdatePointIndicator(i, closestSnapPosition.transform.position);

                        // CHANGE ORIGINAL POSITION TO THE POSITION THAT IT BECAME, AND NOT THE ONE AT THE START OF THE GAME
                        LastPosition = closestSnapPosition.transform.position;
                    }
                    else
                    {
                       // Debug.Log(name);
                        splineController.spline.SetPosition(i, LastPosition);
                        splineController.spriteShapeController.BakeMesh();
                        splineController.UpdatePointIndicator(i, LastPosition);
                    }
                }
            }
        }            
   }

    void DisableOtherInstances()
    {
        // Find all instances of snaphere in the scene
        snaphere[] allInstances = FindObjectsOfType<snaphere>();

        foreach (var instance in allInstances)
        {
            if (instance != this)
            {
                instance.enabled = false; // Disable Update for other instances
            }
        }
    }

    void ReEnableOtherInstances()
    {
        // Find all instances of snaphere in the scene
        snaphere[] allInstances = FindObjectsOfType<snaphere>();

        foreach (var instance in allInstances)
        {
            if (instance != this)
            {
                instance.enabled = true; // Re-enable Update for other instances
            }
        }
    }
}


    