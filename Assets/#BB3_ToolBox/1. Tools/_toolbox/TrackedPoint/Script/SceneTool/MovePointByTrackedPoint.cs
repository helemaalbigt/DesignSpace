using UnityEngine;
using System.Collections;

public class MovePointByTrackedPoint : MonoBehaviour {

    public BasicUnityTrackingPoint trackedPoint;
    public TrackedPoint tp;
    public bool useLocal=true;
    public bool setLocal=false;

	void Start () {

        if (trackedPoint == null)
            trackedPoint = GetComponent<BasicUnityTrackingPoint>() as BasicUnityTrackingPoint;
        if (trackedPoint != null && trackedPoint.TrackedPoint!=null)
        tp = trackedPoint.TrackedPoint ;
        if (tp == null)
            Destroy(this);
	}
	
	void Update () {
        if (tp != null)
        {
            if (setLocal)
                this.transform.localPosition = useLocal ? tp.LocalPosition : tp.Position;
            else
                this.transform.position = useLocal ? tp.Position : tp.Position;

            if (setLocal)
                this.transform.localRotation = useLocal ? tp.LocalRotation : tp.Rotation;
            else
                this.transform.rotation = useLocal ? tp.LocalRotation : tp.Rotation;
        }
	}
}
