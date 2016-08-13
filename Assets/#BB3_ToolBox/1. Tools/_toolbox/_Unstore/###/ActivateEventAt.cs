using UnityEngine;
using System.Collections;

public class ActivateEventAt : MonoBehaviour {

    public EventActivator activator;
    
    public bool atLevelLoad;
    public bool atAwake;
    public bool atStart;
    public bool atEnable;
    public bool atDisable;
    public bool atDestroy;
    void Start()
    {
        if (atStart && activator)
            activator.Activate();
    }
    void Awake()
    {
        if (atAwake && activator)
            activator.Activate();
    }

    void OnLevelWasLoaded(int level)
    {

        if (atLevelLoad && activator)
            activator.Activate();
    }
    void OnEnable()
    {

        if (atEnable && activator)
            activator.Activate();
    }
    void OnDisable()
    {

        if (atDisable && activator)
            activator.Activate();
    }
    void OnDestroy() {

        if (atDestroy && activator)
            activator.Activate();
    }
}
