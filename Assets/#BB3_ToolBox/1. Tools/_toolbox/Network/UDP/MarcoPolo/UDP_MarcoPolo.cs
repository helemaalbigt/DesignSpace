using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

[System.Obsolete("User UDP_Server")]
public class UDP_MarcoPolo : MonoBehaviour {

    public string ReceiverIp { get { return udpReceiver.GetLocalIp(); } }
    public int ReceiverPort { get { return udpReceiver.GetListenedPort(); } }
    public UDP_Sender udpSender;
    public UDP_Receiver udpReceiver;
    public enum UserType { Seeker, Server}
    public UserType userType;

    public int minIpRange=0, maxIpRange = 255;
    public int nextIp;
    public float GetPourcent { get { return Mathf.Clamp((float)nextIp / (float)maxIpRange, 0f, 1f); } }
    public delegate void OnUserDetected(string userIp, int portToReply);
    public delegate void OnServerDetected(string userIp, int portToReply);

    public OnUserDetected onUserDetected;
    public OnServerDetected onServerDetected;

    public bool withServerCallAtStart=true;
    public bool stopSeekingWhenServerFound=true;

    //The Receiver thread can't execute normal thread methode . So the queue is use to make the transition between.
    public Queue<string> messageReceived = new Queue<string>();

	void Start () {
        StartListenToReceiver();
        if (withServerCallAtStart)
            LaunchCoroutineLookForServer();
	}

    void Update() {

        if (messageReceived.Count >= 1)
            MessageToCommand(messageReceived.Dequeue());
    }
    private void StartListenToReceiver()
    {
        udpReceiver.onPackageReceivedParallelThread += MarcoPoloDetection;
      
    }

    public void OnDestroy() {
        udpReceiver.onPackageReceivedParallelThread -= MarcoPoloDetection;
       
    }

    private void MarcoPoloDetection(UDP_Receiver from, string message, string ip, int port)
    {
        messageReceived.Enqueue(message);
    }
    private void MessageToCommand(string message){
 //           Debug.Log("MSG: " + message);
        string [] tokens = message.Split(':');
        if (tokens.Length < 1) return;
        string request = tokens[0];
        if (tokens.Length == 3 && (request.Equals("Marco") || request.Equals("Polo"))) 
        {
            Debug.Log("Processing: " + tokens[1]);
        
            string ip = tokens[1];
            int port;
            int.TryParse(tokens[2], out port);
            if (port > 0 && ! string.IsNullOrEmpty(ip)) {
 
                if (userType == UserType.Server && request.Equals("Marco")) {
                    Debug.Log("Server : Marco detected");
                    SendUserReply_Polo(ip, port);
                    if (onUserDetected != null) {
                        onUserDetected(ip, port);
                    }

                }
                else if (userType == UserType.Seeker && request.Equals("Polo"))
                {
                    Debug.Log(string.Format("Server Found: {0} : {1} ", ip, port));
                    if (onServerDetected != null)
                    {
                        onServerDetected(ip, port);
                    }
                    if (stopSeekingWhenServerFound)
                        ServerFound();
            
                }
            }
        }

    }

    private void SendUserReply_Polo(string ip, int port)
    {
        if (!udpSender) return;
        udpSender.TargetClientToSend(ip, port, true);
        udpSender.Send(string.Format("Polo:{0}:{1}", this.ReceiverIp, this.ReceiverPort));
    }

        public IEnumerator SendServerCall_Marco(string ip, int port)
        {

            if (!udpSender) yield return null;
            string[] ipToken = ip.Split('.');
            if (ipToken.Length != 4) yield return null;
            string ipZone = ipToken[0] + "." + ipToken[1] + "." + ipToken[2] + ".";
            //for (int i = minIpRange; i <= maxIpRange; i++)
            int nextIp =minIpRange;
            while(true)
            {
                for (int x = 0; x < 3; x++) { 
                    nextIp = nextIp++ % maxIpRange + 1;
                
     //               Debug.Log("ip:"+ipZone+i +"from ip :"+ReceiverIp);
                    udpSender.TargetClientToSend(ipZone + nextIp, port, true);
                    udpSender.Send(string.Format("Marco:{0}:{1}", ip, port));
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        public void LaunchCoroutineLookForServer() {
            StopAllCoroutines();
            StartCoroutine( SendServerCall_Marco(ReceiverIp, ReceiverPort));
        }

        public void ServerFound() {
            StopAllCoroutines();
        
        }
	
}
