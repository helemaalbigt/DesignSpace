using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReceiverToVirtualController :MonoBehaviour {

    public UDP_Receiver receiver;
    public Dictionary<string,VirtualController> multipleControllers = new Dictionary<string,VirtualController>();

    public bool withDebugJoystick;
    public bool withDebugButton;
    public bool withDebugTouchePad;
    public bool withDebugTrigger;
    public bool withDebugRequest;
    public bool withDebugPosition;
    public bool withDebugOrientation;

    public delegate void OnNewControllerDetected(string idKey, VirtualController controller);
    public OnNewControllerDetected onNewController;

    //public Queue<string> userToCreate = new Queue<string>();

    void Start () {
        if(receiver!=null )
            receiver.onPackageReceivedParallelThread += TranslatePackageToController;
	
	}
    void Update() {
        
            //while (userToCreate.Count > 0) 
            //   CreateGameObjectController(userToCreate.Dequeue());
        
    }

    private void RegisterUser(ref string idKey, ref VirtualController controller)
    {
        if (!string.IsNullOrEmpty(idKey))
        {
            if (!multipleControllers.ContainsKey(idKey))
            {
                multipleControllers[idKey] = controller;
                if (onNewController != null)
                    onNewController(idKey, controller);
            }
        }
    
    }

    void OnDestroy()
    {
        if (receiver != null)
            receiver.onPackageReceivedParallelThread -= TranslatePackageToController;
    }

    private void TranslatePackageToController(UDP_Receiver from, string message, string ip, int port)
    {
        if (message == null || message.Length <= 0) return;
        string[] tokens = message.Split('|');
        if (tokens.Length != 2) return;
        string[] tokensPlayer = tokens[0].Split(':');
        string[] tokensController = tokens[1].Split(':');
   
        VirtualController controller =null;
        if (tokensPlayer.Length==2) {

            if (tokensPlayer[0].Equals("N"))
            {
                string idKey = tokensPlayer[1];
                controller= VirtualController.Get(idKey);
                RegisterUser(ref idKey, ref controller);
               }
                   
         }

        if (controller == null) 
            return;

        


            if (tokensController.Length == 4 && tokensController[0].Equals("Joy") )
            {
                int index = int.Parse(tokensController[1]);
                float x = float.Parse(tokensController[2]);
                float y = float.Parse(tokensController[3]);
                controller.SetJoystickValue(index, new Vector3(x, y, 0));
                if(withDebugJoystick)
                Debug.Log(string.Format("Detected Joystick: {0}  {1},{2}", index, x, y));
            }
            else if (tokensController.Length == 3 && tokensController[0].Equals("But"))
            {
                int index = int.Parse(tokensController[1]);
                int bolValue = int.Parse(tokensController[2]);
                controller.SetButtonValue(index, bolValue != 0);
                if (withDebugButton)
                Debug.Log(string.Format("Detected Button: {0}  {1}", index, bolValue != 0));
            }
            else if (tokensController.Length == 3 && tokensController[0].Equals("Tri"))
            {
                int index = int.Parse(tokensController[1]);
                int triggerValue = int.Parse(tokensController[2]);
                controller.SetTriggerValue(index, triggerValue);
                if (withDebugTrigger)
                Debug.Log(string.Format("Detected Trigger: {0}  {1}", index, triggerValue == 0));
            }
            else if (tokensController.Length == 4 && tokensController[0].Equals("Pad"))
            {
                int index = int.Parse(tokensController[1]);
                float x = float.Parse(tokensController[2]);
                float y = float.Parse(tokensController[3]);
                controller.SetTouchPadValue(index, new Vector2(x, y));
                if (withDebugTouchePad)
                    Debug.Log(string.Format("Detected Pad: {0}  {1},{2}", index, x, y));
            }

            else if (tokensController.Length == 5 && tokensController[0].Equals("Pos"))
            {
                int index = int.Parse(tokensController[1]);
                float x = float.Parse(tokensController[2]);
                float y = float.Parse(tokensController[3]);
                float z = float.Parse(tokensController[4]);
                controller.SetPositionValue(index, new Vector3(x, y, z));
                if (withDebugPosition)
                    Debug.Log(string.Format("Detected Position: {0}  {1},{2},{3}", index, x, y, z));
            }

            else if (tokensController.Length == 6 && tokensController[0].Equals("Ori"))
            {
                int index = int.Parse(tokensController[1]);
                float x = float.Parse(tokensController[2]);
                float y = float.Parse(tokensController[3]);
                float z = float.Parse(tokensController[4]);
                float w = float.Parse(tokensController[5]);
                controller.SetOrientationValue (index, new Quaternion(x,y,z,w));
                if (withDebugOrientation)
                    Debug.Log(string.Format("Detected Orientation: {0}  {1},{2},{3},{4}", index, x, y,z,w));
            }
            else if (tokensController.Length == 2 && tokensController[0].Equals("Req"))
            {
                string request =tokensController[1];
                controller.SetRequestValue(request);
                if (withDebugRequest)
                Debug.Log(string.Format("Detected Request: {0}  ", request));
            }

        
    }
    public VirtualController GetController(string idKey)
    {return multipleControllers[idKey];}

    public VirtualController [] GetControllers()
    {
        VirtualController [] result = new VirtualController [multipleControllers.Values.Count];
        multipleControllers.Values.CopyTo(result, 0);
        return result;
    }
    public string [] GetUserKeyNames()
    {
        string[] result = new string[multipleControllers.Values.Count];
        multipleControllers.Keys.CopyTo(result, 0);
        return result;
    }
    


    
}
