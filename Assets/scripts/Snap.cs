using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnapToStaticCircle : MonoBehaviour
{
    void Update()
    {
        Debug.Log(GameObject.Find("snapcircle").GetComponent<CircleCollider2D>().isTrigger);
    }
    // This method is called when the object enters a trigger collider
    private void OnTriggerEnter2D(Collider2D other)
        {
        Debug.Log(other.gameObject.name);
        
            // Check if the other object has the tag "snapposition"
            if (other.CompareTag("SnapPosition"))
            {
            Debug.Log("it should work");
                // Snap to the position of the object with the "snapposition" tag
                transform.position = other.transform.position;
            
            }
        }
    
    //public float snapRange = 1f;
    //private Collider2D other;

    

    /*void Update()
    {
        if (other.CompareTag("snapPosition"))
        {
            // Check the distance to the static circle
            float distance = Vector3.Distance(transform.position, other.transform.position);
            print(distance);
            if (distance <= snapRange && Input.GetMouseButtonUp(0))
            {
                // Snap to the position of the static circle
                transform.position = other.transform.position;
            }
        }
    }*/

    
}

