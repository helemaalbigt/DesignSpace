//using UnityEngine;
//
//public class TouchPadToPlayerAnimator : MonoBehaviour {
//
//
//
//    void Awake()
//    {
//        OVRTouchpad.Create();
//        OVRTouchpad.TouchHandler += HandleTouchHandler;
//
//    }
//    void Destroy()
//    {
//        OVRTouchpad.TouchHandler -= HandleTouchHandler;
//
//    }
//
//    private void HandleTouchHandler(object sender, System.EventArgs e)
//    {
//        OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;
//        if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Right)
//        {
//           // playerAnimator.LauchObjectInBag();
//        }
//        if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Right || touchArgs.TouchType == OVRTouchpad.TouchEvent.Down)
//        {
//
//        }
//    }
//	
//	void Update () {
//
//        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Keypad5)) ;
//
//	}
//
//}
