using UnityEngine;
using System.Collections;

public class LAN_Control_Data : MonoBehaviour
{

    public static LAN_Control_Data Instance;
    public string currentIpAddress;
    public string targetIpAddress;
    public int targetPortAddress = 8061;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
        SetAsDefault();
    }

    private void SetAsDefault()
    {

        currentIpAddress = Network.player.ipAddress;
        targetIpAddress = currentIpAddress;
        targetPortAddress = 8061;
    }

    public void Reset() { 
    
    }

    public void SetIpTargeted(string address, int port)
    {
        targetPortAddress = port;
        targetIpAddress = address;
    }
    public string GetTargetAddress() { return targetIpAddress; }
    public int GetTargetPort() { return targetPortAddress; }
    public string GetCurrentAddress() { return currentIpAddress = Network.player.ipAddress; }


}
