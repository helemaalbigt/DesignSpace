using UnityEngine;
using System.Collections;

public class UDP_AFK_Checker : MonoBehaviour {

    public MonoBehaviour connectorScript;
    public I_ServerConnector connector;

    public delegate void OnConnectionLost(string lostIp, int lostPort);
    public OnConnectionLost onConnectionLost;
    public delegate void OnConnectionEstablish(string lostIp, int lostPort);
    public OnConnectionEstablish onConnectionDone;

    public  bool _isConnected;

    public bool IsConnected
    {
        get { return _isConnected; }
        private set {
            if (_isConnected != value) {
                if (value && onConnectionDone != null)
                {
                    onConnectionDone(connector.GetSender().ip, connector.GetSender().port);
                    
                }
                else if (!value && onConnectionLost != null)
                {
                    onConnectionLost(connector.GetSender().ip, connector.GetSender().port);

                }
                
            }    
            _isConnected = value;
        
        }
    }

    public int missedPing;

    public float delayBetweenPing=1f;
    public int missPingTolerance=3;
    public string randomPingNumber;
    private bool pongFound;

    void Start() {

        connector = (I_ServerConnector) connectorScript;

        connector.GetReceiver().onPackageReceivedParallelThread += CheckForPong;
        InvokeRepeating("Ping", 0, delayBetweenPing);
    }

    private void CheckForPong(UDP_Receiver from, string message, string adresse, int port)
    {
        string [] tokens = message.Split(':');
        if (tokens.Length==2 && tokens[0].Equals("Pong") && tokens[1].Equals(randomPingNumber))
        {
            pongFound = true;
        }
        if (tokens.Length==2 && tokens[0].Equals("Ping"))
        {
            string numToReturn = tokens[1];
            connector.GetSender().Send("Pong:" + numToReturn);
        } 


    }
    void Ping() {
        if (connector == null) return;
        if (!pongFound && connector.IsConnected())
        {
            missedPing++;
        }
        else missedPing = 0;

        randomPingNumber = ""+Random.Range(0, 1000);
        pongFound = false;
        connector.GetSender().Send("Ping:" + randomPingNumber);

        IsConnected = connector.IsConnected() && missedPing <= missPingTolerance;
    }

    void Reset() {
        if (connector == null)
            connector = GetComponent<UDP_Connection>() as UDP_Connection;
    }
}
