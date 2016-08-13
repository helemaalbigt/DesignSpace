using UnityEngine;
using System.Collections;

public class HeadRotationLimit : MonoBehaviour {

    public Quaternion localRotation;
    private Quaternion lastQuaternion;
    public Transform Head;

    public float maxHorizontalDegree = 80f;
    public float maxVerticalDegree = 80f;
    public float maxRollingDegree = 30f;
    public float rotationSpeed=30f;

    public void SetHeadOrientation(Quaternion localRotation )
    {
        Vector3 eulerValue = localRotation.eulerAngles;

        EuleurClamp(ref eulerValue.x, maxVerticalDegree);
        EuleurClamp(ref eulerValue.y, maxHorizontalDegree);
        EuleurClamp(ref eulerValue.z, maxRollingDegree);
        this.localRotation = Quaternion.Euler(eulerValue);
        
    }

    private void EuleurClamp(ref float value, float limiteRange)
    {
        if (value > 180f)
        {
            if (value <= 360f - limiteRange)
                value = 360f - limiteRange;
        }
        else {
            if (value >= limiteRange)
                value = limiteRange;
        } 
    }
    public void Update() {
        if ( Head == null ) return;
        if (Head.localRotation != this.localRotation)
            Head.localRotation = this.localRotation; //Quaternion.RotateTowards(headToMove.localRotation, this.localRotation, Time.deltaTime * rotationSpeed);

    }
	

}
