using UnityEngine;
using System.Collections;

public class TTD_Flic : MonoBehaviour {

	// Use this for initialization

    public bool global;
    public bool local=true;
    void Start()
    {
        if (global) { 
            Flics.onNewButtonDetected += Debug_ButtonDetected;
            Flics.onButtonStateChange += Debug_ButtonStateChangeDisplay;
            Flics.onButtonActionDetected += Debug_ButtonActionDetectedDisplay;
        }
        if (local)
            Flics.onNewButtonDetected += AddLocalListener;
    }

    private void AddLocalListener(Flics.Button button, string id)
    {
        Debug_ButtonDetected(button, id);
        button.onStateChange += Debug_ButtonStateChangeDisplay;
        button.onActionDetected += Debug_ButtonActionDetectedDisplay;
    }
    void OnDestroy()
    {

        if (global) { 
            Flics.onNewButtonDetected -= Debug_ButtonDetected;
            Flics.onButtonStateChange -= Debug_ButtonStateChangeDisplay;
            Flics.onButtonActionDetected -= Debug_ButtonActionDetectedDisplay;
        }
    }

    private void Debug_ButtonDetected(Flics.Button button, string id)
    {
        Debug.Log("Button (NEW) :" + id);
    }

    private void Debug_ButtonActionDetectedDisplay(Flics.Button button, string id, Flics.FlicButtonAction action, string message)
    {
        Debug.Log("Button "+id+" (Action) :" + action +"  MSG> "+message);
    }

    private void Debug_ButtonStateChangeDisplay(Flics.Button button, string id, bool isDown)
    {
        Debug.Log("Button " + id + " (State) :" + id);
    }
	
}
