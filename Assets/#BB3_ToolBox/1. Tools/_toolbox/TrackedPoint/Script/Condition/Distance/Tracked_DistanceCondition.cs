using UnityEngine;
using System.Collections;

public class Tracked_DistanceCondition : MonoBehaviour, Conditions.IConditionOwner
{

    public Condition condition = new Condition();
    public BetweenRange betweenCondition;
    public BasicUnityTrackingPoint fromTracked, toTracked;
    public TrackedPoint from, to;
    public bool useLocal;
    public enum Ignore { None, X, Y, Z }
    public Ignore ignoreFirstAxe;
    public Ignore ignoreSecondAxe;

    public double lastDistanceComputed;

    void Start()
    {

        if (from == null)
            from = fromTracked.TrackedPoint;
        if (to == null)
            to = toTracked.TrackedPoint;
        condition.SetConditionAccess(IsInRangeWanted);


    }

    void OnDestroy()
    {
        condition.SetConditionAccess(null);
    }

    public double GetValue()
    {
        if (from == null || to == null) throw new System.ArgumentNullException();
        Vector3 a = useLocal ? from.LocalPosition : from.Position;
        Vector3 b = useLocal ? to.LocalPosition : to.Position;
        IgnoreAxis(ref a);
        IgnoreAxis(ref b);
        return Vector3.Distance(a, b);
    }

    public bool IsInRangeWanted()
    {
        if (betweenCondition == null) return false;
        return betweenCondition.IsInRange(lastDistanceComputed = GetValue());
    }

    public Conditions.ICondition GetCondition()
    {
        return condition;
    }

    public void IgnoreAxis(ref Vector3 value)
    {
        switch (this.ignoreFirstAxe)
        {
            case Ignore.X: value.x = 0; break;
            case Ignore.Y: value.y = 0; break;
            case Ignore.Z: value.z = 0; break;
        }
        switch (this.ignoreSecondAxe)
        {
            case Ignore.X: value.x = 0; break;
            case Ignore.Y: value.y = 0; break;
            case Ignore.Z: value.z = 0; break;
        }
    }

}
