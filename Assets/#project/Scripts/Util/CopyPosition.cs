using UnityEngine;
using System.Collections;

public class CopyPosition : MonoBehaviour {

	public Transform _Anchor;
    public Vector3 _StartPosition;

	private Vector3 _StartDiff;

	void Start(){
        if (_StartPosition == null)
        {
            _StartDiff = _Anchor.position - transform.position;
        }
        else
        {
            _StartDiff = transform.TransformPoint(_StartPosition);
        }
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = _Anchor.transform.position - _StartDiff;
	}
}
