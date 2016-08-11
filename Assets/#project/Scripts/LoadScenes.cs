using UnityEngine;
using System.Collections;

public class LoadScenes : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		if (InputController.controllers[0].menuDown)
		{
			Application.LoadLevel("Main");
		}

		if (InputController.controllers[1].menuDown)
		{
			Application.LoadLevel("Model");
		}
	}
}
