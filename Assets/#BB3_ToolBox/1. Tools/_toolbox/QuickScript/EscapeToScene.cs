using UnityEngine;
using System.Collections;

public class EscapeToScene : MonoBehaviour {


    public LoadScene loadScene;
    public BetweenRange range= new BetweenRange(2,8);
    [Header("Debug")]
    public float pressingTime;
    void Update () {


        if (loadScene != null && (Input.GetKey(KeyCode.Escape) || Input.GetButton("Cancel")))
        {
            pressingTime += Time.deltaTime;
        }
        else if (loadScene != null && (Input.GetKeyUp(KeyCode.Escape) || Input.GetButtonUp("Cancel")))
        {
            if (range.IsInRange(pressingTime)) {
                pressingTime = 0;
                loadScene.LoadSelectedSceneWithDelay();
                Destroy(this);
                return;

            }

        }
        else { pressingTime = 0; }
	}
}
