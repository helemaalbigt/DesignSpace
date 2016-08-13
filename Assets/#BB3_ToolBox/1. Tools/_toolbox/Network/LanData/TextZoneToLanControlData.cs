using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextZoneToLanControlData : MonoBehaviour {

    public string lastIpAddress = "";
    public int lastPort ;
    public Text[] ip_texts;
    public Text   port_text;

    void Update () {

        if (port_text != null)
        {
            int currentPort = int.Parse(port_text.text);
            if (currentPort != lastPort)
            {
                if (LAN_Control_Data.Instance != null) { 
                    LAN_Control_Data.Instance.SetIpTargeted(lastIpAddress, currentPort);
                    lastPort = currentPort;
                }
            }
        }

        string ip = GetIp();
        if (! string.IsNullOrEmpty(ip) && ! ip.Equals(lastIpAddress))
        {
            if (LAN_Control_Data.Instance != null)
            {
                LAN_Control_Data.Instance.SetIpTargeted(ip, lastPort);
                lastIpAddress = ip;
            }
            
        }
	}

    private string GetIp()
    {
        
        if(ip_texts!=null && ip_texts.Length==4)
            return ip_texts[0].text + "." + ip_texts[1].text + "." + ip_texts[2].text + "." + ip_texts[3].text;
        return "";
    }

    internal void SetTextIPTo(string userIp, int port)
    {
        if (string.IsNullOrEmpty(userIp)) return;
        string[] tokens = userIp.Split('.');
        if (tokens.Length != 4) return;
        ip_texts[0].text = tokens[0];
        ip_texts[1].text = tokens[1];
        ip_texts[2].text = tokens[2];
        ip_texts[3].text = tokens[3];
        port_text.text = "" + port;
    }
}
