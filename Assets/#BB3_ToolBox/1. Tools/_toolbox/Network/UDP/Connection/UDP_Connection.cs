using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Net;

public class UDP_Connection : UDP_Server, I_ServerConnector {

    public string seekingName="Unnamed";
    public string linkedIp;

    public Coroutine seekForServer;

	public override void Start () {

        base.Start();
        StartSeekingServer();
	}

    public override void Update()
    {
        base.Update();
    }

    protected override void CheckForSeeker(UDP_Message udpMessage)
    {
        base.CheckForSeeker(udpMessage);
        string[] tokens = udpMessage.message.Split(':');

        if (tokens.Length == 2 && tokens[0].Equals("ServerFound"))
        {
            if (tokens[1].CompareTo(seekingName)==0) { 
            linkedIp = udpMessage.ip;
            return;
            }
        }
            

        





    }

    public bool IsConnected() {
        return !string.IsNullOrEmpty(linkedIp);
    }

        public IEnumerator SeekForServerCoroutine()
        {

            while(!IsConnected()){
            
            string [] ips = GetIpList();
            for (int i = 0; i < ips.Length; i++) {

                     if (IsConnected()) break;
                    string ipToCheck = ips[i];
                    string[] ipToken = ipToCheck.Split('.');
                    string ipZone = ipToken[0] + "." + ipToken[1] + "." + ipToken[2] + ".";
                    for (int ipCount = 0; ipCount <= 255; ipCount++) {

                        if (IsConnected()) break;
                        sender.TargetClientToSend(ipZone + ipCount, sender.port, true);
                        sender.Send("SeekServer");
                        if (ipCount % 2 == 0) yield return new WaitForSeconds(0.03f);
                    }

                }
            
            }
            sender.TargetClientToSend(linkedIp, sender.port, true);
        }

        

        public void StartSeekingServer() {

            if (seekForServer != null)
                StopCoroutine(seekForServer);
            linkedIp = "";
            seekForServer = StartCoroutine(SeekForServerCoroutine());
        }

        public void StopSeekingServer() {
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
            return seekingName;
        }
        public void FoundServer()
        {
            StartSeekingServer();
        }
}


