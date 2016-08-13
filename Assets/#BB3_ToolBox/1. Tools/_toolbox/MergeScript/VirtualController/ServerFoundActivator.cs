using UnityEngine;
using System.Collections;

public class ServerFoundActivator : MonoBehaviour {

    public UDP_MarcoPolo marcopolo;
    public EventActivator activator;
	// Use this for initialization
	void Start () {
        marcopolo.onServerDetected += ActivateObjects;
      
	}
    private void ActivateObjects(string userIp, int portToReply)
    {
        activator.Activate();
    }
    void OnDestroy() {

        marcopolo.onServerDetected -= ActivateObjects;
    }
	
}
