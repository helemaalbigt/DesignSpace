using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VirtualControllerAccessor : MonoBehaviour {

    public static VirtualControllerAccessor Instance;
    public static List<VirtualControllerAccessor> InstancesInScene = new List<VirtualControllerAccessor>();
    public string controllerName = "";
    public VirtualController Controller;
    public string description;
    public bool withDebug;

    public void SetController(string controllerName)
    {
        this.controllerName = controllerName;
        this.Controller = VirtualController.Get(controllerName);
    }

    void Start()
    {
        Instance = this;
        InstancesInScene.Add(this);
        SetController(controllerName);
    }
    void OnDestroy()
    {
        InstancesInScene.Remove(this);
    }


    void Update()
    {
        if (Controller != null)
        {
            description = Controller.ToString();
            if (withDebug)
                Debug.Log(description);
        }
    }
}
