using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Net;


public class UDP_LinkSenderToServer : MonoBehaviour, I_ServerConnector
{

    public string seekedServerName = "Unnamed";
    public string linkedIp;
    public UDP_Sender sender;
    public UDP_Receiver receiver;


    public Coroutine seekForServer;


    public Queue<UDP_Message> messageReceived = new Queue<UDP_Message>();

    public virtual void Start()
    {
        StartListenToReceiver();
        StartSeekingServer();
    }

    public virtual void Update()
    {

        while (messageReceived.Count >= 1)
            CheckForServerFound(messageReceived.Dequeue());
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
        UDP_Message msg = new UDP_Message() { message = _message, ip = _ip, port = _port };

        messageReceived.Enqueue(msg);
    }

    protected  void CheckForServerFound(UDP_Message udpMessage)
    {
        string[] tokens = udpMessage.message.Split(':');

        if (tokens.Length == 2 && tokens[0].Equals("ServerFound"))
        {
            if (tokens[1].CompareTo(seekedServerName) == 0)
            {
                linkedIp = udpMessage.ip;
                return;
            }
        }
      
    }

    public bool IsConnected()
    {
        return !string.IsNullOrEmpty(linkedIp);
    }

    public IEnumerator SeekForServerCoroutine()
    {

        while (!IsConnected())
        {

            string[] ips = UDP_Server.GetIpList();
            for (int i = 0; i < ips.Length; i++)
            {

                if (IsConnected()) break;
                string ipToCheck = ips[i];
                string[] ipToken = ipToCheck.Split('.');
                string ipZone = ipToken[0] + "." + ipToken[1] + "." + ipToken[2] + ".";
                for (int ipCount = 0; ipCount <= 255; ipCount++)
                {

                    if (IsConnected()) break;
                    sender.TargetClientToSend(ipZone + ipCount, sender.port, true);
                    sender.Send("SeekServer");
                    if (ipCount % 2 == 0) yield return new WaitForSeconds(0.03f);
                }

            }

        }
        sender.TargetClientToSend(linkedIp, sender.port, true);
    }



    public void StartSeekingServer()
    {

        if (seekForServer != null)
            StopCoroutine(seekForServer);
        linkedIp = "";
        seekForServer = StartCoroutine(SeekForServerCoroutine());
    }

    public void StopSeekingServer()
    {
        StopCoroutine(seekForServer);

    }




    public UDP_Sender GetSender()
    {
        return sender;
    }

    public UDP_Receiver GetReceiver()
    {
        return receiver;
    }

    public string GetServerNameWanted()
    {
     return   seekedServerName;
    }


    public void FoundServer()
    {
        StartSeekingServer();
    }
}

