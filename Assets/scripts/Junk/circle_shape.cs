using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle_shape : MonoBehaviour
{
    Mesh mesh;
    public Vector3[] circlePoints;
    public int[] circleTriangles;

    public bool isFilled;
    public int circleSides;
    public float circleRadius;
    public float centerRadius;

    private Camera mainCamera;
    private int selectedPointIndex = -1;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isFilled)
        {
            DrawFilled(circleSides, circleRadius);
        }
        else
        {
            Drawhollow(circleSides, circleRadius, centerRadius);
        }

        HandleInput();
    }

    void DrawFilled(int sides, float radius)
    {
        circlePoints = GetCircumferencePoints(sides, radius).ToArray();
        circleTriangles = DrawfilledTriangles(circlePoints);
        mesh.Clear();
        mesh.vertices = circlePoints;
        mesh.triangles = circleTriangles;
    }

    void Drawhollow(int sides, float outerRadius, float innerRadius)
    {
        List<Vector3> pointsList = new List<Vector3>();
        List<Vector3> outerPoints = GetCircumferencePoints(sides, outerRadius);
        pointsList.AddRange(outerPoints);
        List<Vector3> innerPoints = GetCircumferencePoints(sides, innerRadius);
        pointsList.AddRange(innerPoints);

        circlePoints = pointsList.ToArray();

        circleTriangles = DrawHollowTriangles(circlePoints);
        mesh.Clear();
        mesh.vertices = circlePoints;
        mesh.triangles = circleTriangles;
    }

    int[] DrawHollowTriangles(Vector3[] points)
    {
        int sides = points.Length / 2;
        int triangleAmount = sides * 2;

        List<int> newTriangles = new List<int>();
        for (int i = 0; i < sides; i++)
        {
            int outerIndex = i;
            int innerIndex = i + sides;

            newTriangles.Add(outerIndex);
            newTriangles.Add(innerIndex);
            newTriangles.Add((i + 1) % sides);

            newTriangles.Add(outerIndex);
            newTriangles.Add(sides + ((sides + i - 1) % sides));
            newTriangles.Add(outerIndex + sides);
        }
        return newTriangles.ToArray();
    }

    List<Vector3> GetCircumferencePoints(int sides, float radius)
    {
        List<Vector3> points = new List<Vector3>();
        float circumferenceProgressPerStep = (float)1 / sides;
        float TAU = 2 * Mathf.PI;
        float radianProgressPerStep = circumferenceProgressPerStep * TAU;

        for (int i = 0; i < sides; i++)
        {
            float currentRadian = radianProgressPerStep * i;
            points.Add(new Vector3(Mathf.Cos(currentRadian) * radius, Mathf.Sin(currentRadian) * radius, 0));
        }
        return points;
    }

    int[] DrawfilledTriangles(Vector3[] points)
    {
        int triangleAmount = points.Length - 2;
        List<int> newTriangles = new List<int>();
        for (int i = 0; i < triangleAmount; i++)
        {
            newTriangles.Add(0);
            newTriangles.Add(i + 2);
            newTriangles.Add(i + 1);
        }
        return newTriangles.ToArray();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            selectedPointIndex = GetClosestPointIndex(mousePosition);
        }

        if (Input.GetMouseButton(0) && selectedPointIndex != -1)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            circlePoints[selectedPointIndex] = mousePosition;
            mesh.vertices = circlePoints;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedPointIndex = -1;
        }
    }

    int GetClosestPointIndex(Vector3 position)
    {
        float minDistance = float.MaxValue;
        int closestPointIndex = -1;

        for (int i = 0; i < circlePoints.Length; i++)
        {
            float distance = Vector3.Distance(position, circlePoints[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPointIndex = i;
            }
        }

        return closestPointIndex;
    }
}
