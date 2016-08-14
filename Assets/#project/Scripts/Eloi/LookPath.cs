using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

[Serializable]
public class LookPath  {
    [JsonProperty(PropertyName = "C")]
    public string _createdTime;

    [JsonProperty(PropertyName = "S")]
    public string _systemId;
    [JsonProperty(PropertyName = "P")]
    public List<TimeLinkedLookState> _lookStatePath = new List<TimeLinkedLookState>();

    public void AddLookState(float when, Vector3 fromWhere, Vector3 toWhere) {
        AddLookState(
            new TimeLinkedLookState() {
                _timeSinceStartLooking = when,
                _lookState = new LookState() { _rootPosition = fromWhere, _lookAtPosition = toWhere } }
            );
    }

    public LookPath(string systemId) {

        _systemId = systemId;
        _createdTime = DateTime.Now.ToString("yyyy/MM/dd/HH:mm.ss.fff");
    }

    public void AddLookState(TimeLinkedLookState lookState)
    {
        _lookStatePath.Add(lookState);
        //Should add it to the correct time position in the list to be optimised.
    }
    public  void Reset()
    {
        _lookStatePath.Clear();
    }
    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    public static Vector3 RoundVector3(Vector3 value, int digits)
    {
        value.x = Round(value.x, digits);
        value.y = Round(value.y, digits);
        value.z = Round(value.z, digits);
        return value;
    }
}

[System.Serializable]
public struct TimeLinkedLookState
{
    [JsonProperty(PropertyName = "T")]

    public float TimeOfRecord
    {
        get { return LookPath.Round(_timeSinceStartLooking,2); }
        set { _timeSinceStartLooking = value; }
    }

    
    [JsonIgnore]
    public float _timeSinceStartLooking;

    [JsonProperty(PropertyName = "LR")]
    public LookState _lookState;
}
[System.Serializable]
public struct LookState {

    [JsonProperty(PropertyName = "PR")]
    public Vector3 RootPosition
    {
        get { return LookPath.RoundVector3(_rootPosition, 2); }
        set { _rootPosition = value; }
    }
    [JsonIgnore]
    public Vector3 _rootPosition;

    [JsonProperty(PropertyName = "LR")]
    public Vector3 LookPosition
    {
        get { return LookPath.RoundVector3(_lookAtPosition, 2); }
        set { _lookAtPosition = value; }
    }

    [JsonIgnore]
    public Vector3 _lookAtPosition;


    //FOR THOMAS FROM ELOI EXEMPLE OF PROPERTY USING JSON
    //private int _whatHell;

    //[JsonProperty(PropertyName = "Test")]
    //public int MyProperty
    //{
    //    get { return UnityEngine.Random.Range(0,100); }
    //    set { Debug.Log(value); }
    //}
    //FOR THOMAS FROM ELOI EXEMPLE OF PROPERTY USING JSON
}
