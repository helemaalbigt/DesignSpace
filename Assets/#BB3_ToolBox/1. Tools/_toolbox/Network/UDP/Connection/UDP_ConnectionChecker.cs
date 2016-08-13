using UnityEngine;
using System.Collections;

public class UDP_ConnectionChecker : MonoBehaviour {

    public UDP_Connection connection;

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
                    onConnectionDone(connection.sender.ip, connection.sender.port);
                    
                }
                else if (!value && onConnectionLost != null)
                {
                    onConnectionLost(connection.sender.ip, connection.sender.port);

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
        connection.receiver.onPackageReceived += CheckForPong;
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
            connection.sender.Send("Pong:" + numToReturn);
        } 


    }
    void Ping() {
        if (connection == null) return;
        if (!pongFound && connection.IsConnected())
        {
            missedPing++;
        }
        else missedPing = 0;

        randomPingNumber = ""+Random.Range(0, 1000);
        pongFound = false;
        connection.sender.Send("Ping:" + randomPingNumber);

        IsConnected = connection.IsConnected() &&  missedPing <= missPingTolerance;
    }

    void Reset() {
        if (connection == null)
            connection = GetComponent<UDP_Connection>() as UDP_Connection;
    }
}
