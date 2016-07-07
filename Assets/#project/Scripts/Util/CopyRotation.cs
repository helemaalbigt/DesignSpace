using UnityEngine;
using System.Collections;

public class CopyRotation : MonoBehaviour {

	public Transform _Target;

	// Update is called once per frame
	void Update () {
		transform.rotation = _Target.rotation;
	}
}
