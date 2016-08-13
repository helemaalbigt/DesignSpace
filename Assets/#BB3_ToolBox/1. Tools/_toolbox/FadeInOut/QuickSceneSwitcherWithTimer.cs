using UnityEngine;
using System.Collections;

public class QuickSceneSwitcherWithTimer : QuickSceneSwitcher {

    public float nextSceneTimer = 5f;
	// Update is called once per frame
	protected override void Update () {
        base.Update();

        nextSceneTimer -= Time.deltaTime;
        if (nextSceneTimer < 0) {
            nextSceneTimer = 0;
            CallNextSceneWithDelay();
        }

	    
	}
}
