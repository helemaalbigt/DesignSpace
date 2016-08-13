using UnityEngine;
using System.Collections;

public class EventActivator : MonoBehaviour {


    public GameObject[] objectToDeactivate;
    public GameObject[] objectToActivate;
    public GameObject[] objectToDestroy;

    public MonoBehaviour[] scriptToDisable;
    public MonoBehaviour[] scriptToActivate;
    public MonoBehaviour[] scriptToDestroy;

    [Tooltip("Will destroy this script after being used")]
    public bool autoDestroy;

    public void Activate()
    {
        foreach (GameObject obj in objectToDeactivate)
            if (obj != null)
                obj.SetActive(false);


        foreach (GameObject obj in objectToActivate)
            if(obj!=null)
            obj.SetActive(true);

        foreach (GameObject obj in objectToDestroy)
            if (obj != null) 
                Destroy(obj);
        foreach (MonoBehaviour script in scriptToDisable)
            if (script != null)
                script.enabled = false;

        foreach (MonoBehaviour script in scriptToActivate)
            if (script != null) 
                script.enabled = true;


        foreach (MonoBehaviour script in scriptToDestroy)
            if (script != null)
                Destroy(script);
        if(autoDestroy)
        Destroy(this);
    }
}
