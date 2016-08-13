using UnityEngine;
using System.Collections.Generic;

public class TrackedPoint {

    public delegate Vector3 RecoverPosition();
    public delegate Quaternion RecoverRotation();

    public RecoverPosition accessPosition;
    public RecoverPosition accessLocalPosition;
    public RecoverRotation accessRotation;
    public RecoverRotation accessLocalRotation;

    /// <summary>
    /// Give the possiblity to have a link to a transform
    /// </summary>
    private Transform _linkedTransform;

    public Transform OptinalLinkedTransform
    {
        get { return _linkedTransform; }
        set { _linkedTransform = value; }
    }

    public bool HasPositionDefine() { return accessPosition != null; }
    public bool HasLocalPositionDefine() { return accessLocalPosition != null; }
    public bool HasRotationDefine() { return accessRotation != null; }
    public bool HasLocalRotationDefine() { return accessLocalRotation != null; }
    public bool IsAccessorsDefine() { return HasPositionDefine() && HasLocalPositionDefine() && HasRotationDefine() && HasLocalRotationDefine(); }
    

    public bool IsAccessorDefine() { return accessPosition != null && accessLocalPosition != null && accessRotation != null && accessLocalRotation != null; }

    public Vector3 Position
    {
        get { return accessPosition == null ? Vector3.zero : accessPosition(); }
    }

    public Vector3 LocalPosition
    {
        get { return accessPosition == null ? Vector3.zero : accessLocalPosition(); }
    }

    public Quaternion Rotation
    {
        get { return accessPosition == null ? Quaternion.identity : accessRotation(); }
    }

    public Quaternion LocalRotation
    {
        get { return accessPosition == null ? Quaternion.identity : accessLocalRotation(); }
    }


    public static Dictionary<string, TrackedPoint> trackedPointByIdName = new Dictionary<string, TrackedPoint>();
    public delegate void OnTrackedPointAdd(string idName, TrackedPoint trackedPoint);
    public delegate void OnTrackedPointReplace(string idName, TrackedPoint trackedPoint,TrackedPoint oldOne);
    public delegate void OnTrackedPointRemove(string idName, TrackedPoint trackedPoint);
    public static OnTrackedPointAdd onTrackedPointAdd;
    public static OnTrackedPointReplace onTrackedPointReplace;
    public static OnTrackedPointRemove onTrackedPointRemove;


    #region Static methode to register TrackedPoint
    /// <summary>
    /// Add the tracked point in a hashtable based on idName
    /// </summary>
    /// <param name="idName"></param>
    /// <param name="trackedPoint"></param>
    /// <param name="alreadyExisting"></param>
    public static void Add(string idName, TrackedPoint trackedPoint, out bool alreadyExisting)
    {
        alreadyExisting = false;
        if (trackedPoint == null || string.IsNullOrEmpty(idName)) return;
        TrackedPoint tpInPlace = Get(idName);
        if (tpInPlace != null) { alreadyExisting = true; return; }
        trackedPointByIdName.Add(idName, trackedPoint);
        if (onTrackedPointAdd != null)
            onTrackedPointAdd(idName, trackedPoint);

    }
    /// <summary>
    /// Repalce the tracked point referenced at the idName key
    /// </summary>
    /// <param name="idName"></param>
    /// <param name="trackedPoint"></param>
    /// <param name="oldOne"></param>
    /// <returns></returns>
    public static bool Replace(string idName, TrackedPoint trackedPoint, out TrackedPoint oldOne)
    {
        oldOne = null;
        if (trackedPoint == null || string.IsNullOrEmpty(idName)) return false;
        oldOne = Get(idName);
        trackedPointByIdName.Remove(idName);
        trackedPointByIdName.Add(idName, trackedPoint);
        if (onTrackedPointReplace != null)
            onTrackedPointReplace(idName, trackedPoint, oldOne);
        return true;
    }
    /// <summary>
    /// Remove the tracked point at the referenced at the idName key
    /// </summary>
    /// <param name="idName"></param>
    /// <param name="oldOne"></param>
    public static void Remove(string idName, out TrackedPoint oldOne)
    {
        oldOne = null;
        if (string.IsNullOrEmpty(idName)) return;
        oldOne = Get(idName);
        trackedPointByIdName.Remove(idName);
        if (onTrackedPointRemove != null)
            onTrackedPointRemove(idName, oldOne);
    }

    /// <summary>
    /// Remove this specifique Trackedpoint if referenced at idName  key
    /// </summary>
    /// <param name="idName"></param>
    /// <param name="trackedPointToRemove"></param>
    public static void Remove(string idName, TrackedPoint trackedPointToRemove)
    {
        if (trackedPointToRemove == null || string.IsNullOrEmpty(idName)) return;
        TrackedPoint current = Get(idName);
        if (current == trackedPointToRemove)
        {
            TrackedPoint oldOne;
            Remove(idName, out oldOne);
        }
    }

    public static TrackedPoint Get(string idName)
    {
        TrackedPoint tp;
        trackedPointByIdName.TryGetValue(idName, out tp);
        return tp;
    }


    public static  TrackedPoint[] GetTrackedPoints()
    {
        TrackedPoint[] trackedPoints = new TrackedPoint[trackedPointByIdName.Values.Count];
        trackedPointByIdName.Values.CopyTo(trackedPoints, 0);
        return trackedPoints;
    }
    public static string[] GetTrackedKeys()
    {
        string[] trackedPoints = new string[trackedPointByIdName.Keys.Count];
        trackedPointByIdName.Keys.CopyTo(trackedPoints, 0);
        return trackedPoints;
    }
    #endregion
}
