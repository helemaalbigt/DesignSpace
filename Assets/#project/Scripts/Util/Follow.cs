using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform _Anchor;

	private Vector3 _StartDiff;

	void Start(){
		_StartDiff = _Anchor.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = _Anchor.transform.position - _StartDiff;
	}
}
