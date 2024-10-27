using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.U2D;

public class snaphere : MonoBehaviour
{

    public CircleCollider2D circleCollider;
    public SplineController2 splineController;
   
    
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

   void HandleInput()
    {
        // Check if the mouse button is released
        if (Input.GetMouseButtonUp(0))
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
                if (circleCollider.OverlapPoint(closestSnapPosition.transform.position))
                {

                    transform.position = closestSnapPosition.transform.position;
                    for (int i = 0; i < splineController.pointPrefabs.Length; i++)
                    {

                        if (name == "Circle_" + i)
                        {
                            Debug.Log(name);
                            splineController.spline.SetPosition(i, closestSnapPosition.transform.position);
                            splineController.spriteShapeController.BakeMesh();
                            splineController.UpdatePointIndicator(i, closestSnapPosition.transform.position);

                        }
                    }
                }
            
        }
    }


    
}
