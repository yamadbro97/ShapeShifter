using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snaphere : MonoBehaviour
{
    public GameObject Circle;
    public float snapRange = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check the distance to the static circle
        float distance = Vector3.Distance(transform.position, Circle.transform.position);
        print(distance);
         if (distance <= snapRange && Input.GetMouseButtonUp(0))
         {
             // Snap to the position of the static circle
             Circle.transform.position = transform.position;
         }

    }
}
