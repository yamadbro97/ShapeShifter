using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private bool isDragging = false;
    private CircleCollider2D circleCollider;
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
        // Check if the mouse is over the object
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        // Check if the mouse button is pressed down
        if (Input.GetMouseButtonDown(0))
        {
            if (circleCollider.OverlapPoint(mousePosition))
            {
                isDragging = true;
            }
           
            
        }

        // Check if the mouse button is being held down and the object is being dragged
        if (isDragging && Input.GetMouseButton(0))
        {
            
            transform.position = mousePosition;
        }

        // Check if the mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
           if (circleCollider.OverlapPoint(GameObject.FindWithTag("SnapPosition").transform.position))
            {
                transform.position = GameObject.FindWithTag("SnapPosition").transform.position;
            }

        }
    }
}
