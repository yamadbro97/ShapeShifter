using UnityEngine;

public class SnaptoPlace : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Transform snapPositionTransform = null;

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
        // Snap to the designated position if in proximity
        if (snapPositionTransform != null)
        {
            transform.position = snapPositionTransform.position;
        }
    }

    Vector3 GetMouseWorldPos()
    {
        // Convert mouse position to world position in 2D
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 0; // Set z to 0 for 2D
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object we collided with is the snap position
        if (other.gameObject.name == "StaticCircle")
        {
            snapPositionTransform = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object we exited from is the snap position
        if (other.gameObject.name == "StaticCircle")
        {
            snapPositionTransform = null;
        }
    }
}
