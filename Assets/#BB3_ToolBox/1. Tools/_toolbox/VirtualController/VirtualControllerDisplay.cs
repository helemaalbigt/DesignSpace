using UnityEngine;
using System.Collections;

public class VirtualControllerDisplay : MonoBehaviour {


    public VirtualController controller;
    public string description;
    public bool withDebug;

    void Update() {
        if (controller != null)
        {
            description = controller.ToString();
            if(withDebug)
            Debug.Log(description);
        }
    }

}
