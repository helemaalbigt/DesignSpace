using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Net;

public class UDP_Server : MonoBehaviour {


    public string serverName="Unnamed";

    public UDP_Sender sender;
    public UDP_Receiver receiver;



    public Queue<UDP_Message> messageReceived = new Queue<UDP_Message>();
    public virtual void Start()
    {
        StartListenToReceiver();
    }

     public virtual void Update()
    {
        while (messageReceived.Count >= 1)
            CheckForSeeker(messageReceived.Dequeue());
    }
    private void StartListenToReceiver()
    {
        receiver.onPackageReceivedParallelThread += ReceivedUdpMessage;

    }

    public void OnDestroy()
    {
        receiver.onPackageReceivedParallelThread -= ReceivedUdpMessage;

    }
    private void ReceivedUdpMessage(UDP_Receiver from, string _message, string _ip, int _port)
    {
        if (string.IsNullOrEmpty(_message)) {
            Debug.LogWarning("UDP Message received should not be null", this.gameObject);
        }
        UDP_Message msg = new UDP_Message() { message = _message, ip = _ip, port = _port };

        messageReceived.Enqueue(msg);
    }


    protected virtual void CheckForSeeker(UDP_Message udpMessage)
    {
        if (0==udpMessage.message.IndexOf("SeekServer"))
        {
                sender.Lock(udpMessage.ip, sender.port, true);
                sender.Send("ServerFound:" + serverName);
                sender.Unlock();
        }

        if (0 == udpMessage.message.IndexOf("Ping:"))
        {

            sender.Lock(udpMessage.ip, sender.port, true);
            sender.Send("Pong:" + udpMessage.message.Substring(5 ));
            sender.Unlock();

        }
    }





    public static string[] GetIpList()
    {
        string strHostName = Dns.GetHostName();
        IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

        int ipCount = ipEntry.AddressList.Length;
        string[] ipAdresses = new string[ipCount];
        for (int i = 0; i < ipCount; i++)
            ipAdresses[i] = ipEntry.AddressList[i].ToString();
        return ipAdresses;
    }

}
