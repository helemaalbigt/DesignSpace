//General: index 0 = left, index 1 = right
using UnityEngine;
using System;

public class InputControllerOld : MonoBehaviour {
	
	//Controller references
	public SteamVR_TrackedObject _TrackedObjectLeft;
	public SteamVR_TrackedObject _TrackedObjectRight;
	private SteamVR_Controller.Device _CL { get { return SteamVR_Controller.Input ((int)_TrackedObjectLeft.index); }} //reference to left controller
	private SteamVR_Controller.Device _CR { get { return SteamVR_Controller.Input ((int)_TrackedObjectRight.index); }} //reference to right controller
	private SteamVR_Controller.Device[] _Controllers;

	private Valve.VR.EVRButtonId _GripButton = Valve.VR.EVRButtonId.k_EButton_Grip; //buttoninex - check WandController.cs script to see names of buttons
	private Valve.VR.EVRButtonId _TriggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private Valve.VR.EVRButtonId _TouchButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	private Valve.VR.EVRButtonId _MenuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
	//private Valve.VR.EVRButtonId _SystemButton = Valve.VR.EVRButtonId.k_EButton_System;

	//Trigger
	public static event Action OnOneTrigger;
	public static event Action OnOneTriggerUp;						//called when only one trigger up
	public static event Action OnOneTriggerDown;					//called when only one trigger down
	public static event Action OnBothTriggers;
	public static event Action OnBothTriggersUp;					//called when both triggers down
	public static event Action OnBothTriggersDown;					//called when both triggers down
	public static int triggerPrimary;								//index of controller pressed (first)
	public static bool triggerStateChange = false;
	public static bool[] triggerState = new bool[] {false, false};	//axis state of both triggers

	public static float[] triggerValue = new float[] {0, 0};		//axis value of both triggers

	//Grip
	public static event Action OnOneGrip;
	public static event Action OnOneGripUp;					//called when only one trigger up
	public static event Action OnOneGripDown;				//called when only one trigger down
	public static event Action OnBothGrips;				
	public static event Action OnBothGripsUp;				//called when both triggers down
	public static event Action OnBothGripsDown;				//called when both triggers up
	public static int gripPrimary;							//index of controller pressed (first)
	public static bool gripStateChange = false;
	public static bool[] gripState = new bool[] {false, false};							//state of both grips

	//Menu
	public static event Action OnOneMenu;
	public static event Action OnOneMenuUp;						//called when only one trigger up
	public static event Action OnOneMenuDown;					//called when only one trigger down
	public static event Action OnBothMenus;				
	public static event Action OnBothMenusUp;					//called when both triggers down
	public static event Action OnBothMenusDown;					//called when both triggers up
	public static int menuPrimary;								//index of controller pressed (first)
	public static bool[] menuState = new bool[] {false, false};	//state of both menus

	//Touchpad
	public static event Action OnOneTouchHover;
	public static event Action OnOneTouchHoverUp;					
	public static event Action OnOneTouchHoverDown;				
	public static event Action OnBothTouchHover;				
	public static event Action OnBothTouchHoverUp;				
	public static event Action OnBothTouchHoverDown;				
	public static int touchHoverPrimary;							
	public static bool[] touchHoverState = new bool[] {false, false};

	public static event Action OnOneTouchPress;
	public static event Action OnOneTouchPressUp;					
	public static event Action OnOneTouchPressDown;				
	public static event Action OnBothTouchPress;				
	public static event Action OnBothTouchPressUp;				
	public static event Action OnBothTouchPressDown;				
	public static int touchPressPrimary;							
	public static bool[] touchPressState = new bool[] {false, false};

	public static Vector2[] touchXY = new Vector2[] {Vector2.zero, Vector2.zero};	//The x and y pos of the finger on the touchpad
	public static Vector2[] touchRad = new Vector2[] {Vector2.zero, Vector2.zero};	//The angle (degrees) and radius of finger on touchpad

	//RaycastAnchor
	public float _RayRange = 10f;
	public Transform _PointerAnchorLeft;
	public Transform _PointerAnchorRight;
	private Ray _Ray;
	private RaycastHit _Hit;

	public static int pointerHitPrimary;
	public static bool[] hitState = new bool[] {false, false};
	public static string[] hitTag = new string[] {null, null};
	public static int?[] hitLayer = new int?[] {null, null};
	public static float?[] hitDistance = new float?[] {null, null};
	public static GameObject[] hitObj = new GameObject[] {null, null};
	public static Vector3[] hitPos = new Vector3[] {Vector3.zero, Vector3.zero};
	public static Vector3[] hitPosPrev = new Vector3[] {Vector3.zero, Vector3.zero};
	public static Vector3[] hitNorm = new Vector3[] {Vector3.zero, Vector3.zero};
	public static Vector3[] hitNormPrev = new Vector3[] {Vector3.zero, Vector3.zero};

	// Use this for initialization
	void Start () {
		_Controllers = new SteamVR_Controller.Device[] {_CL,_CR};
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTrigger ();
		UpdateGrip ();
		UpdateMenu ();

		UpdateTouchHover();
		UpdateTouchPress();

		UpdateRayCasters ();
	}


	private void UpdateTrigger(){

		triggerStateChange = false;

		if (_CL.GetPress (_TriggerButton) ^ _CR.GetPress (_TriggerButton))
		{
			//up event
			if (triggerState [0] && triggerState [1])
			{
				if (OnBothTriggersUp != null)
				{
					triggerStateChange = true;
					OnBothTriggersUp ();
				}
			}
			//down event
			if (!triggerState [0] && !triggerState [1])
			{
				if (OnOneTriggerDown != null)
				{
					triggerStateChange = true;
					OnOneTriggerDown ();
				}
			}
			//primary
			triggerPrimary = _CL.GetPress(_TriggerButton) ? 0 : 1;
			//state
			triggerState [0] = _CL.GetPress(_TriggerButton);
			triggerState [1] = _CR.GetPress(_TriggerButton);
			//pressed event
			if (OnOneTrigger != null)
				OnOneTrigger ();
			
		} else if (_CL.GetPress (_TriggerButton) && _CR.GetPress (_TriggerButton))
		{
			//down event
			if (triggerState [0] ^ triggerState [1])
			{
				if (OnBothTriggersDown != null)
				{
					triggerStateChange = true;
					OnBothTriggersDown ();
				}
			}
			//state
			triggerState [0] = triggerState [1] = true;
			//pressed event
			if (OnBothTriggers != null)
				OnBothTriggers ();
			
		} else
		{
			//up event
			if (triggerState [0] ^ triggerState [1])
			{
				if (OnOneTriggerUp != null)
				{
					triggerStateChange = true;
					OnOneTriggerUp ();
				}
			}

			triggerState [0] = triggerState [1] = false;
		}

		//value
		triggerValue[0] = _CL.GetAxis(_TriggerButton).x;
		triggerValue[1] = _CR.GetAxis(_TriggerButton).x;
	}


	private void UpdateGrip(){

		gripStateChange = false;

		if (_CL.GetPress (_GripButton) ^ _CR.GetPress (_GripButton))
		{
			//up event
			if (gripState [0] && gripState [1])
			{
				if (OnBothGripsUp != null)
				{
					gripStateChange = true;
					OnBothGripsUp ();
				}
			}
			//down event
			if (!gripState [0] && !gripState [1])
			{
				if (OnOneGripDown != null)
				{
					gripStateChange = true;			
					OnOneGripDown ();
				}
			}
			//primary
			gripPrimary = _CL.GetPress(_GripButton) ? 0 : 1;
			//state
			gripState [0] = _CL.GetPress(_GripButton);
			gripState [1] = _CR.GetPress(_GripButton);
			//pressed event
			if (OnOneGrip != null)
				OnOneGrip ();

		} else if (_CL.GetPress (_GripButton) && _CR.GetPress (_GripButton))
		{
			//down event
			if (gripState [0] ^ gripState [1])
			{
				if (OnBothGripsDown != null)
				{
					gripStateChange = true;
					OnBothGripsDown ();
				}
			}
			//state
			gripState [0] = gripState [1] = true;
			//pressed event
			if (OnBothGrips != null)
				OnBothGrips ();

		} else
		{
			//up event
			if (gripState [0] ^ gripState [1])
			{
				if (OnOneGripUp != null)
				{
					gripStateChange = true;	
					OnOneGripUp ();
				}
			}

			gripState [0] = gripState [1] = false;
		}
	}


	private void UpdateMenu(){

		if (_CL.GetPress (_MenuButton) ^ _CR.GetPress (_MenuButton))
		{
			//up event
			if (menuState [0] && menuState [1])
			{
				if (OnBothMenusUp != null)
					OnBothMenusUp();
			}
			//down event
			if (!menuState [0] && !menuState [1])
			{
				if (OnOneMenuDown != null)
					OnOneMenuDown ();
			}
			//primary
			menuPrimary = _CL.GetPress(_MenuButton) ? 0 : 1;
			//state
			menuState [0] = _CL.GetPress(_MenuButton);
			menuState [1] = _CR.GetPress(_MenuButton);
			//pressed event
			if (OnOneMenu != null)
				OnOneMenu ();

		} else if (_CL.GetPress (_MenuButton) && _CR.GetPress (_MenuButton))
		{
			//down event
			if (menuState [0] ^ menuState [1])
			{
				if (OnBothMenusDown != null)
					OnBothMenusDown ();
			}
			//state
			menuState [0] = menuState [1] = true;
			//pressed event
			if (OnBothMenus != null)
				OnBothMenus ();

		} else
		{
			//up event
			if (menuState [0] ^ menuState [1])
			{
				if (OnOneMenuUp != null)
					OnOneMenuUp ();
			}

			menuState [0] = menuState [1] = false;
		}
	}


	private void UpdateTouchHover(){
		
		if (_CL.GetTouch (_TouchButton) ^ _CR.GetTouch (_TouchButton))
		{
			//up event
			if (touchHoverState [0] && touchHoverState [1])
			{
				if (OnBothTouchHoverUp != null)
					OnBothTouchHoverUp();
			}
			//down event
			if (!touchHoverState [0] && !touchHoverState [1])
			{
				if (OnOneTouchHoverDown != null)
					OnOneTouchHoverDown ();
			}
			//primary
			touchHoverPrimary = _CL.GetTouch(_TouchButton) ? 0 : 1;
			//state
			touchHoverState [0] = _CL.GetTouch(_TouchButton);
			touchHoverState [1] = _CR.GetTouch(_TouchButton);
			//pressed event
			if (OnOneTouchHover != null)
				OnOneTouchHover ();

		} else if (_CL.GetTouch (_TouchButton) && _CR.GetTouch (_TouchButton))
		{
			//down event
			if (touchHoverState [0] ^ touchHoverState [1])
			{
				if (OnBothTouchHoverDown != null)
					OnBothTouchHoverDown ();
			}
			//state
			touchHoverState [0] = touchHoverState [1] = true;
			//pressed event
			if (OnBothTouchHover != null)
				OnBothTouchHover ();

		} else
		{
			//up event
			if (touchHoverState [0] ^ touchHoverState [1])
			{
				if (OnOneTouchHoverUp != null)
					OnOneTouchHoverUp ();
			}

			touchHoverState [0] = touchHoverState [1] = false;
		}

		//value
		touchXY[0] = _CL.GetAxis(_TouchButton);
		touchXY[1] = _CR.GetAxis(_TouchButton);

		touchRad[0].x = Mathf.Atan2( _CL.GetAxis (_TouchButton).y, _CL.GetAxis (_TouchButton).x);
		touchRad[1].x = Mathf.Atan2( _CR.GetAxis (_TouchButton).y, _CR.GetAxis (_TouchButton).x);
		touchRad[0].y = touchRad[0].x * Mathf.Rad2Deg;
		touchRad[1].y = touchRad[1].x * Mathf.Rad2Deg;
	}


	private void UpdateTouchPress(){
		
		if (_CL.GetPress (_TouchButton) ^ _CR.GetPress (_TouchButton))
		{
			//up event
			if (touchPressState [0] && touchPressState [1])
			{
				if (OnBothTouchPressUp != null)
					OnBothTouchPressUp();
			}
			//down event
			if (!touchPressState [0] && !touchPressState [1])
			{
				if (OnOneTouchPressDown != null)
					OnOneTouchPressDown ();
			}
			//primary
			touchPressPrimary = _CL.GetPress(_TouchButton) ? 0 : 1;
			//state
			touchPressState [0] = _CL.GetPress(_TouchButton);
			touchPressState [1] = _CR.GetPress(_TouchButton);
			//pressed event
			if (OnOneTouchPress != null)
				OnOneTouchPress ();

		} else if (_CL.GetPress (_TouchButton) && _CR.GetPress (_TouchButton))
		{
			//down event
			if (touchPressState [0] ^ touchPressState [1])
			{
				if (OnBothTouchPressDown != null)
					OnBothTouchPressDown ();
			}
			//state
			touchPressState [0] = touchPressState [1] = true;
			//pressed event
			if (OnBothTouchPress != null)
				OnBothTouchPress ();

		} else
		{
			//up event
			if (touchPressState [0] ^ touchPressState [1])
			{
				if (OnOneTouchPressUp != null)
					OnOneTouchPressUp ();
			}

			touchPressState [0] = touchPressState [1] = false;
		}
	}


	/* need pointers to make abstract button update function?
	private void UpdateButton(Valve.VR.EVRButtonId id, bool[] state, int primary, Action one, Action oneUp, Action oneDown, Action both, Action bothUp, Action bothDown){

		if (_CL.GetPress (id) ^ _CR.GetPress (id))
		{
			//up event
			if (state[0] && state[1])
			{
				if (bothUp != null)
					bothUp ();
			}
			//down event
			if (!state[0] && !state[1])
			{
				if (oneDown != null)
					oneDown ();
			}
			//primary
			primary = _CL.GetPress(id) ? 0 : 1;
			//state
			state [0] = _CL.GetPress(id);
			state [1] = _CR.GetPress(id);
			//pressed event
			if (one != null)
				one ();

		} else if (_CL.GetPress (id) && _CR.GetPress (id))
		{
			//down event
			if (state [0] ^ state [1])
			{
				if (bothDown != null)
					bothDown ();
			}
			//state
			state [0] = state [1] = true;
			//pressed event
			if (both != null)
				both ();

		} else
		{
			//up event
			if (state [0] ^ state [1])
			{
				if (oneUp != null)
					oneUp ();
			}

			state [0] = state [0] = false;
		}
	}*/

	private void UpdateRayCasters(){

		for (int i = 0; i <= 1; i++)
		{
			hitPosPrev[i] = hitPos[i];
			hitNormPrev[i] = hitNorm[i];

			_Ray.origin = i==0 ? _PointerAnchorLeft.position : _PointerAnchorRight.position ;
			_Ray.direction = i==0 ? _PointerAnchorLeft.forward : _PointerAnchorRight.forward ;

			if (Physics.Raycast (_Ray, out _Hit, _RayRange))
			{
				hitState [i] = true;
				hitTag [i] = _Hit.transform.tag;
				hitLayer [i] = _Hit.transform.gameObject.layer;
				hitDistance [i] = _Hit.distance;
				hitObj [i] = _Hit.transform.gameObject;
				hitPos [i] = _Hit.point;
				hitNorm [i] = _Hit.normal;

				//_Hit.transform.gameObject.SendMessage ("HoverOn", this);
			} else
			{
				hitState[i] = true;
			}
		}
	}
}
