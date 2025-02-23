using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Diagnostics.Tracing;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;

public class SplineController2 : MonoBehaviour
{

    public SpriteShapeController spriteShapeController;
    [SerializeField] 
    public GameObject[] pointPrefabs;
    public static Color defaultColor = Color.red;
    public static Color highlightColor = Color.green;
    public float defaultSize = 0.4f;
    public float highlightSize = 0.4f;
    public int move_counter;
       
    // Controller Variables
    private bool IsNavigating = false;
    private bool IsMoving = false;
    public static  bool IsSelected = false;
    private bool secretbool = false;
    private int currentIndex =0;
    public static Color SelectedColor = Color.blue;
    public float MoveSpeed = 10f;



    public Spline spline;
    public int selectedPointIndex = -1;

    private List<GameObject> pointIndicators = new List<GameObject>();

    void Start()
    {
        spline = spriteShapeController.spline;
        CreatePointIndicators();
        move_counter = 0;
        if (!SceneLoader2.IsTouch)
        {
            EventSystem.current.SetSelectedGameObject(pointPrefabs[currentIndex].gameObject);
            UpdateSelection();
        }
    }

    void Update()
    {
        if (SceneLoader2.IsTouch)
        {
            TouchHandleInput();
        }
        if (!SceneLoader2.IsTouch)
        {
            ControllerHandleInput();
        }

    }

    void TouchHandleInput()
    {
        if (IsPointerOverUIElement())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);

            if (colliders.Length > 0)
            {
                Collider2D closestCollider = null;
                float closestDistance = float.MaxValue;

                foreach (Collider2D col in colliders)
                {
                    if (!col.gameObject.CompareTag("SnapPosition"))
                    {
                        // Calculate distance from mouse position to the collider's transform position
                        float distance = Vector2.Distance(mousePosition, col.transform.position);

                        // Find the closest collider
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestCollider = col;
                        }
                    }  
                }

                if (closestCollider != null)
                {
                    selectedPointIndex = GetClosestPointIndex(mousePosition);
                    Debug.Log("Clicked on the closest GameObject: " + closestCollider.gameObject.name);
                }
            }



            if (selectedPointIndex != -1)
            {
                HighlightPoint(selectedPointIndex, 1);
            }
        }

        if (Input.GetMouseButton(0) && selectedPointIndex != -1)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            spline.SetPosition(selectedPointIndex, mousePosition);
            spriteShapeController.BakeMesh();
            UpdatePointIndicator(selectedPointIndex, mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedPointIndex != -1)
            {
                HighlightPoint(selectedPointIndex, 2);

            }
            selectedPointIndex = -1;


        }
    }

    void ControllerHandleInput()
    {
        if(Input.GetButtonDown("5"))
        {
            if (GameObject.Find("GiveUp_Button"))
            {
                GameObject.Find("GiveUp_Button").GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
        }
        if(Input.GetButtonDown("7"))
        {
            //Enlarge_Image.onClick.Invoke();
            if (GameObject.Find("Enlarge_Button") && !secretbool)
            {
                GameObject.Find("Enlarge_Button").GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
            if(GameObject.Find("Image_Canvas") && secretbool)
            {
                GameObject.Find("Image_Canvas").SetActive(false);
            }
            if (GameObject.Find("Touch"))
            {
                GameObject.Find("Touch").GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
            if(GameObject.Find("Next_level_Button"))
            {
                GameObject.Find("Next_level_Button").GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
            if (GameObject.Find("Yes"))
            {
                GameObject.Find("Yes").GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
            secretbool = !secretbool;
        }
        if (Input.GetButtonDown("6"))
        {
            if (GameObject.Find("Start"))
            {
                GameObject.Find("Start").GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
            if (GameObject.Find("Controller"))
            {
                GameObject.Find("Controller").GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
            if (GameObject.Find("Restart_Button"))
            {
                GameObject.Find("Restart_Button").GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
            if (GameObject.Find("No"))
            {
                GameObject.Find("No").GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
        }
        if (Input.GetButtonDown("1"))
        {
            IsSelected = !IsSelected;
            Debug.Log("IsSelected is:" + IsSelected);
            if (IsSelected) HighlightPoint(currentIndex, 1);
            else HighlightPoint(currentIndex, 3);
        }
        if (!IsSelected)
        {
            if (Input.GetAxis("Vertical") < -0.5f && !IsNavigating)
            {
                // Get Previous Spline ClockWise
                HighlightPoint(currentIndex, 2);
                currentIndex = ((currentIndex - 1 + pointPrefabs.Length) % pointPrefabs.Length);
                UpdateSelection();
            }
            else if (Input.GetAxis("Vertical") > 0.5f && !IsNavigating)
            {
                // Get Next Spline clockwise
                HighlightPoint(currentIndex, 2);
                currentIndex = (currentIndex + 1) % pointPrefabs.Length;
                UpdateSelection();
            }
            if (Input.GetAxis("Horizontal") > 0.5f && !IsNavigating) Debug.Log(" trying to move Right?");
            if (Input.GetAxis("Horizontal") < -0.5f && !IsNavigating) Debug.Log(" trying to move Left?");

            // Reset isNavigating when joystick is released
            if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.5f)
            {
                IsNavigating = false;
            }
        }
        
        if (IsSelected)
            {
            // If the joystick is outside the dead zone
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.5f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.5f)
            {
                Vector3 currentPosition = pointPrefabs[currentIndex].transform.position;
                currentPosition.z = 0;

                // Move vertically
                currentPosition.y += Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;

                // Move horizontally
                currentPosition.x += Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

                // Update the spline position and bake the mesh
                spline.SetPosition(currentIndex, currentPosition);
                spriteShapeController.BakeMesh();

                // Update the point indicator
                UpdatePointIndicator(currentIndex, currentPosition);

                IsMoving = true;
            }
            else
            {
                // Stop movement when the joystick is in the dead zone
                IsMoving = false;
            }
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

    
    void CreatePointIndicators()
    {
        // make it so that you can define the pointindicators manually as object kids?
        foreach (GameObject point in pointIndicators)
        {
            Destroy(point);
        }
        pointIndicators.Clear();
        for (int i = 0; i < spline.GetPointCount(); i++)
        {
            
            if (pointPrefabs[i] != null)
            {
                GameObject pointIndicator = pointPrefabs[i];
               Vector3 position = spline.GetPosition(i);
               pointIndicator.transform.position = position;
                pointIndicator.GetComponent<Renderer>().material.color = defaultColor;
                pointIndicator.transform.localScale = Vector3.one * defaultSize;
                pointIndicator.name = "Circle_" + i;
                pointIndicators.Add(pointIndicator);
                
            }
            else
            {
                Debug.Log("Amount of Pointprefabs is less than the spline count");
            }
        }
        
    }

    public void UpdatePointIndicator(int index, Vector3 position)
    {
        if (index < 0 || index >= pointIndicators.Count) return;

        pointIndicators[index].transform.position = position;
    }

    private void UpdateSelection()
    {
        // Update the selected button
        EventSystem.current.SetSelectedGameObject(pointPrefabs[currentIndex].gameObject);
        HighlightPoint(currentIndex, 3);
        IsNavigating = true;

        IsSelected = false;
    }

    void HighlightPoint(int index, int highlight)
    {
        index = index % pointPrefabs.Length;
        Debug.Log(index);
        Debug.Log(pointIndicators.Count);
        GameObject pointIndicator = pointIndicators[index];
        if (highlight==1)
        {
            pointIndicator.GetComponent<Renderer>().material.color = highlightColor;
            pointIndicator.transform.localScale = Vector3.one * highlightSize;
        }
        if(highlight==2)
        {
            pointIndicator.GetComponent<Renderer>().material.color = defaultColor;
            pointIndicator.transform.localScale = Vector3.one * defaultSize;
        }
        if(highlight == 3) 
        {
            pointIndicator.GetComponent<Renderer>().material.color = SelectedColor;
            pointIndicator.transform.localScale = Vector3.one * defaultSize;
        }
    }
    bool IsPointerOverUIElement()
    {
        // Check if the pointer is over a UI element
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }




}
