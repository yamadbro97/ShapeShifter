using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snaphere : MonoBehaviour
{

    private CircleCollider2D circleCollider;
    public SplineController splineController;
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
            if (circleCollider.OverlapPoint(GameObject.FindWithTag("SnapPosition").transform.position))
            {
                transform.position = GameObject.FindWithTag("SnapPosition").transform.position;
                
            }

        }
    }
}
