using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WandController : MonoBehaviour {

	//reference to controller - index can change session to session - hmd is always index 0
	private SteamVR_Controller.Device _Controller { get { return SteamVR_Controller.Input ((int)_TrackedObject.index); }} //reference to controller
	private SteamVR_TrackedObject _TrackedObject;

	private Valve.VR.EVRButtonId _GripButton = Valve.VR.EVRButtonId.k_EButton_Grip; //buttoninex - check WandController.cs script to see names of buttons
	private Valve.VR.EVRButtonId _TriggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private Valve.VR.EVRButtonId _TouchButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	private Valve.VR.EVRButtonId _MenuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
	private Valve.VR.EVRButtonId _SystemButton = Valve.VR.EVRButtonId.k_EButton_System;

	public static List<WandController> controllers = new List<WandController>();

	public bool inUse = false;

	[Header("POINTER PARAMS")]
	public float _RayRange;
	public Transform _PointerAnchor;
	public CursorController cursor;

	public float _MaxDoubleClickTime = 500f;
	public bool doubleClick = false;
	private float _LastClickTimeStamp = -99999f;

	private Ray _Ray;
	private RaycastHit _Hit;

	[Header("POINTER READOUT")]
	public bool rayHit;
	public bool rayHitModel;
	public string hitTag;
	public int hitLayer;
	public float hitDistance;
	public GameObject hitObj;
	public Vector3 hitPos;
	public Vector3 hitPosPrev;
	public Vector3 hitNorm;
	public Vector3 hitNormPrev;

	public Vector3 pointerPos;		//location of pointer anchor
	public Vector3 pointerPosPrev; 	//location of pointer anchor in previous frame

	[Header("BUTTON READOUT")]
	public bool gripDown = false;
	public bool gripUp = false;
	public bool gripPress = false;

	public bool triggerDown = false;
	public bool triggerUp = false;
	public bool triggerPress = false;
	public float triggerAxis;
	[Space(15)]
	public bool touchHoverDown = false;
	public bool touchHoverUp = false;
	public bool touchHover = false;
	[Space(5)]
	public bool touchDown = false;  //TOUCHDOWN!!!!!!!!!!!!!!
	public bool touchUp = false;
	public bool touchPress = false;
	[Space(5)]
	public Vector2 touchAxis;
	public Vector2 touchAngle;
	public float touchRadius;

	[Space(15)]
	public bool menuDown = false;
	public bool menuUp = false;
	public bool menuPress = false;

	//Get the controller instance
	void Start () {
		_TrackedObject = GetComponent<SteamVR_TrackedObject> ();
		WandController.controllers.Add (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (_Controller == null) {
			Debug.Log ("Controller not initialized");
			return;
		}
			
		UpdateButtons ();
		UpdatePointer ();
	}	

	private void UpdateButtons(){
		//check buttons
		gripDown = _Controller.GetPressDown (_GripButton) ? true : false;
		gripUp = _Controller.GetPressUp (_GripButton) ? true : false;
		gripPress = _Controller.GetPress (_GripButton) ? true : false;

		triggerDown = _Controller.GetPressDown (_TriggerButton) ? true : false;
		triggerUp = _Controller.GetPressUp (_TriggerButton) ? true : false;
		triggerPress = _Controller.GetPress (_TriggerButton) ? true : false;
		triggerAxis = _Controller.GetAxis(_TriggerButton).x;

		doubleClick = _Controller.GetPressDown (_TriggerButton) && Time.time - _LastClickTimeStamp <= _MaxDoubleClickTime ? true : false;
		if (_Controller.GetPressDown (_TriggerButton))
		{
			_LastClickTimeStamp = Time.time;
		}

		touchHoverDown = _Controller.GetTouchDown (_TouchButton) ? true : false;
		touchHoverUp = _Controller.GetTouchUp (_TouchButton) ? true : false;
		touchHover = _Controller.GetTouch (_TouchButton) ? true : false;
		touchDown = _Controller.GetPressDown (_TouchButton) ? true : false;
		touchUp = _Controller.GetPressUp (_TouchButton) ? true : false;
		touchPress = _Controller.GetPress (_TouchButton) ? true : false;
		touchAxis = _Controller.GetAxis(_TouchButton);
		touchAngle.x = Mathf.Atan2( _Controller.GetAxis (_TouchButton).y, _Controller.GetAxis (_TouchButton).x);
		touchAngle.y = touchAngle.x * Mathf.Rad2Deg;
		touchRadius = Mathf.Sqrt ( Mathf.Pow(touchAxis.x,2) + Mathf.Pow(touchAxis.y,2));

		menuDown = _Controller.GetPressDown (_MenuButton) ? true : false;
		menuUp = _Controller.GetPressUp (_MenuButton) ? true : false;
		menuPress = _Controller.GetPress (_MenuButton) ? true : false;
	}

	private void UpdatePointer (){

		pointerPosPrev = pointerPos;
		pointerPos = _PointerAnchor.position;

		hitPosPrev = hitPos;
		hitNormPrev = hitNorm;

		_Ray.origin = _PointerAnchor.position;
		_Ray.direction = _PointerAnchor.forward;

		//if nothing hit, hit the grid
		if (Physics.Raycast (_Ray, out _Hit, _RayRange)) {
			rayHit = true;
			hitTag = _Hit.transform.tag;
			hitLayer = _Hit.transform.gameObject.layer;
			hitObj = _Hit.transform.gameObject;
			hitPos = _Hit.point;
			hitNorm = _Hit.normal;
			hitDistance = _Hit.distance;
			rayHitModel = hitLayer == LayerMask.NameToLayer ("Model") ? true : false;

			_Hit.transform.gameObject.SendMessage ("HoverOn", this, SendMessageOptions.RequireReceiver);
		} else {
			rayHit = false;
		}
			
	}

	private void OnTriggerEnter(Collider collider){
		
	}

	private void OnTriggerExit(Collider collider){

	}

	public void Vibrate(ushort dur){
		_Controller.TriggerHapticPulse (dur);
	}
}
