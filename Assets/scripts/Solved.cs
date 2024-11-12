using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;

public class Solved : MonoBehaviour
{

     public int[] [] Solution_Angles;
    public SpriteShapeController Splines;
    //public int SolvedCount;
    public GameObject WinCanvas;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //OnSolve();
        if (Input.GetMouseButtonUp(0))
        {
            CalcAngle();
        }
    }
   public void CalcAngle()
    {
        for( int i = 0; i < Splines.spline.GetPointCount(); i++ ) 
        {
            if(i==0)
            {
                CalculateAngle(Splines.spline.GetPosition(Splines.spline.GetPointCount()-1), Splines.spline.GetPosition(i), Splines.spline.GetPosition(i+1));
            }
            else if(i== Splines.spline.GetPointCount() - 1)
            {
                CalculateAngle(Splines.spline.GetPosition(i-1), Splines.spline.GetPosition(i), Splines.spline.GetPosition(0));

            }
            else
            {
                CalculateAngle(Splines.spline.GetPosition(i-1), Splines.spline.GetPosition(i), Splines.spline.GetPosition(i+1));

            }
            /*Rtangent = Splines.spline.GetRightTangent(i);
            Ltangent = Splines.spline.GetLeftTangent(i);
            Debug.Log(Rtangent);
            Debug.Log(Ltangent);*/
        }
    }
    public static void CalculateAngle(Vector2 position1, Vector2 position2, Vector2 position3)
    {
        // Calculate direction vectors
        Vector2 dir1 = position2 - position1;
        Vector2 dir2 = position2 - position3;

        // Calculate the magnitudes of the direction vectors
        float mag1 = dir1.magnitude;
        float mag2 = dir2.magnitude;

        // Calculate the dot product
        float dotProduct = Vector2.Dot(dir1, dir2);

        // Calculate the angle in radians
        float angleRad = Mathf.Acos(dotProduct / (mag1 * mag2));

        // Convert radians to degrees
        float angleDeg = angleRad * Mathf.Rad2Deg;

        Debug.Log("Circle"+position2+":"+ angleDeg);
    }
}
