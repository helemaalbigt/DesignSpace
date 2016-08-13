using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VirtualControllerViewer : MonoBehaviour
{


    public string controllerName;
    public VirtualController controller;
    public Text textDisplayer;
    public string description;
    public bool withDebug;

    public void SetController(VirtualController controller) { this.controller = controller; }
    public void Start() {
        SetController(VirtualController.Get(controllerName));
    }

    void Update()
    {
        if (controller != null)
        {
            description = controller.ToString();
            if (textDisplayer)
                textDisplayer.text = description;
            if (withDebug)
                Debug.Log(description);
        }
    }

}
