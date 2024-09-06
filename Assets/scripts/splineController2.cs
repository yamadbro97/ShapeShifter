using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SplineController2 : MonoBehaviour
{

    public SpriteShapeController spriteShapeController;
   [SerializeField] public GameObject[] pointPrefabs;
    public Color defaultColor = Color.white;
    public Color highlightColor = Color.red;
    public float defaultSize = 0.1f;
    public float highlightSize = 0.2f;



    public Spline spline;
    public int selectedPointIndex = -1;

    private List<GameObject> pointIndicators = new List<GameObject>();

    void Start()
    {
        spline = spriteShapeController.spline;
        CreatePointIndicators();
        Debug.Log(pointPrefabs.Length);
        
    }

    void Update()
    {

        HandleInput();


    }

    void HandleInput()
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
                    // Calculate distance from mouse position to the collider's transform position
                    float distance = Vector2.Distance(mousePosition, col.transform.position);

                    // Find the closest collider
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestCollider = col;
                    }
                }

                if (closestCollider != null)
                {
                    selectedPointIndex = GetClosestPointIndex(mousePosition);
                    Debug.Log("Clicked on the closest GameObject: " + closestCollider.gameObject.name);
                    // For example:
                    // closestCollider.gameObject.SetActive(false);
                }
            }



            if (selectedPointIndex != -1)
            {
                HighlightPoint(selectedPointIndex, true);
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
            // Debug.Log(snaphere.circleCollider.isTrigger);
            /*if (snaphere.circleCollider.isTrigger)
             {
                 spline.SetPosition(selectedPointIndex, GameObject.FindWithTag("SnapPosition").transform.position);
                 spriteShapeController.BakeMesh();
                 UpdatePointIndicator(selectedPointIndex, GameObject.FindWithTag("SnapPosition").transform.position);
             } */

            if (selectedPointIndex != -1)
            {
                HighlightPoint(selectedPointIndex, false);


            }
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
                Vector3 position = spline.GetPosition(i);
                GameObject pointIndicator = Instantiate(pointPrefabs[i], position, Quaternion.identity, transform);
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
        ;
    }

    void HighlightPoint(int index, bool highlight)
    {
        GameObject pointIndicator = pointIndicators[index];
        if (highlight)
        {
            pointIndicator.GetComponent<Renderer>().material.color = highlightColor;
            pointIndicator.transform.localScale = Vector3.one * highlightSize;
        }
        else
        {
            pointIndicator.GetComponent<Renderer>().material.color = defaultColor;
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
