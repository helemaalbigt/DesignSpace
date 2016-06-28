using UnityEngine;
using System.Collections;

public class GlobalMovement : MonoBehaviour {

	public Transform _ScaleWrapper;
	public Transform _MoveWrapper;
	public Transform _RotateWrapper;

	public WandController _WCL; //wandcontroller right
	public WandController _WCR; //wandcontroller left
	public WandController _ActiveController;

	private bool _SceneInFocus = true;
	private bool _SceneMovementEnabled = true;

	public enum MovementReference
	{
		controller,
		cursor
	};
	public MovementReference _MovementReference = MovementReference.cursor;

	private bool _SingleClickActive = false;
	private Vector3[] _ClickStartPos; //the scene and controller pos at start of first button down
	private Quaternion[] _ClickStartRot; //the scene and controller rot at start of first button down
	private Vector3 _ClickStartScale;//the scene startScale 

	private bool _DoubleClickActive = false;
	private Vector3[] _DoubleClickStartPos; //the scene and controller pos at start of first button down
	private Quaternion[] _DoubleClickStartRot; //the scene and controller rot at start of first button down
	private Vector3 _DoubleClickStartScale;//the scene startScale 


	// Use this for initialization
	void Start () {
		_ClickStartPos = new Vector3[5];
		_ClickStartRot = new Quaternion[3];

		_DoubleClickStartPos = new Vector3[5];
		_DoubleClickStartRot = new Quaternion[3];
	}
	
	// Update is called once per frame
	void Update () {
		
		if (_SceneInFocus && _SceneMovementEnabled) {

			UpdateStartState ();

			//Translate - Only one grip button pressed
			if (_WCR.gripPress ^ _WCL.gripPress && _SingleClickActive) {
				DoTranslate ();
			}

			//Rotate && Scale - Two buttons pressed
			if (_WCR.gripPress && _WCL.gripPress && _DoubleClickActive) {
				DoRotate ();
				DoScale ();
			}
		}

	}

	private void UpdateStartState(){
		//first button down start state
		if (_WCR.gripPress ^ _WCL.gripPress && !_SingleClickActive) 
		{
			_SingleClickActive = true;

			UpdateTransformState ();
		} 
		else if(!_WCR.gripPress && !_WCL.gripPress)
		{
			_SingleClickActive = false;
		}

		//double button down start state
		if (_WCR.gripPress && _WCL.gripPress && !_DoubleClickActive) 
		{
			_SingleClickActive = false;
			_DoubleClickActive = true;

			UpdateTransformState ();
		} 
		else if(!_WCR.gripPress || !_WCL.gripPress)
		{
			_DoubleClickActive = false;
		}
	}

	private void DoTranslate(){

		Vector3 movDelta; //distance diff the referencepoint has moved since press started
		switch (_MovementReference) {

		case MovementReference.controller:
			//movement paired directly to controller (=hand position)
			movDelta = _WCL.gripPress ?
				_ClickStartPos [1] - _WCL.transform.position:
				_ClickStartPos [2] - _WCR.transform.position;

			if(_WCR.triggerPress && _WCL.triggerPress){
				_MoveWrapper.localPosition = new Vector3 (
					_ClickStartPos [0].x,
					(_ClickStartPos [0] - movDelta).y,
					_ClickStartPos [0].z);
				
			} else if (_WCR.triggerPress ^ _WCL.triggerPress) {
				
				_MoveWrapper.localPosition = new Vector3 (
					(_ClickStartPos [0] - movDelta).x,
					_ClickStartPos [0].y,
					(_ClickStartPos [0] - movDelta).z);
				
			} else{
				_MoveWrapper.localPosition = _ClickStartPos [0] - movDelta;
			}

			break;

		case MovementReference.cursor:
			//movement paired to cursor movement; can cover bigger distances if cursor is further removed from hand
			movDelta = _WCL.gripPress ?
				_ClickStartPos [3] - _WCL._Cursor.position :
				_ClickStartPos [4] - _WCR._Cursor.position;

			if(_WCR.triggerPress && _WCL.triggerPress){
				
				_MoveWrapper.localPosition = new Vector3 (
					_ClickStartPos [0].x,
					(_ClickStartPos [0] - movDelta).y,
					_ClickStartPos [0].z);

			} else if (_WCR.triggerPress ^ _WCL.triggerPress) {
				
				_MoveWrapper.localPosition = new Vector3 (
					(_ClickStartPos [0] - movDelta).x,
					_ClickStartPos [0].y,
					(_ClickStartPos [0] - movDelta).z);
				
			} else {
				_MoveWrapper.localPosition = _ClickStartPos [0] - movDelta;
			}
				
			break;
		}

	}

	private void DoRotate(){
		//XZ translate model along Left Delta
		Vector3 movDeltaL = _ClickStartPos[3] - _WCL._Cursor.position;
		_MoveWrapper.localPosition = new Vector3 (
			(_ClickStartPos [0] - movDeltaL).x,
			_ClickStartPos [0].y,
			(_ClickStartPos [0] - movDeltaL).z);

		//Rotate model around left cursorpoint
		/*Vector3 vectorBetweenControllersStart = Vector3.ProjectOnPlane (_WCR._Cursor.position - _WCL._Cursor.position, new Vector3(0,1,0));
		Vector3 vectorBetweenControllersEnd = Vector3.ProjectOnPlane (_ClickStartPos[4] - _ClickStartPos[3], new Vector3(0,1,0)); //Vector3.Project
		Debug.DrawRay (_WCL._Cursor.position, vectorBetweenControllersStart);
		Debug.DrawRay (_WCL._Cursor.position, vectorBetweenControllersEnd);

		float angle = Vector3.Angle(vectorBetweenControllersStart, vectorBetweenControllersEnd);

		Debug.Log (Mathf.Deg2Rad*angle);
		_RotateWrapper.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
*/

		Vector3 relativePos = _ClickStartPos [3] - _WCR._Cursor.position;
		_MoveWrapper.rotation = Quaternion.LookRotation (relativePos, Vector3.up);

		//_RotateWrapper.RotateAround( _WCL._Cursor.position, new Vector3(0,1,0), Mathf.Deg2Rad*angle);
		//_RotateWrapper.rotation = Quaternion.FromToRotation(Vector3.ProjectOnPlane (_ClickStartPos[4] - _ClickStartPos[3], new Vector3(0,1,0)), Vector3.ProjectOnPlane (_WCR._Cursor.position - _WCL._Cursor.position, new Vector3(0,1,0)));

	}

	private void DoScale(){

	}

	public void UpdateTransformState(){
		_ClickStartPos[0] = _MoveWrapper.localPosition;
		_ClickStartPos[1] = _WCL.transform.position;
		_ClickStartPos[2] = _WCR.transform.position;
		_ClickStartPos[3] = _WCL._Cursor.position;
		_ClickStartPos[4] = _WCR._Cursor.position;

		_ClickStartRot[0] = _RotateWrapper.localRotation;
		_ClickStartRot[1] = _WCL.transform.rotation;
		_ClickStartRot[2] = _WCR.transform.rotation;

		_ClickStartScale = _ScaleWrapper.localScale;
	}
}
