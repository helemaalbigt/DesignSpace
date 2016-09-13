using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class KeyValue : MonoBehaviour {

    public event Action<string> OnKeyPress;

    private string _keyValue;

    // Use this for initialization
    void Start () {
        try
        {
            _keyValue = GetComponentInChildren<Text>().text;
        }
        catch
        {
            Debug.LogError("Couldn't find text component in children");
        }
    }
	
	public void KeyPress()
    {
        if (OnKeyPress != null)
            OnKeyPress(_keyValue);
    }
}
