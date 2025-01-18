using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickTest : MonoBehaviour
{
    private bool IsGrabbed= false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal")>0f)
        {
            Debug.Log("Moving Right");
        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            Debug.Log("Moving Left");
        }
        if (Input.GetAxis("Vertical") > 0f)
        {
            Debug.Log("Moving Up");
        }
        if (Input.GetAxis("Vertical") < 0f)
        {
            Debug.Log("Moving Down");
        }
        if (Input.GetButtonDown("0"))
        {
            Debug.Log("THIS IS THE 0");
        }
        if (Input.GetButtonDown("1"))
        {
            Debug.Log("THIS IS THE 1");
        }
        if (Input.GetButtonDown("2"))
        {
            Debug.Log("THIS IS THE 2");
        }
        if (Input.GetButtonDown("3"))
        {
            IsGrabbed = !IsGrabbed;
            Debug.Log("THIS IS THE 3");
            Debug.Log("is it grabbing?" + IsGrabbed);
        }
        if (Input.GetButtonDown("4"))
        {
            Debug.Log("THIS IS THE 4");
        }
        if (Input.GetButtonDown("5"))
        {
            Debug.Log("THIS IS THE 5");
        }
        if (Input.GetButtonDown("6"))
        {
            Debug.Log("THIS IS THE 6");
        }
        if (Input.GetButtonDown("7"))
        {
            Debug.Log("THIS IS THE 7");
        }
        if (Input.GetButtonDown("8"))
        {
            Debug.Log("THIS IS THE 8");
        }
        if (Input.GetButtonDown("9"))
        {
            Debug.Log("THIS IS THE 9");
        }
        if (Input.GetButtonDown("10"))
        {
            Debug.Log("THIS IS THE 10");
        }
        if (Input.GetButtonDown("11"))
        {
            Debug.Log("THIS IS THE 11");
        }
        if (Input.GetButtonDown("12"))
        {
            Debug.Log("THIS IS THE 12");
        }
        if (Input.GetButtonDown("13"))
        {
            Debug.Log("THIS IS THE 13");
        }
        
    }
}
