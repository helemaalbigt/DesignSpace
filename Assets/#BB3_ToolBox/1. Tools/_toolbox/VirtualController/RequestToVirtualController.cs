using UnityEngine;
using System.Collections;

public class RequestToVirtualController : MonoBehaviour {

    public VirtualController virtualController;
    public string request="";

    public void LoadRequest() 
    {
        if (virtualController != null)
            virtualController.SetRequestValue(request);
    }
}
