using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UDP_AFK_Checker))]
public class UDP_SeekNewServerWhenNoMore : MonoBehaviour {

    public UDP_AFK_Checker connectionChecker;


    void Start()
    {
        Reset();
        connectionChecker.onConnectionLost += SeekNewConnection;
	}
    public void SeekNewConnection(string ip, int port) {

        connectionChecker.connector.FoundServer();
    }
    void Reset()
    {
        if (connectionChecker == null)
            connectionChecker = GetComponent<UDP_AFK_Checker>() as UDP_AFK_Checker;
    }
}
