using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

public class UDP_Receiver : MonoBehaviour
{

    Thread receiveThread;
    UdpClient client;

    public int listenedPort = 2501;
    public bool withAutoStart = true;

    public string lastReceivedUDPPackage = "";
    public string allReceivedUDPPackages = ""; // clean up this from time to time!
    private int maxLenghtOfAllPackagesRecorded = 10000;

    public delegate void OnStartListening(UDP_Receiver who, int port);
    public delegate void OnReceivedUDPPackage(UDP_Receiver from, string message, string adresse, int port);
    public delegate void OnPackagesClear(UDP_Receiver from, string allMessage);
    public delegate void OnStopListening(UDP_Receiver who);

    public OnStartListening onStartListening;
    /// <summary>
    ///Warning: As the receiver work on a parallel thread, listen to this  delegate can cause error if used int Unity main thread.
    /// If you really need to use the data when it arrive use it or use onPackageReceived instead.
    /// </summary>
    public OnReceivedUDPPackage onPackageReceivedParallelThread;
    public OnPackagesClear onPackagesClear;
    public OnStopListening onStopListening;
    public BytesCounter bytes;


    public OnReceivedUDPPackage onPackageReceived;
    private Queue<UDPPackageReceived> receivedPackages = new Queue<UDPPackageReceived>();
    public struct UDPPackageReceived {

        public UDPPackageReceived(UDP_Receiver _receiver, string _message, string _address, int _port) {
            receiver = _receiver;
            message = _message;
            addresse = _address;
            port = _port;
        }
        public UDP_Receiver receiver;
        public string message;
        public string addresse;
        public int port;
    }

    public void Start()
    {
        if (withAutoStart)
            StartListening();
        InvokeRepeating("RefreshBytesCount", 0f, 1f);
    }

    public void Update()
    {
        UDPPackageReceived pack;
        while(receivedPackages.Count>0)
        {
            pack = receivedPackages.Dequeue();
            if (onPackageReceived != null)
                onPackageReceived(pack.receiver, pack.message, pack.addresse, pack.port);
        }

    }
    public void RefreshBytesCount() { bytes.SecondHasPasted(); }
    public void SetPortListened(int port) {
        if (port > 0)
            listenedPort = port;
    }
    
    
    public void StartListening(int port)
    {
        SetPortListened(port);
        StartListening();
    }

    public void StartListening()
    {
        if (IsListening()) return;

        receiveThread = new Thread(
            new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

        if (onStartListening != null)
            onStartListening(this, listenedPort);
    }

    private bool IsListening()
    {
        return receiveThread != null && receiveThread.IsAlive;
    }

    private void ReceiveData()
    {

        client = new UdpClient(listenedPort);
        while (true)
        {

            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                bytes.Add(data.Length);
                string text = Encoding.UTF8.GetString(data);
                lastReceivedUDPPackage = text;
                allReceivedUDPPackages = allReceivedUDPPackages + text;

                if (string.IsNullOrEmpty(text))
                    Debug.LogWarning("What the fuck ????");
                if (onPackageReceived != null)
                    receivedPackages.Enqueue(new UDPPackageReceived(this, text, anyIP.Address.ToString(), anyIP.Port));
                if (onPackageReceivedParallelThread != null)
                    onPackageReceivedParallelThread(this, text, anyIP.Address.ToString(), anyIP.Port);
                if (allReceivedUDPPackages.Length > maxLenghtOfAllPackagesRecorded)
                    ClearPackagesRecorded();

            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    public void ClearPackagesRecorded()
    {
        if (onPackagesClear != null)
            onPackagesClear(this, allReceivedUDPPackages);
        allReceivedUDPPackages = "";
    }

    public string GetLastPackage()
    {
        return lastReceivedUDPPackage;
    }

    void OnDestroy()
    {
        StopListening();
    }
    void OnApplicationQuit()
    {
        StopListening();
    }

    public void StopListening()
    {
        if (!IsListening()) return;
        if (receiveThread != null) { 
             receiveThread.Abort();
             receiveThread = null;
        }

        if (client != null)
        {
            client.Close();
            client = null;
        }
        if (onStopListening != null)
            onStopListening(this);
    }


    internal int GetListenedPort()
    {
        return listenedPort;
    }

    internal string GetLocalIp()
    {
        return Network.player.ipAddress;
    }
    internal string GetExternalIp()
    {
        return Network.player.externalIP;
    }
}
