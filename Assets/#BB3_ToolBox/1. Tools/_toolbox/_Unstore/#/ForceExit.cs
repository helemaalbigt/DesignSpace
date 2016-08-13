using UnityEngine;
using System.Collections;

public class ForceExit : MonoBehaviour {


    public LoadScene sceneLoader;
    public float timeToForceExit = 3f;

    [Header("Debug")]
    public float forceExitTime;
    public void Update() {

        if (Input.GetKey(KeyCode.Escape)) {
            forceExitTime += Time.deltaTime;
            if (timeToForceExit < forceExitTime)
            {
                forceExitTime = 0;
                sceneLoader.LoadSelectedSceneWithDelay();
                Destroy(this);
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            forceExitTime = 0f;
        }


    }

}
