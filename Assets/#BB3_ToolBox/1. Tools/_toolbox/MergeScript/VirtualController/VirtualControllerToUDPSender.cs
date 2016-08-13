using UnityEngine;
using System.Collections;

public class VirtualControllerToUDPSender : MonoBehaviour
{
    public string networkId;
    public string controllerName;
    public UDP_Sender udpSender;
    //public TCP_Sender tcpSender;
    public ListenToChange[] changeToSendByUdp;
    public VirtualController virtualControl;

    public bool withDebug;
    void Start () {
        virtualControl = VirtualController.Get(controllerName);
        Init();
        foreach (ListenToChange changeListen in changeToSendByUdp) {
            SendUDPWhenChange(changeListen.whatToListen, changeListen.indexToListen,changeListen.onlyOnChange);
        
        }
	}

    private void SendUDPWhenChange(ListenToChange.WhatToListenTo whatToListenTo, int index, bool onlyOnChange)
    {

        switch (whatToListenTo)
        {
            case ListenToChange.WhatToListenTo.Joystick:
                if(onlyOnChange)
                    virtualControl.GetJoystick(index).onValueChanged += JoystickChange;
                else
                    virtualControl.GetJoystick(index).onValueAffected += JoystickChange;
                break;
            case ListenToChange.WhatToListenTo.Trigger:
                if (onlyOnChange)
                    virtualControl.GetTrigger(index).onValueChanged += TriggerChange;
                else
                     virtualControl.GetTrigger(index).onValueAffected += TriggerChange;
                break;
            case ListenToChange.WhatToListenTo.Button:
                if (onlyOnChange)
                    virtualControl.GetButton(index).onValueChanged += ButtonChange;
                else
                virtualControl.GetButton(index).onValueAffected += ButtonChange;
                break;
            case ListenToChange.WhatToListenTo.Request:
                if (onlyOnChange)
                    virtualControl.GetRequests().onValueChanged += RequestChange;
                else
                virtualControl.GetRequests().onValueAffected += RequestChange;
                break;
            case ListenToChange.WhatToListenTo.Positioning:
                if (onlyOnChange)
                    virtualControl.GetPosition(index).onValueChanged += PositioningChange;
                else
                virtualControl.GetPosition(index).onValueAffected += PositioningChange;
                break;
            case ListenToChange.WhatToListenTo.Orientation:
                if (onlyOnChange)
                    virtualControl.GetOrientation(index).onValueChanged += OrientationChange;
                else
                  virtualControl.GetOrientation(index).onValueAffected += OrientationChange;
                break;
            case ListenToChange.WhatToListenTo.TouchPad:
                if (onlyOnChange)
                    virtualControl.GetTouchPad(index).onValueChanged += TouchPadChange;
                else
                virtualControl.GetTouchPad(index).onValueAffected += TouchPadChange;
                break;
            default:
                break;
        }

    }

    private void JoystickChange(int index, Vector3 oldValue, Vector3 newValue)
    {
        SendCommand(string.Format("Joy:{0}:{1:0.000}:{2:0.000}", index, newValue.x, newValue.y));
    }

    private void TriggerChange(int index, float oldValue, float newValue)
    {
        SendCommand(string.Format("Tri:{0}:{1:0.000}", index, newValue));
    }

    private void RequestChange(int index, string oldValue, string newValue)
    {
        SendCommand(string.Format("Req:{0}", newValue));
    }

    private void PositioningChange(int index, Vector3 oldValue, Vector3 newValue)
    {
        SendCommand(string.Format("Pos:{0}:{1:0.000}:{2:0.000}:{3:0.000}", index, newValue.x, newValue.y, newValue.z));
    }

    private void OrientationChange(int index, Quaternion oldValue, Quaternion newValue)
    {
        SendCommand(string.Format("Ori:{0}:{1:0.000}:{2:0.000}:{3:0.000}:{4:0.000}", index, newValue.x, newValue.y, newValue.z, newValue.w));
    }

    private void TouchPadChange(int index, Vector2 oldValue, Vector2 newValue)
    {

        SendCommand(string.Format("Pad:{0}:{1:0.000}:{2:0.000}", index, newValue.x, newValue.y));

    }

    private void ButtonChange(int index, bool oldValue, bool newValue)
    {
        SendCommand(string.Format("But:{0}:{1}",index,newValue?1:0));

    }
    private void SendCommand(string cmdToSend) { 
        if (!udpSender) return;
        string cmd = GetPlayerCmdStart();
        cmd += cmdToSend;
        if(withDebug)
        Debug.Log("Send...  " + cmd);
        udpSender.Send(cmd);    
    }

    public string GetPlayerCmdStart() {
        return string.Format("N:{0}|", networkId);
    }

    void Init() {
        if (string.IsNullOrEmpty(networkId))
        { 
            string id =Network.player.ipAddress;
            if(id.Length>3)
                id = id.Substring(id.Length-3);
            id = id.Replace(".", "");
            networkId = id;
        }
    }


    [System.Serializable]
    public struct ListenToChange { 
        public enum WhatToListenTo {Joystick, Trigger, Button, Request, Positioning, Orientation, TouchPad}
        public WhatToListenTo whatToListen;
        public int indexToListen;
        public bool onlyOnChange;
        public string memo;
    }
}
