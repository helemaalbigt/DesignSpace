using UnityEngine;
using System.Collections;

public class SetUDPSendFromLanControlData : MonoBehaviour {

    public UDP_Sender sender;
 	void Update () {
        if (sender != null && LAN_Control_Data.Instance != null)
        {
            if (!sender.HasTargetClient())
            {
                string address = LAN_Control_Data.Instance.GetTargetAddress();
                int port = LAN_Control_Data.Instance.GetTargetPort();
                sender.TargetClientToSend(address, port, true);
                Destroy(this);
            }
        }
	
	}
}
