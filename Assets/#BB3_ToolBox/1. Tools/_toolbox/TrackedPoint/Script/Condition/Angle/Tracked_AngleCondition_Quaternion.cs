using UnityEngine;
using System.Collections;

public class Tracked_AngleCondition_Quaternion : MonoBehaviour , Conditions.IConditionOwner{

    public Condition condition = new Condition();
    public BetweenRange betweenCondition;
    public BasicUnityTrackingPoint firstTracked, secondTracked;
    public TrackedPoint first, second;
    public bool useLocal;
    public enum Ignore {None,X,Y,Z }
    public Ignore ignore;

    public double lastAngleComputed;

    void Start() {

        if (first == null)
            first = firstTracked.TrackedPoint;
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
        Quaternion a = useLocal ? first.LocalRotation : first.Rotation;
        Quaternion b = useLocal ? second.LocalRotation : second.Rotation;

        if (Ignore.None != ignore) { 
            IgnoreAxis(ref a);
            IgnoreAxis(ref b);
        }
       
        return Quaternion.Angle(a, b); 
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

    public void IgnoreAxis(ref Quaternion value) {

        throw new System.NotImplementedException();
        //switch (this.ignore)
        //{
        //    //TODO  ignore axe x, y ou z (applatir sur un axe 2D)
            
        //}
    }

}
