//General: index 0 = left, index 1 = right
using UnityEngine;
using System;

public class InputController : MonoBehaviour {

	public static bool inUse = false;

	//Controller references
	public WandController left;
	public WandController right;
	public static WandController[] controllers;

	//Trigger
	private int FirstTriggerIndex = 0; 								//index (L=0, R=1) of first trigger pressed (in case both pressed, indicates the one that was already pressed)
	public static event Action<WandController[]> OnOneTrigger;		//Fires when only one trigger active - passes WandController of active trigger at index 0, and other at index 1
	public static event Action<WandController[]> OnBothTriggers;	//Fires when both triggers active - passes WandController of first active trigger at index 0, and other at index 1
	public static event Action<WandController[]> OnNoTriggers;		//firees when both triggers are first released
	public static int activeTriggers = 0; 							//how many triggers are active

	//Grip
	private int FirstGripIndex = 0;
	public static event Action<WandController[]> OnOneGrip;
	public static event Action<WandController[]> OnBothGrips;
	public static event Action<WandController[]> OnNoGrips;
	public static int activeGrips = 0; //how many triggers are active

	//Menu
	private int FirstMenuIndex = 0;
	public static event Action<WandController[]> OnOneMenu;
	public static event Action<WandController[]> OnBothMenus;	
	public static int activeMenus = 0; //how many triggers are active

	//Touchpad
	private int FirstTouchHoverIndex = 0;
	public static event Action<WandController[]> OnOneTouchHover;			
	public static event Action<WandController[]> OnBothTouchHover;	
	public static int activeTouchHovers = 0; //how many triggers are active

	private int FirstTouchPressIndex = 0;
	public static event Action<WandController[]> OnOneTouchPress;			
	public static event Action<WandController[]> OnBothTouchPress;	
	public static int activeTouchPress = 0; //how many triggers are active

	//Raycasts
	private int FirstRayOnModelIndex = 0;
	public static event Action<WandController[]> OnRayOnModel;	
	public static int activeRaysOnModel = 0;

	void Start(){
		controllers = new WandController[] { left, right };
		inUse = false;
	}


	// Update is called once per frame
	void Update () {
		UpdateTrigger ();
		UpdateGrip ();
		UpdateTouchHover();
		UpdateTouchPress();
		UpdateRayHits ();
	}


	private void UpdateTrigger(){
		WandController[] controllers;

		if (left.triggerPress && right.triggerPress)
		{
			controllers = FirstTriggerIndex == 0 ? new WandController[] { left, right } : new WandController[] { right, left };

			activeTriggers = 2;

			if (OnBothTriggers != null)
				OnBothTriggers (controllers);
		} else if (left.triggerPress ^ right.triggerPress)
		{
			FirstTriggerIndex = left.triggerPress ? 0 : 1;
			controllers = left.triggerPress ? new WandController[] { left, right } : new WandController[] { right, left };

			activeTriggers = 1;

			if (OnOneTrigger != null)
				OnOneTrigger (controllers);
		} else
		{
			if (activeTriggers != 0)
			{
				controllers = new WandController[] { left, right };
				if (OnNoTriggers != null)
					OnNoTriggers (controllers);
			}
			activeTriggers = 0;
		}
	}


	private void UpdateGrip(){
		WandController[] controllers;

		if (left.gripPress && right.gripPress)
		{
			controllers = FirstGripIndex == 0 ? new WandController[] { left, right } : new WandController[] { right, left };

			activeGrips = 2;

			if (OnBothGrips != null)
				OnBothGrips (controllers);
		} else if (left.gripPress ^ right.gripPress)
		{
			FirstGripIndex = left.gripPress ? 0 : 1;
			controllers = left.gripPress ? new WandController[] { left, right } : new WandController[] { right, left };

			activeGrips = 1;

			if (OnOneGrip != null)
				OnOneGrip (controllers);
		} else
		{
			if (activeGrips != 0)
			{
				controllers = new WandController[] { left, right };
				if (OnNoGrips != null)
					OnNoGrips (controllers);
			}
			activeGrips = 0;
		}
	}


	private void UpdateTouchHover(){
		WandController[] controllers;

		if (left.touchHover && right.touchHover)
		{
			controllers = FirstTouchHoverIndex == 0 ? new WandController[] { left, right } : new WandController[] { right, left };

			activeTouchHovers = 2;

			if (OnBothTouchHover != null)
				OnBothTouchHover (controllers);
		} else if (left.touchHover ^ right.touchHover)
		{
			FirstTouchHoverIndex = left.touchHover ? 0 : 1;
			controllers = left.touchHover ? new WandController[] { left, right } : new WandController[] { right, left };

			activeTouchHovers = 1;

			if (OnOneTouchHover != null)
				OnOneTouchHover (controllers);
		} else
		{
			activeTouchHovers = 0;
		}
	}
		
	private void UpdateTouchPress(){
		WandController[] controllers;

		if (left.touchPress && right.touchPress)
		{
			controllers = FirstTouchPressIndex == 0 ? new WandController[] { left, right } : new WandController[] { right, left };

			activeTouchPress = 2;

			if (OnBothTouchPress != null)
				OnBothTouchPress (controllers);
		} else if (left.touchPress ^ right.touchPress)
		{
			FirstTouchPressIndex = left.touchPress ? 0 : 1;
			controllers = left.touchPress ? new WandController[] { left, right } : new WandController[] { right, left };

			activeTouchPress = 1;

			if (OnOneTouchPress != null)
				OnOneTouchPress (controllers);
		} else
		{
			activeTouchPress = 0;
		}
	}

	private void UpdateRayHits(){
		WandController[] controllers;

		if ((left.rayHit && left.hitLayer == LayerMask.NameToLayer ("Model")) &&
		    (right.rayHit && right.hitLayer == LayerMask.NameToLayer ("Model")))
		{
			controllers = FirstRayOnModelIndex == 0 ? new WandController[] { left, right } : new WandController[] { right, left };

			activeRaysOnModel = 2;

			if (OnRayOnModel != null)
				OnRayOnModel (controllers);

		} else if ((left.rayHit && left.hitLayer == LayerMask.NameToLayer ("Model")) ^
		           (right.rayHit && right.hitLayer == LayerMask.NameToLayer ("Model")))
		{
			FirstRayOnModelIndex = (left.rayHit && left.hitLayer == LayerMask.NameToLayer ("Model")) ? 0 : 1;
			controllers = (left.rayHit && left.hitLayer == LayerMask.NameToLayer ("Model")) ? new WandController[] { left, right } : new WandController[] { right, left };

			activeRaysOnModel = 1;

			if (OnRayOnModel != null)
				OnRayOnModel (controllers);
			
		} else
		{
			activeRaysOnModel = 0;
		}
	}
}
