//using UnityEngine;
//using System.Collections;
//
//public class InputSelector : MonoBehaviour {
//
//    public static InputSelector Instance;
//    public Transform udp;
//    public UDP_Connection connection;
//    public string kinectControlName= "KinectInfo";
//    public VirtualController kinectVirtualController;
//    public Transform kinectControl;
//    public Transform keyboardControl;
//    public Transform gamepadControl;
//    public Transform defaultControl;
//
//    public enum ControlSelected { View, Gamepad, Keyboard, Kinect}
//    public ControlSelected selection = ControlSelected.View;
//    public int level;
//    public string levelNameNeeded = "MainMenu";
//    public string levelName;
//
//    public void Awake()
//    {
//        Instance = this;
//       DontDestroyOnLoad(this.gameObject);
//       SetSelectionTo(ControlSelected.View);
//       Init();
//    }
//
//    public void OnLevelWasLoaded(int level){
//        Init();
//    
//    }
//    private void Init() {
//
//        level = Application.loadedLevel;
//        levelName = Application.loadedLevelName;
//        kinectVirtualController = VirtualController.Get(kinectControlName);
//        if (levelName.Equals( levelNameNeeded))
//        {
//            udp.gameObject.SetActive(true);
//        }
//        else {
//            if (selection != ControlSelected.Kinect)
//                udp.gameObject.SetActive(false);
//        
//        }
//        
//    }
//
//	
//	void Update () {
//        if (levelName.Equals(levelNameNeeded))
//        {
//
//            //if (selection!= ControlSelected.Keyboard && IsKeyBoardDetected())
//            //    SetSelectionTo(ControlSelected.Keyboard);
//            if (selection != ControlSelected.Kinect && IsKinectDetected())
//                SetSelectionTo(ControlSelected.Kinect);
//            if (selection != ControlSelected.Gamepad && IsControllerDetected())
//                SetSelectionTo(ControlSelected.Gamepad);
//            if (selection != ControlSelected.View && IsClickDetected())
//                SetSelectionTo(ControlSelected.View);
//        }
//    }
//
//
//
//    public void SetSelectionTo(ControlSelected selected)
//    {
//        kinectControl.gameObject.SetActive(selected == ControlSelected.Kinect);
//        keyboardControl.gameObject.SetActive(selected == ControlSelected.Keyboard);
//        gamepadControl.gameObject.SetActive(selected == ControlSelected.Gamepad);
//        defaultControl.gameObject.SetActive(selected == ControlSelected.View);
//        selection = selected;
//    }
//
//    private bool IsKinectDetected()
//    {
//        return connection.IsConnected() && kinectVirtualController.GetPositionValue(2) != Vector3.zero;
//    }
//
//    private bool IsControllerDetected()
//    {
//        return OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.LeftXAxis) != 0 || OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.RightXAxis) != 0; 
//    }
//
//    private bool IsKeyBoardDetected()
//    {
//        return Input.anyKey;
//    }
//    private bool IsClickDetected()
//    {
//        return Input.GetMouseButton(0);
//    }
//}
