using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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

            foreach (GameObject snapPosition in snapPositions)
            {
                if (circleCollider.OverlapPoint(snapPosition.transform.position))
                {

                    transform.position = snapPosition.transform.position;
                    circleCollider.isTrigger = true; // unnecessary
                    Debug.Log(splineController.pointPrefabs);
                    for (int i = 0; i < splineController.pointPrefabs.Length; i++)
                    {

                        if (name == "Circle_" + i)
                        {
                            Debug.Log(name == "Circle_" + i);
                            Debug.Log(name);
                            splineController.spline.SetPosition(i, snapPosition.transform.position);
                            splineController.spriteShapeController.BakeMesh();
                            splineController.UpdatePointIndicator(i, snapPosition.transform.position);

                        }
                    }

                }
            }

        }
        else
        {
            circleCollider.isTrigger = false; // unnecessary
        }

    }
    
}
