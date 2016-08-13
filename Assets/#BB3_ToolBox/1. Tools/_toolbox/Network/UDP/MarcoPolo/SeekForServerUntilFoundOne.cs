using UnityEngine;
using System.Collections;
[System.Obsolete("")]
public class SeekForServerUntilFoundOne : MonoBehaviour {

    public UDP_MarcoPolo marcoPolo;
    public string serverFoundIp;
    public int serverFoundPort;
    public TextZoneToLanControlData textZone;
    public UDP_Sender sender;
	
    void Start () {
        marcoPolo.onServerDetected += ServerFound;
	}
    void OnDestroy() {
        marcoPolo.onServerDetected -= ServerFound;
    }
    private void ServerFound(string userIp, int port)
    {
        serverFoundIp = userIp;
        serverFoundPort = port;
        if(textZone!=null)
        textZone.SetTextIPTo(userIp, port);
        if (sender != null)
            sender.TargetClientToSend(userIp, port, true);

    }
	
}
