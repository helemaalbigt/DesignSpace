using UnityEngine;
using System.Collections;

public class Tracked_DistanceRatioCompare : MonoBehaviour, Conditions.IConditionOwner
{

    public Condition condition = new Condition();
    public BetweenRange betweenCondition;
    public BasicUnityTrackingPoint fromTracked, toTracked;
    public BasicUnityTrackingPoint fromRefTracked, toRefTracked;
    public TrackedPoint from, to, fromRef, toRef;
    public bool useLocal;
    public enum Ignore { None, X, Y, Z }
    public Ignore ignore;

    public double lastRatioComputed;

    void Start()
    {

        if (from == null)
            from = fromTracked.TrackedPoint;
        if (to == null)
            to = toTracked.TrackedPoint;
        if (fromRef == null)
            fromRef = fromRefTracked.TrackedPoint;
        if (toRef == null)
            toRef = toRefTracked.TrackedPoint;
        condition.SetConditionAccess(IsInRangeWanted);


    }

    void OnDestroy()
    {
        condition.SetConditionAccess(null);
    }

    public double GetValue()
    {
        if (from == null || to == null  || toRef == null  || fromRef == null  ) return 0f;
        Vector3 a = useLocal ? from.LocalPosition : from.Position;
        Vector3 b = useLocal ? to.LocalPosition : to.Position;
        Vector3 c = useLocal ? fromRef.LocalPosition : fromRef.Position;
        Vector3 d = useLocal ? toRef.LocalPosition : toRef.Position;
        IgnoreAxis(ref a);
        IgnoreAxis(ref b);
        IgnoreAxis(ref c);
        IgnoreAxis(ref d);

        double dist = Vector3.Distance(a,b);
        double refDist = Vector3.Distance(c, d);
        if (refDist == 0f) return dist==0f?0f: (dist<0f?-1f:1f)*double.MaxValue;
        return dist/refDist;
    }

    public bool IsInRangeWanted()
    {
        if (betweenCondition == null) return false;
        return betweenCondition.IsInRange(lastRatioComputed = GetValue());
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