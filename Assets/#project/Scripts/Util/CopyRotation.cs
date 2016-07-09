using UnityEngine;
using System.Collections;

public class CopyRotation : MonoBehaviour {

	public Transform _Target;
	public Quaternion _Startrotation;
	public bool _MaintainRelativeRoation = false;

	void Start(){

	}

	// Update is called once per frame
	void Update () {
		if (_MaintainRelativeRoation)
		{
		} else
		{
			transform.rotation = _Target.rotation;
		}
	}
}
