using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToStaticCircle : MonoBehaviour
{
    public GameObject staticCircle; // Reference to the StaticCircle GameObject
    public float snapRange = 1.0f; // The range within which snapping will occur

    void Start()
    {
        // Ensure the staticCircle reference is set
        if (staticCircle == null)
        {
            Debug.LogError("StaticCircle is not assigned!");
        }
    }

    void Update()
    {
        // Check the distance to the static circle
        float distance = Vector3.Distance(transform.position, staticCircle.transform.position);
        print(distance);
       /* if (distance <= snapRange && Input.GetMouseButtonUp(0))
        {
            // Snap to the position of the static circle
            transform.position = staticCircle.transform.position;
        } */
    }

    
}

