using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<SplineController> draggableobjects;
    public float snapRange = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        foreach(SplineController draggable in draggableobjects)
        {
            draggable.dragEndedCallback = OnDragEnded;
        }
    }

    // Update is called once per frame
    public void OnDragEnded(SplineController draggable)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;

        foreach(Transform SnapPoint in  snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.localPosition, SnapPoint.localPosition);
            if(closestSnapPoint == null || currentDistance < closestDistance)
            {
                closestSnapPoint = SnapPoint;
                closestDistance = currentDistance;
            }
        }
        if (closestSnapPoint != null && closestDistance <= snapRange) 
        {
            draggable.transform.localPosition = closestSnapPoint.localPosition;
        }
    }
}
