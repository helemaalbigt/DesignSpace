using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UDPReceiverToText : MonoBehaviour
{

    public UDP_Receiver receiver;
    public Text tDisplayValue;
    public Text tDisplayAll;
    public string toDisplay;
    public string toDisplayAll;

    public void Start()
    {
        receiver.onPackageReceivedParallelThread += RefreshReceived;

    }
    public void RefreshReceived(UDP_Receiver from, string message, string adresse, int port)
    {
        toDisplay =  message;
        toDisplayAll = from.allReceivedUDPPackages;

    }
    public void Update()
    {
        if(tDisplayValue)
            tDisplayValue.text = "In: " + toDisplay;

        if (tDisplayAll)
            tDisplayAll.text = "All: " + toDisplayAll;

    }
}