using UnityEngine;
using System.Collections;

public class Tracked_AngleCondition_Position : MonoBehaviour , Conditions.IConditionOwner{

    public Condition condition = new Condition();
    public BetweenRange betweenCondition;
    public BasicUnityTrackingPoint firstTracked, pivotTracked, secondTracked;
    public TrackedPoint first, pivot, second;
    public bool useLocal;
    public enum Ignore {None,X,Y,Z }
    public Ignore ignore;

    public double lastAngleComputed;

    void Start() {

        if (first == null)
            first = firstTracked.TrackedPoint;
        if (pivot == null)
            pivot = pivotTracked.TrackedPoint;
        if (second == null)
            second = secondTracked.TrackedPoint;
    
        condition.SetConditionAccess(IsInRangeWanted);
    
        
    }

    void OnDestroy() 
    {
        condition.SetConditionAccess(null);    
    }

    public float GetValue() {
        if (first == null || second == null) return 0f;
        Vector3 c = pivot == null ? Vector3.zero : (useLocal ? pivot.LocalPosition : pivot.Position);
        Vector3 a = useLocal ? first.LocalPosition : first.Position;
        Vector3 b = useLocal ? second.LocalPosition : second.Position;

        IgnoreAxis(ref c);
        IgnoreAxis(ref a);
        IgnoreAxis(ref b);
        a -= c;
        b -= c;

        return Vector3.Angle(a, b); 
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

    public void IgnoreAxis(ref Vector3 value) {
        switch (this.ignore)
        {
            case Ignore.X: value.x = 0; break;
            case Ignore.Y: value.y = 0; break;
            case Ignore.Z: value.z = 0; break;
        }
    }

}
