using UnityEngine;
using System.Collections;

public abstract class BasicUnityTrackingPoint : MonoBehaviour {

    public string idName;
    private TrackedPoint _trackedPoint = new TrackedPoint();
    public TrackedPoint TrackedPoint
    {
        get { return _trackedPoint; }
    }
    public bool trackedPointGlobal = true;


    public bool AddAndRegister()
    {
        bool alreadyExisting = false;
        if (trackedPointGlobal)
            TrackedPoint.Add(idName, TrackedPoint , out alreadyExisting);
        if (alreadyExisting)
            Debug.LogWarning("There already are a tracked point registerer at this ID:  "+idName, this.gameObject);
        return ! alreadyExisting;
    }


    private void Start()
    {
        DefineAccessorDelegate();
        AddAndRegister();

    }



    public virtual void DefineAccessorDelegate() 
    {
        Debug.Log("You have define no accessor on your inherited class. Please do it for the tracking point to be operant.");
    } 

    private Quaternion GetLocalRotation()
    {
        return transform.localRotation;
    }

    private Quaternion GetRotation()
    {
        return transform.rotation;
    }

    private Vector3 GetLocalPosition()
    {
        return transform.localPosition;
    }

    private Vector3 GetPosition()
    {
        return transform.position;
    }
    private void OnDestroy()
    {
        BeforeDestroy();
        _trackedPoint.accessPosition = null;
        _trackedPoint.accessLocalPosition = null;
        _trackedPoint.accessRotation = null;
        _trackedPoint.accessLocalRotation = null;
        _trackedPoint.OptinalLinkedTransform = null;

        if (trackedPointGlobal)
            TrackedPoint.Remove(idName, _trackedPoint);
    }
    /// <summary>
    /// Please use this methode instead of OnDestroy()
    /// I do not want people, or me, to forgot to add and remove from TrackingPoint Register, so OnDestroy and Start are forbiten.
    /// </summary>
    protected virtual void BeforeDestroy() { }
    /// <summary>
    /// Please use this methode instead of Start()
    /// I do not want people, or me, to forgot to add and remove from TrackingPoint Register, so OnDestroy and Start are forbiten.
    /// </summary>
    protected virtual void AfterStart() { }
}


public abstract class RatioBase
{
    public bool active = false;
    public bool IsActive() { return active; }
    public abstract bool GetRatio(float value, out float ratio);
}

[System.Serializable]
public abstract class Ratio : RatioBase
{
    public abstract bool GetValue(out float value);
    public override bool GetRatio(float value, out float ratio)
    {
        float dist=0f;
        ratio = 0f;
        if (!GetValue(out dist))
        return false;
        if(dist==0f) return false;

        ratio = value/dist;
        return IsActive();

    }
    
}
[System.Serializable]
public class TrP_DistanceRatio : Ratio
{
    public BasicUnityTrackingPoint fromTracked, toTracked;
    public TrackedPoint from, to;
    public bool useLocal = true;
    public enum Ignore {None,X,Y,Z }
    public Ignore ignore;
    public Ignore ignorePlus;

    public override bool GetValue(out float distance)
    {
        distance = 0f;
        if (from == null)
        {
            if (fromTracked == null) return false;
            from = fromTracked.TrackedPoint;
            if (from == null) return false;
        }
        if (to == null)
        {
            if (toTracked == null) return false;
            to = toTracked.TrackedPoint;
            if (to == null) return false;
        }
        Vector3 f = useLocal? from.LocalPosition : from.Position;
        Vector3 t = useLocal? to.LocalPosition : to.Position;
        IgnoreAxis(ref f);
        IgnoreAxis(ref t);
        distance = Vector3.Distance(f, t);
        return true;

     
    }
    public void IgnoreAxis(ref Vector3 value)
    {
        switch (this.ignore)
        {
            case Ignore.X: value.x = 0; break;
            case Ignore.Y: value.y = 0; break;
            case Ignore.Z: value.z = 0; break;
        } switch (this.ignorePlus)
        {
            case Ignore.X: value.x = 0; break;
            case Ignore.Y: value.y = 0; break;
            case Ignore.Z: value.z = 0; break;
        }
    }

}
[System.Serializable]
public class FixedDistanceRatio : Ratio
{
    public float distance=1f;
    public override bool GetValue(out float distance)
    {
        distance = this.distance;
        return active;
    }
}
[System.Serializable]
public class TransformDistanceRatio : Ratio
{
    public Transform from;
    public Transform to;
    public bool useLocal = true;
    public enum Ignore { None, X, Y, Z }
    public Ignore ignore;
    public Ignore ignorePlus;

    public override bool GetValue(out float distance)
    {
        distance = 0f;
        
        Vector3 f = useLocal ? from.localPosition : from.position;
        Vector3 t = useLocal ? to.localPosition : to.position;
        IgnoreAxis(ref f);
        IgnoreAxis(ref t);
        distance = Vector3.Distance(f, t);
        return true;


    }
    public void IgnoreAxis(ref Vector3 value)
    {
        switch (this.ignore)
        {
            case Ignore.X: value.x = 0; break;
            case Ignore.Y: value.y = 0; break;
            case Ignore.Z: value.z = 0; break;
        } switch (this.ignorePlus)
        {
            case Ignore.X: value.x = 0; break;
            case Ignore.Y: value.y = 0; break;
            case Ignore.Z: value.z = 0; break;
        }
    }
}

/// <summary>
/// Return the first ratio active with its values valide
/// </summary>
[System.Serializable]
public class SeveralKindRatio : Ratio
{
    public Ratio[] ratios = new Ratio [] {
        new FixedDistanceRatio() { active = true },
        new TransformDistanceRatio() { active = false },
        new TrP_DistanceRatio() { active = false }
    };

    public FixedDistanceRatio f;
 
    public override bool GetValue(out float value)
    {
        foreach (Ratio rat in ratios)
            if (rat.IsActive())
                return rat.GetValue(out value);
        value = 0;
        return false;
    }

    public override bool GetRatio(float value, out float ratio)
    {
        foreach (Ratio rat in ratios)
            if (rat.IsActive())
                return rat.GetRatio(value, out ratio);
        ratio = 0;
        return false;
    }
}