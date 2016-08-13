using UnityEngine;
using System.Collections;

/// <summary>
/// This classe is not finish.
/// The idee was that the user give à direction in Vector3
/// and specify how many degree of precision he want
/// But Angle return 0-180 and I wanted -180 to 180
/// TODO fin a way to do my own angle
/// </summary>
public class Tracked_DirectionAngleCondition : MonoBehaviour, Conditions.IConditionOwner
{

    public Vector3 directionWanted = Vector3.forward;
    public Condition condition = new Condition();
    public BetweenRange betweenAngleCondition = new BetweenRange(-5,5);
    public BasicUnityTrackingPoint directionTracked;
    public TrackedPoint direction;
    public bool useLocal = true ;
    
    public enum Ignore { None, X, Y, Z }
    public Ignore ignore;

    public double lastDistanceComputed;

    void Start()
    {

        if (direction == null)
            direction = directionTracked.TrackedPoint;        
        condition.SetConditionAccess(IsInRangeWanted);
        directionWanted.Normalize();    
    }

    void OnDestroy()
    {
        condition.SetConditionAccess(null);
    }

    public double GetValue()
    {
        if (direction == null ) throw new System.ArgumentNullException();
        Vector3 d = useLocal ? direction.LocalPosition : direction.Position;
        Vector3 w = directionWanted;
        if (w == Vector3.zero)
            Debug.Log("Direction Wanted should not be zero");
        if (d == Vector3.zero)
            Debug.Log("Direction should not be zero");
        
        //w.Normalize();
        //d.Normalize();
        IgnoreAxis(ref d);
        IgnoreAxis(ref w);

        Quaternion angleWanted = Quaternion.FromToRotation(w, d);
        Debug.DrawLine(d, w,Color.green);
        if (w == Vector3.zero || d == Vector3.zero)
            Debug.Log("Warning you can't compute angle if it is equal at Zero",this.gameObject);
        return Quaternion.Angle(Quaternion.Euler(0,1,0),angleWanted);
    }

    public bool IsInRangeWanted()
    {
        if (betweenAngleCondition == null) return false;
        return betweenAngleCondition.IsInRange(lastDistanceComputed = GetValue());
    }

    public Conditions.ICondition GetCondition()
    {
        return condition;
    }

    public void IgnoreAxis(ref Vector3 value)
    {
        switch (this.ignore)
        {
            case Ignore.X: value.x = 0; break;
            case Ignore.Y: value.y = 0; break;
            case Ignore.Z: value.z = 0; break;
        }
    }

}
