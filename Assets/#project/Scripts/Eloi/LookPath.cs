using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

[Serializable]
public class LookPath  {
    [JsonProperty(PropertyName = "Path")]
    public List<TimeLinkedLookState> _lookStatePath = new List<TimeLinkedLookState>();

    public void AddLookState(float when, Vector3 fromWhere, Vector3 toWhere) {
        AddLookState(
            new TimeLinkedLookState() {
                _timeSinceStartLooking = when,
                _lookState = new LookState() { _rootPosition = fromWhere, _lookAtPosition = toWhere } }
            );
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
}

[System.Serializable]
public struct TimeLinkedLookState
{
    [JsonProperty(PropertyName = "RecordedTime")]
    public float _timeSinceStartLooking;
    [JsonProperty(PropertyName = "Record")]
    public LookState _lookState;
}
[System.Serializable]
public struct LookState {

    [JsonProperty(PropertyName = "Position")]
    public Vector3 _rootPosition;

    [JsonProperty(PropertyName = "LookingPosition")]
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
