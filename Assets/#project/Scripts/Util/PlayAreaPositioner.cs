using UnityEngine;
using System.Collections;

public class PlayAreaPositioner : MonoBehaviour {

	public Transform _PlayArea;
	public Transform _Camera;
	public Transform _PlayerStartAnchor;

	// Use this for initialization
	void Start () {
		Invoke ("ResetPlayAreaPosition", 0.1f);
	}

	public void ResetPlayAreaPosition(){
		Vector3 positionCorrection = _PlayerStartAnchor.position - _Camera.position;
		positionCorrection = new Vector3 (positionCorrection.x, 0, positionCorrection.z);
		_PlayArea.Translate(positionCorrection);
	}
}
