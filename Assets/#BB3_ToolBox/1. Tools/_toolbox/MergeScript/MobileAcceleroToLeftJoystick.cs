using UnityEngine;
using System.Collections;

public class MobileAcceleroToLeftJoystick : MonoBehaviour {

    public JoystickInput joystick;
	void Update () {
        if(joystick!=null)
        joystick.Value = HomidoUtility.GetAccelerationAsJoystick(0.5f, 0.5f, true, true);
	
	}
}
