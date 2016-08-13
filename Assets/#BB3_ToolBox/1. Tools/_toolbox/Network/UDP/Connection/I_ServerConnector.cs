using UnityEngine;
using System.Collections;

public interface I_ServerConnector
{

    bool IsConnected();
    UDP_Sender GetSender();
    UDP_Receiver GetReceiver();
    string GetServerNameWanted();

    void FoundServer();

}
