using UnityEngine;
using System.Collections;

public class MenuColliderControl : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		//Debug.Log ("enter");
		transform.gameObject.layer = LayerMask.NameToLayer ("Default");
	}

	void OnTriggerExit(Collider other){
		//Debug.Log ("exit");
		transform.gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
	}

}
