using UnityEngine;
using System.Collections;

public class ScreenTouchToVirtualTouchPad : MonoBehaviour {

    public VirtualControllerAccessor accessor;
    public int touchPadId;
    public Vector2 screenPosition;
    public Vector2 screenPourcentPosition;
    private Vector2 lastPosition;
	
	void Update () {

        Vector3 position = Vector3.zero; 

        if (Input.GetMouseButton(0)) {
            position = Input.mousePosition;
        }

        if (Input.touchCount > 0 )
        {
            position = Input.GetTouch(0).position;

        }

        screenPosition = position;
        if (screenPosition != lastPosition && screenPosition!=Vector2.zero) {
            lastPosition = screenPosition;
            screenPourcentPosition.x = screenPosition.x / (float)Screen.width;
            screenPourcentPosition.y = screenPosition.y / (float)Screen.height;
            accessor.Controller.SetTouchPadValue(touchPadId, screenPourcentPosition);
        }
    }
}
