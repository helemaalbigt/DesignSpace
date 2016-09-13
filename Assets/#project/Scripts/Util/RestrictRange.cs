using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(InputField))]
public class RestrictRange : MonoBehaviour{

    public float _minValue;
    public float _maxValue;

    private InputField _targetField;

    void Start()
    {
        _targetField = GetComponent<InputField>();
        _targetField.onEndEdit.AddListener(Restrict);
    }

    public void Restrict(string value)
    {
        float val = float.Parse(value);
        _targetField.text = Mathf.Clamp(val, _minValue, _maxValue) + "";
    }
}
