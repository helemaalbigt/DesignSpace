using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PointToPointMeasure : MonoBehaviour {

    public float ratioMeterPerUnit = 1; 
    public Transform otherPoint;

	void Update () {

       
		Display ();
   

	}

    public float GetDistance() { if (otherPoint == null) return 0; return Vector3.Distance(transform.position, otherPoint.position); }
    public float ApplyRatio(float value) { return value * ratioMeterPerUnit; }

	public void Display(){
		DrawCross(transform, 0.15f);    
		if (otherPoint != null)
		{
			DrawLine();
			DrawCross(otherPoint, 0.05f);
		}
	}

    public void DrawLine() 
    {
        if(otherPoint!=null)
           Debug.DrawLine(transform.position, otherPoint.position);
    }
    public void DrawCross(Transform point, float distance) 
    {
        if (point != null)
        {
            Vector3 ptOne, ptTwo;

            ptOne = point.position;
            ptOne.x -= distance;
            ptTwo = point.position;
            ptTwo.x += distance;
            Debug.DrawLine(ptOne, ptTwo, Color.magenta);

            ptOne = point.position;
            ptOne.y -= distance;
            ptTwo = point.position;
            ptTwo.y += distance;
            Debug.DrawLine(ptOne, ptTwo, Color.magenta);


            ptOne = point.position;
            ptOne.z -= distance;
            ptTwo = point.position;
            ptTwo.z += distance;
            Debug.DrawLine(ptOne, ptTwo, Color.magenta);
        }
    }
}
