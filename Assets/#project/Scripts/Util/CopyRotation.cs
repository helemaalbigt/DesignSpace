using UnityEngine;
using System.Collections;

public class CopyRotation : MonoBehaviour {

	public Transform _Target;
    public Vector3 _StartRotation;
    private Quaternion _StartrotationDiff;
	public bool _MaintainRelativeRotation = false;

	void Start(){
        if (_StartRotation == null)
        {
            _StartrotationDiff = transform.rotation * _Target.rotation;
        }
        else
        {
            _StartrotationDiff = Quaternion.Euler(_StartRotation);
        }
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
