using UnityEngine;
using System.Collections;

public class UDP_SenderDebug : MonoBehaviour {

    public UDP_Sender sender;
	void Start () {
        if (sender != null)
            sender.onMessageSent += DisplayMessage;

	}

    private void DisplayMessage(UDP_Sender sender, string message)
    {
        Debug.Log("> Send: " + message);
        Debug.Log("> To: " + sender.GetTargetAdresse()+" : "+sender.GetTargetPort());
    }

    void OnDestroy() {

        if (sender != null)
            sender.onMessageSent -= DisplayMessage;
    
    }
}
