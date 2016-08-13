using UnityEngine;
using System.Collections;

public class VirtualControllerToLocalRotation : MonoBehaviour {

    public VirtualController virtualController;
    public int orientationIndex = 0;
    public Transform objectToRotate;
	
	void Update () {
        if (virtualController == null) return;
        transform.localRotation = virtualController.GetOrientationValue(orientationIndex);
       
	
	}

    void Reset() 
    {
        if (objectToRotate == null)
            objectToRotate = transform;

    }


}
