using UnityEngine;
using System.Collections;

public class TrP_Test : MonoBehaviour {

    public bool withDebug;
	// Use this for initialization
    void Awake()
    {
        TrackedPoint.onTrackedPointAdd += DisplayTrackedPointKey;
        TrackedPoint.onTrackedPointRemove += DisplayTrackedPointKey;
	}
    private TrackedPoint lastTrackedPoint;
    private void DisplayTrackedPointKey(string idName, TrackedPoint trackedPoint)
    {
        if (!withDebug) return;
        if (lastTrackedPoint != null)
            Debug.DrawLine(lastTrackedPoint.Position, trackedPoint.Position,Color.green,10f);
        lastTrackedPoint = trackedPoint;

        

        string keys = "";
        foreach(string k in TrackedPoint.GetTrackedKeys())
            keys+=" "+k;
        Debug.Log(keys);
    }
	
}
