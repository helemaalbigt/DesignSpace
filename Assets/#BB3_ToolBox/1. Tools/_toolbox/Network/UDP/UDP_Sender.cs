using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDP_Sender : MonoBehaviour {

    private static int localPort;
    IPEndPoint remoteEndPoint;
    UdpClient client;

    public string defaultIp = "127.0.0.1";
    public int defaultPort = 2501;
    public bool hasMultipleReceiver = true;
    public bool withAutoStart = true;

    public string ip = "127.0.0.1";
    public int port = 2501;

    public delegate void OnTargetDefined(UDP_Sender sender, string ip, int port);
    public delegate void OnMessageSent(UDP_Sender sender, string message);
    public delegate void OnStopTargeting(UDP_Sender sender);

    public OnTargetDefined onTargetDefined;
    public OnMessageSent onMessageSent;
    public OnStopTargeting onStopTargeting;

    public bool ipLocked;

    public BytesCounter bytes;

    public void Start()
    {
        if (withAutoStart)
            TargetClientToSend(defaultIp, defaultPort, hasMultipleReceiver);

        InvokeRepeating("RefreshBytesCount", 0f, 1f);
    }
    public void RefreshBytesCount() { bytes.SecondHasPasted(); }

    public void Lock(string ip, int port, bool hasMultipleReceiver=true) {
        TargetClientToSend(ip, port, hasMultipleReceiver, true);
    }
    public void TargetClientToSend(string ip, int port, bool hasMultipleReceiver=true, bool lockIt = false)
    {

        if (ipLocked)
            return;
        if (lockIt)
            ipLocked = true;
        

        if (string.IsNullOrEmpty(ip))
            this.ip = defaultIp;
        else this.ip = ip;
        if (port <= 0)
            this.port = defaultPort;
        else this.port = port;
        //Set who will received
        if (remoteEndPoint == null)
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(this.ip), this.port);
        else {
            remoteEndPoint.Address = IPAddress.Parse(this.ip);
            remoteEndPoint.Port = this.port;
        }

        //Define a udp client
        if(client==null)
            client = new UdpClient();
        if (hasMultipleReceiver)
            client.EnableBroadcast = true;

        //Warn the listener of this class
        if (onTargetDefined != null)
            onTargetDefined(this, this.ip, this.port);
    }

  
    public void Send(string message)
    {
        if (!IsTargetDefine()) return;
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            bytes.Add(data.Length);
            client.Send(data, data.Length, remoteEndPoint);
            if (onMessageSent != null)
                onMessageSent(this, message);
        }
        catch (Exception err)
        {
            Debug.LogWarning(err.ToString(),this.gameObject);
        }
    }

    private bool IsTargetDefine()
    {
        return client != null && remoteEndPoint != null;
    }

    void OnDestroy()
    {
        StopTargetingClient();
    }
    void OnApplicationQuit()
    {
        StopTargetingClient();
    }

    public void StopTargetingClient()
    {
        if (remoteEndPoint == null && client == null) return;

        if (remoteEndPoint != null) {
            remoteEndPoint = null;
        }
        if (client != null) { 
            client.Close();
            client = null;
        }
        if (onStopTargeting != null)
            onStopTargeting(this);
    }



    internal bool HasTargetClient()
    {
        return client != null;
    }

    public string GetTargetAdresse() {
        if (remoteEndPoint == null) return "";
        return remoteEndPoint.Address.ToString();
       
    }
    public int GetTargetPort() {
        if (remoteEndPoint == null) return 0;
       return remoteEndPoint.Port ;
    
    }


    public void Unlock()
    {
        ipLocked = false;
    }
}
