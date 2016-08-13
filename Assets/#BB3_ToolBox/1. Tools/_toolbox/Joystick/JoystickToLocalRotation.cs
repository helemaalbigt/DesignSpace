using UnityEngine;
using System.Collections;

public class JoystickToLocalRotation : MonoBehaviour {

     [Header("This script take joystick data and convert it in object local rotation")]
    public JoystickInput joystickInput;
    public Transform toRotate;
    public Vector3 joystick;


    public float horizontalRot= 50f;
    public float verticalRot = 30f;
    public float rollRot = 50f;
   

    void Update()
    {

        joystick = Vector3.Lerp(joystick, joystickInput.Value, Time.deltaTime);
        float rotHori = joystick.x * horizontalRot;
        toRotate.localRotation = Quaternion.Euler(-joystick.y * verticalRot, joystick.x * horizontalRot, joystick.x * rollRot);
    }
}
