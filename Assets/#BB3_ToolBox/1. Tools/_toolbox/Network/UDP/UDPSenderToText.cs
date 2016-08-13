using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UDPSenderToText : MonoBehaviour {

    public UDP_Sender sender;
    public Text tDisplayValue;
    public string toDisplay;
    public void Start() {
        sender.onMessageSent += RefreshSend;

    }
    public void RefreshSend(UDP_Sender sender, string message) {
        
        toDisplay = message;
    }

    public void Update(){
        tDisplayValue.text ="Out: "+ toDisplay;
    
    }
}
