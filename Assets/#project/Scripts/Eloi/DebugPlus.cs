using UnityEngine;
using System.Collections;

public class DebugPlus  {

 
    static public void DrawGizmoCrossAt(Vector3 point, float lineSize, Color color, float time)
    {
        Vector3 from, to;
        float segDistance = lineSize/2f;

        from = point;
        to = point;
        from.x += -segDistance;
        to.x += segDistance;
        Debug.DrawLine(from, to, color, time);

        from = point;
        to = point;
        from.y += -segDistance;
        to.y += segDistance;
        Debug.DrawLine(from, to, color, time);

        from = point;
        to = point;
        from.z += -segDistance;
        to.z += segDistance;
        Debug.DrawLine(from, to, color, time);


    }
    





}
