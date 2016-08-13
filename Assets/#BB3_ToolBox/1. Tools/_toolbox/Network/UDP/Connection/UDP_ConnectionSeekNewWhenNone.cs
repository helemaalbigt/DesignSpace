using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UDP_ConnectionChecker))]
public class UDP_ConnectionSeekNewWhenNone : MonoBehaviour {

    public UDP_ConnectionChecker connectionChecker;


    void Start()
    {
        connectionChecker.onConnectionLost += SeekNewConnection;
	}
    public void SeekNewConnection(string ip, int port) {
        Debug.Log("What ?");
        connectionChecker.connection.StartSeekingServer();
    }
    void Reset()
    {
        if (connectionChecker == null)
            connectionChecker = GetComponent<UDP_ConnectionChecker>() as UDP_ConnectionChecker;
    }
}
