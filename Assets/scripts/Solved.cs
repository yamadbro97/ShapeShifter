using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Solved : MonoBehaviour
{
    public List<Range> Ranges;
    public List<SymmAngles> SymAngles;
    public SpriteShapeController Splines;
    public GameObject WinCanvas;
    //chaange Angles to private after determining all ranges
    public float[] Angles;


    // Start is called before the first frame update
    void Start()
    {
        Angles = new float[Splines.spline.GetPointCount()];
    }

    // Update is called once per frame
    void Update()
    {
        //OnSolve();
        if (Input.GetMouseButtonUp(0))
        { 
            IsSolved();
        }
    }
   public float CalcAngle(int i)
    {
        
            if (i == 0)
            {
              return GetAngle(Splines.spline.GetPosition(Splines.spline.GetPointCount() - 1), Splines.spline.GetPosition(i), Splines.spline.GetPosition(i + 1));

            }
            else if (i == Splines.spline.GetPointCount() - 1)
            {
               return GetAngle(Splines.spline.GetPosition(i - 1), Splines.spline.GetPosition(i), Splines.spline.GetPosition(0));

            }
            else
            {
               return GetAngle(Splines.spline.GetPosition(i - 1), Splines.spline.GetPosition(i), Splines.spline.GetPosition(i + 1));

            }
        
        
    }
    public  float GetAngle(Vector2 position1, Vector2 position2, Vector2 position3)
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
        //Debug.Log(angleDeg);
        return angleDeg;
        
    }
    public void IsSolved()
    {
        int counter = 0;
        int symcount = 0;
        for (int i = 0; i < Splines.spline.GetPointCount(); i++)
      {
            // save all Angles into an array
            Angles[i] = CalcAngle(i);
            //Debug.Log(Angles[i]);
      }
      for(int i = 0;i < Splines.spline.GetPointCount();i++)
        {
            if(Ranges[i].min <= Angles[i] && Angles[i] <= Ranges[i].max)
            {
                // Counter checks if all edges are in the correct Range
                counter++;
                //Debug.Log("correct edges:" + counter);
            }
        }
        for (int i = 0; i < SymAngles.Count; i++)
        {
            if (Angles[SymAngles[i].Angle_1] == Angles[SymAngles[i].Angle_2])
            {
                // counts how many Angles have the same Value
                symcount++;
                //Debug.Log("similar edges:" + symcount);

            }
        }
      if(counter == Splines.spline.GetPointCount() && symcount == SymAngles.Count)
        {
            WinCanvas.SetActive(true);
        }


    }
    [System.Serializable]
    public struct Range
    {
        public float min;
        public float max;
    }
    [System.Serializable]
    public struct SymmAngles
    {
        public int Angle_1;
        public int Angle_2;
    }
}
