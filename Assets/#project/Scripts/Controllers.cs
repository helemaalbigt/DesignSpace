/*
 * Keep track of state of both controllers (none active = 1, 1 active = 1, both active = 2)
 */

using UnityEngine;
using Valve.VR;

public class Controllers : MonoBehaviour {

	/*[Header("WandController")]
	public SteamVR_TrackedObject _TrackedObjectLeft;
	private SteamVR_Controller.Device _ControllerLeft { get { return SteamVR_Controller.Input ((int)_TrackedObjectLeft.index); }}
	public SteamVR_TrackedObject _TrackedObjectRight;
	private SteamVR_Controller.Device _ControllerRight { get { return SteamVR_Controller.Input ((int)_TrackedObjectRight.index); }}

	private Valve.VR.EVRButtonId _GripButton = Valve.VR.EVRButtonId.k_EButton_Grip; //buttoninex - check WandController.cs script to see names of buttons
	private Valve.VR.EVRButtonId _TriggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private Valve.VR.EVRButtonId _TouchButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	private Valve.VR.EVRButtonId _MenuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
	private Valve.VR.EVRButtonId _SystemButton = Valve.VR.EVRButtonId.k_EButton_System;

	[Header("ButtonReadout")]


	[Header("POINTER PARAMS")]
	public float _RayRange;
	public Transform _PointerAnchor;
	public Transform _Cursor;

	public float _MaxDoubleClickTime = 500f;
	public bool doubleClick = false;
	private float _LastClickTimeStamp = -99999f;

	private Ray _RayL;
	private RaycastHit _HitL;
	private Ray _RayR;
	private RaycastHit _HitR;*/

	public WandController left, right;

	[Header("STATE READOUT")]
	public bool grip0 = false;
	public bool grip1 = false;
	public bool grip2 = false;
	public WandController gripPrim = null;
	public WandController gripSec = null;

	[Space(15)]
	public bool trigger0 = false;
	public bool trigger1 = false;
	public bool trigger2 = false;
	public WandController triggerPrim = null;
	public WandController triggerSec = null;

	[Space(15)]
	public bool touchHover0 = false;
	public bool touchHover1 = false;
	public bool touchHover2 = false;
	public WandController touchHoverPrim = null;
	public WandController touchHoverSec = null;

	[Space(5)]
	public bool touchPress0 = false;  //TOUCHDOWN!!!!!!!!!!!!!!
	public bool touchPress1 = false;
	public bool touchPress2 = false;
	public WandController touchPressPrim = null;
	public WandController touchPressSec = null;

	[Space(15)]
	public bool menu0 = false;
	public bool menu1 = false;
	public bool menu2 = false;
	
	// Update is called once per frame
	void Update () {
		if (left.gripPress ^ right.gripPress)
		{
			grip1 = true;
			gripPrim = left.gripPress ? left : right;
			gripSec = left.gripPress ? right : left;
		} else if (left.gripPress && right.gripPress)
		{
			grip2 = true;
		} else
		{
			grip0 = true;
			gripPrim = gripSec = null;
		}
	}
}
