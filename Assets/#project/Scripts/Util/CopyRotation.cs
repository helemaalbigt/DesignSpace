using UnityEngine;
using System.Collections;

public class CopyRotation : MonoBehaviour {

	public Transform _Target;
	public Quaternion _StartrotationDiff;
	public bool _MaintainRelativeRotation = false;

	void Start(){
		_StartrotationDiff = transform.rotation * _Target.rotation;
	}

	// Update is called once per frame
	void Update () {
		if (_MaintainRelativeRotation)
		{
			transform.rotation = _Target.rotation * _StartrotationDiff;
		} else
		{
			transform.rotation = _Target.rotation;
		}
	}
}
