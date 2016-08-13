using UnityEngine;
using System.Collections;

public class Tracked_AngleCondition_SinglePosition : MonoBehaviour, Conditions.IConditionOwner
{

    public Condition condition = new Condition();
    public BetweenRange betweenCondition;
    public BasicUnityTrackingPoint pointTracked;
    public TrackedPoint point;
    public bool useLocal;
    public enum Ignore { None, X, Y, Z }
    public Ignore ignore;

    public double lastAngleComputed;

    void Start()
    {

        if (point == null)
            point = pointTracked.TrackedPoint;
       
        condition.SetConditionAccess(IsInRangeWanted);


    }

    void OnDestroy()
    {
        condition.SetConditionAccess(null);
    }

    public float GetValue()
    {
        if (point == null ) return 0f;
        Vector3 pt = useLocal ? point.LocalPosition : point.Position;
        
        IgnoreAxis(ref pt);
        
        return Vector3.Angle(Vector3.forward, pt);
    }

    public bool IsInRangeWanted()
    {
        if (betweenCondition == null) return false;
        return betweenCondition.IsInRange(lastAngleComputed = GetValue());
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
