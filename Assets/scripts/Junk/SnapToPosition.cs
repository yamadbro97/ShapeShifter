using UnityEngine;

public class SnapToPosition2D : MonoBehaviour
{
    public Transform snapPosition; // The target position to snap to
    public float snapRange = 1.0f; // The distance within which the object will snap to the target position

    private Vector3 offset;
    private bool isDragging = false;

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
        // Check the distance to the snap position
        float distance = Vector3.Distance(transform.position, snapPosition.position);
        if (distance <= snapRange)
        {
            // Snap to the designated position
            transform.position = snapPosition.position;
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
