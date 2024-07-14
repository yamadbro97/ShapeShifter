using UnityEngine;
using UnityEngine.U2D;

public class SplineEditor : MonoBehaviour
{
    public SpriteShapeController spriteShapeController;
    private Spline spline;
    private int selectedPointIndex = -1;

    void Start()
    {
        spline = spriteShapeController.spline;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Detect mouse click to select a spline point
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            selectedPointIndex = GetClosestPointIndex(mousePosition);
        }

        // Move the selected spline point with the mouse
        if (Input.GetMouseButton(0) && selectedPointIndex != -1)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            spline.SetPosition(selectedPointIndex, mousePosition);
            spriteShapeController.BakeMesh(); // Update the mesh
        }

        // Deselect point on mouse up
        if (Input.GetMouseButtonUp(0))
        {
            selectedPointIndex = -1;
        }
    }

    int GetClosestPointIndex(Vector3 position)
    {
        float minDistance = float.MaxValue;
        int closestPointIndex = -1;

        for (int i = 0; i < spline.GetPointCount(); i++)
        {
            float distance = Vector3.Distance(position, spline.GetPosition(i));
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPointIndex = i;
            }
        }

        return closestPointIndex;
    }
}
