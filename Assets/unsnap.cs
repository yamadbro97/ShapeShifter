using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unsnap : MonoBehaviour
{

    public CircleCollider2D circleCollider;
    public SplineController splineController;
    GameObject[] NewCircles;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        GameObject[] Circles = GameObject.FindGameObjectsWithTag("circle");
        foreach (GameObject Circle in Circles) 
        {
            for (int i = 0; i < Circles.Length; i++)
            {
                NewCircles[i] = Circle;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        foreach (GameObject circle in NewCircles)
        {
            Debug.Log(circle.name);
            if (Input.GetMouseButtonUp(0) && circleCollider.OverlapPoint(circle.transform.position))
            {
                Debug.Log("circle detected");
                circle.transform.position = transform.position;
                splineController.spline.SetPosition(splineController.selectedPointIndex, transform.position);
                splineController.spriteShapeController.BakeMesh();
                splineController.UpdatePointIndicator(splineController.selectedPointIndex, transform.position);
            }
        }
        
       /* if (Input.GetMouseButtonUp(0) && circleCollider.OverlapPoint(GameObject.FindWithTag("circle").transform.position))
        {
            Debug.Log("circle detected");
            GameObject.FindWithTag("circle").transform.position = transform.position;
            splineController.spline.SetPosition(splineController.selectedPointIndex, transform.position);
            splineController.spriteShapeController.BakeMesh();
            splineController.UpdatePointIndicator(splineController.selectedPointIndex, transform.position);
        }*/
        
    }
}
