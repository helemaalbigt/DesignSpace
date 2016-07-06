using UnityEngine;
using System.Collections;

public class AxisController : MonoBehaviour {

	public Transform _GlobalWrapper;
	
	// Update is called once per frame
	void Update () {
		transform.rotation = _GlobalWrapper.rotation;
	}

	public void HoverOn(WandController WC){
		if (WC.triggerDown)
		{
			WC.Vibrate (2000);
			_GlobalWrapper.rotation = Quaternion.identity;
		}

		if (WC.doubleClick)
		{
			WC.Vibrate (2000);
		}
	}
}
