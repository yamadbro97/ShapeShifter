using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToStaticCircle : MonoBehaviour
{
    public GameObject staticCircle; // Reference to the StaticCircle GameObject
    public float snapRange = 1.0f; // The range within which snapping will occur

    private Vector3 offset;
    private bool isDragging = false;

    void Start()
    {
        // Ensure the staticCircle reference is set
        if (staticCircle == null)
        {
            Debug.LogError("StaticCircle is not assigned!");
        }
    }

    void OnMouseDown()
    {
        // Calculate offset between object and mouse position
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseDrag()
    {
        // Update the object's position as the mouse moves
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        // Check the distance to the static circle
        float distance = Vector3.Distance(transform.position, staticCircle.transform.position);
        if (distance <= snapRange)
        {
            // Snap to the position of the static circle
            transform.position = staticCircle.transform.position;
        }
    }

    Vector3 GetMouseWorldPos()
    {
        // Convert mouse position to world position in 2D
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 0; // Set z to 0 for 2D
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}

