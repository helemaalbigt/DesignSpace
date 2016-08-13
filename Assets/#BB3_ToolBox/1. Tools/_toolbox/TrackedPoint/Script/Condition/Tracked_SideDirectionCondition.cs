using UnityEngine;
using System.Collections;

public class Tracked_SideDirectionCondition : MonoBehaviour, Conditions.IConditionOwner
{
    public BasicUnityTrackingPoint pointTracked;
    public Condition condition = new Condition();
    public BetweenRange leftRightBetween = new BetweenRange() { rangeA = -float.MaxValue, rangeB = float.MaxValue };
    public BetweenRange downTopBetween = new BetweenRange() { rangeA = -float.MaxValue, rangeB = float.MaxValue };
    public BetweenRange backFrontBetween = new BetweenRange() { rangeA = -float.MaxValue, rangeB = float.MaxValue };
   
    public TrackedPoint point;
    public bool useLocal = true;
    public bool mustAllBeValide = true;
    public string lastValue;
    public bool isInHorizontalRange = false, isInVerticalRange = false, isInFrontRange = false;

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



    public bool IsInRangeWanted()
    {
        if (point == null) throw new System.ArgumentNullException();
        Vector3 position = useLocal ? point.LocalPosition : point.Position;
        isInHorizontalRange = false; isInVerticalRange = false; isInFrontRange = false;
        
        isInHorizontalRange = leftRightBetween.IsInRange((double)position.x);
        isInVerticalRange = downTopBetween.IsInRange((double)position.y);
        isInFrontRange = backFrontBetween.IsInRange((double)position.z);

        lastValue = "" + position + "  ---   Horizontal: " + isInHorizontalRange + " Vertical: " + isInVerticalRange + " Front: " + isInFrontRange;

        if(mustAllBeValide)
            return isInHorizontalRange && isInVerticalRange && isInFrontRange;
        else
            return isInHorizontalRange || isInVerticalRange || isInFrontRange;
        
    }

    public Conditions.ICondition GetCondition()
    {
        return condition;
    }

    
}
