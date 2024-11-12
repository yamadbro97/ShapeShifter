using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;

public class SolvedPrototype : MonoBehaviour
{
    public Vector2Int[] Solution_Position;
    public SpriteShapeController Spline;
    public int SolvedCount;
    public GameObject WinCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnSolve();
    }
    public void OnSolve()
    {
        if (Input.GetMouseButtonUp(0))
        {
            int counter = 0;
            for (int i = 0; i < Spline.spline.GetPointCount(); i++)
            {
                if (Spline.spline.GetPosition(i).x == Solution_Position[i].x && Spline.spline.GetPosition(i).y == Solution_Position[i].y)
                {
                    Debug.Log(i + "IS IN PLACE");
                    counter++;
                }
                else
                {
                    Debug.Log(i + " ISNT IN PLACE");
                    Debug.Log(Spline.spline.GetPosition(i).x == Solution_Position[i].x);
                    Debug.Log(Spline.spline.GetPosition(i).y == Solution_Position[i].y);
                    Debug.Log(Spline.spline.GetPosition(i).x);
                    Debug.Log(Spline.spline.GetPosition(i).y);
                }
            }
            if(counter == SolvedCount) 
            {
                WinCanvas.SetActive(true);
            }
        }
    }
}
