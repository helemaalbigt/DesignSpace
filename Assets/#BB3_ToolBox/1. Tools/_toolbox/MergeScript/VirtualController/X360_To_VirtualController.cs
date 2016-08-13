using UnityEngine;
using System.Collections;

public class X360_To_VirtualController : MonoBehaviour {

    public VirtualControllerAccessor accessor;
	
	void Update () {
        if (accessor == null) 
            return;

        //accessor.Controller.SetJoystickValue(0, X360.GetDirection(X360.Direction.Left));
        //accessor.Controller.SetJoystickValue(1, X360.GetDirection(X360.Direction.Right));
        //accessor.Controller.SetJoystickValue(3, X360.GetDirection(X360.Direction.Arrow));

        //accessor.Controller.SetButtonValue(0, X360.IsActive(X360.ButtonType.A));
        //accessor.Controller.SetButtonValue(1, X360.IsActive(X360.ButtonType.B));
        //accessor.Controller.SetButtonValue(2, X360.IsActive(X360.ButtonType.X));
        //accessor.Controller.SetButtonValue(3, X360.IsActive(X360.ButtonType.Y));
        //accessor.Controller.SetButtonValue(4, X360.IsActive(X360.ButtonType.Menu));
        //accessor.Controller.SetButtonValue(5, X360.IsActive(X360.ButtonType.Start));
        //accessor.Controller.SetButtonValue(6, X360.IsActive(X360.ButtonType.LeftButton));
        //accessor.Controller.SetButtonValue(7, X360.IsActive(X360.ButtonType.RightButton));
        //accessor.Controller.SetButtonValue(8, X360.IsActive(X360.ButtonType.PullLeftJoystick));
        //accessor.Controller.SetButtonValue(9, X360.IsActive(X360.ButtonType.PullRightJoystick));

        //accessor.Controller.SetTriggerValue(0, X360.GetAxe(X360.Axis.TriggerLeft));
        //accessor.Controller.SetTriggerValue(1, X360.GetAxe(X360.Axis.TriggerRight));
	}
}
