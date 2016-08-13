using UnityEngine;
using System.Collections;

public class MobileAcceleroToVirutalController : MonoBehaviour {

    public VirtualController virutalController;
    public int joystickIndex;
    void Update()
    {
        if (virutalController != null)
            virutalController.SetJoystickValue(joystickIndex, HomidoUtility.GetAccelerationAsJoystick(0.5f, 0.5f, true, true));

    }
}
