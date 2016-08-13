using UnityEngine;
using System.Collections;

public class TrackedRootedPoint: BasicUnityTrackingPoint
{

    public Transform rootPosition;
    public Transform rootRotation;
    public Transform destination;


    public override void DefineAccessorDelegate()
    {
        TrackedPoint.accessPosition = GetPosition;
        TrackedPoint.accessLocalPosition = GetLocalPosition;
        TrackedPoint.accessRotation = GetRotation;
        TrackedPoint.accessLocalRotation = GetLocalRotation;
        TrackedPoint.OptinalLinkedTransform = this.transform;

    }

    private void RecenterPoint( Transform rootPosition, Transform rootRotation ,ref Vector3 pos, ref Quaternion rot)
    {
      
            Vector3 initPos = pos;
            Quaternion r = rootRotation==null?Quaternion.identity : Quaternion.Inverse(rootRotation.rotation);
            pos = pos - (rootPosition==null?Vector3.zero : rootPosition.position);
            pos = r * pos;
            rot = rot * r;
       
        
    }
    private Quaternion GetLocalRotation()
    {
        Vector3 position = destination.position;
        Quaternion rotation = destination.rotation;
        RecenterPoint(rootPosition,rootRotation, ref position, ref rotation);
        return rotation;
    }

    private Quaternion GetRotation()
    {
        return destination.rotation;
    }

    private Vector3 GetLocalPosition()
    {
        Vector3 position = destination.position;
        Quaternion rotation = destination.rotation;
        RecenterPoint(rootPosition, rootRotation, ref position, ref rotation);
        return position;
    }

    private Vector3 GetPosition()
    {
        return destination.position;
    }

	
}
