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
        SetPlayAreaToAnchor();
        CorrectPlayAreaRotation();
        CorrectPlayAreaPosition();
    }

    private void SetPlayAreaToAnchor()
    {
        _PlayArea.position = _PlayerStartAnchor.position;
        _PlayArea.rotation = _PlayerStartAnchor.rotation;
    }

    private void CorrectPlayAreaRotation()
    {
        _PlayArea.rotation = Quaternion.Euler(new Vector3(_PlayerStartAnchor.eulerAngles.x, _PlayerStartAnchor.eulerAngles.y - _Camera.localEulerAngles.y, _PlayerStartAnchor.eulerAngles.z));
    }

    private void CorrectPlayAreaPosition()
    {
        Vector3 CameraPosOnXZ = new Vector3(_Camera.position.x, 0f, _Camera.position.z);
        Vector3 StartAnchorOnXZ = new Vector3(_PlayerStartAnchor.position.x, 0f, _PlayerStartAnchor.position.z);
        Vector3 positionCorrection = CameraPosOnXZ - StartAnchorOnXZ;

        _PlayArea.Translate(-positionCorrection, UnityEngine.Space.World);
    }
}
