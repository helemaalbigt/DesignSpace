using UnityEngine;
using System.Collections;

public class FaceTarget : MonoBehaviour {

	public Transform _Target;

	public bool _Flip = true;
	public bool _FreezeX = false;
	public bool _FreezeY = false;
	public bool _FreezeZ = false;
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(_Target.position);

		float rotX = _FreezeX ? 0 : transform.localRotation.eulerAngles.x;
		float rotY = _FreezeY ? 0 : transform.localRotation.eulerAngles.y;
		if (_Flip)
			rotY += 180f;
		float rotZ = _FreezeZ ? 0 : transform.localRotation.eulerAngles.z;

		transform.localRotation = Quaternion.Euler(new Vector3 (rotX, rotY, rotZ));
	}
}
