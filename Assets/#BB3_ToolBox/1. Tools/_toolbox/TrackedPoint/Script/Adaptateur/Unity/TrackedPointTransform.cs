using UnityEngine;
using System.Collections;

public class TrackedPointTransform : BasicUnityTrackingPoint {


    public override void DefineAccessorDelegate()
    {
        TrackedPoint.accessPosition = GetPosition;
        TrackedPoint.accessLocalPosition = GetLocalPosition;
        TrackedPoint.accessRotation = GetRotation;
        TrackedPoint.accessLocalRotation = GetLocalRotation;
        TrackedPoint.OptinalLinkedTransform = this.transform;

    }

    private Quaternion GetLocalRotation()
    {
        return transform.localRotation;
    }

    private Quaternion GetRotation()
    {
        return transform.rotation;
    }

    private Vector3 GetLocalPosition()
    {
        return transform.localPosition;
    }

    private Vector3 GetPosition()
    {
        return transform.position;
    }

	
}
