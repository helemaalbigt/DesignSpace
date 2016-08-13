using UnityEngine;
using System.Collections;

public class JoystickInput : MonoBehaviour {

    public Vector2 _value;
    public Vector2 Value {
        get
        {
            return _value;
        }
        set
        {
            X = value.x;
            Y = value.y;
        }
    }

    public float X
    {
        get { return _value.x; }
        set
        {
            _value.x = Mathf.Clamp(value, -1f, 1f);
        }
    }
    public float Y
    {
        get { return _value.y; }
        set
        {
            _value.y = Mathf.Clamp(value, -1f, 1f);
        }
    }


    internal void SetValues(float horizontal, float vertical)
    {
        X = horizontal;
        Y = vertical;
    }

}
