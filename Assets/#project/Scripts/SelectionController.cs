using UnityEngine;
using System;
using System.Collections.Generic;

public class SelectionController : MonoBehaviour {
	
	public static List<Transform> _Selection = new List<Transform> ();
	public static event Action OnSelectionChange; //an object is added to _Selection
	public static event Action OnSelectionCancel; //_Selection is cleared

	private void Start(){
		_Selection.Clear ();
	}

	private void OnEnable(){
		//InputController.OnRayOnModel += ManageSelection;
		InputController.OnOneTrigger += ManageSelection;
		InputController.OnBothTriggers += ManageSelection;
	}

	private void OnDisable(){
		//InputController.OnRayOnModel -= ManageSelection;
		InputController.OnOneTrigger -= ManageSelection;
		InputController.OnBothTriggers -= ManageSelection;
	}
		
	private void ManageSelection(WandController[] cont){

		if (InputController.inUse)
			return;

		foreach (WandController C in cont)
		{
			if (!C.triggerPress || !C.triggerDown || C.hitObj == null)
				return;

			if (!C.rayHit && InputController.activeTouchPress == 0 && C.triggerDown)
			{
				ClearSelection ();
				return;
			}
				
			
			if (!_Selection.Contains(C.hitObj.transform))
			{
				if (_Selection.Count >= 1 && InputController.activeTouchPress == 0)
				{
					ClearSelection ();
				} 

				if (C.hitObj.GetComponent<Geometry> () != null)
				{
					_Selection.Add (C.hitObj.transform);

					C.hitObj.GetComponent<Geometry> ().IsActive = true;
				}
			} else
			{
				if (C.triggerDown)
				{
					if (OnSelectionChange != null)
						OnSelectionChange ();
				}
			}
		}

	}

	private void ClearSelection(){
		foreach (Transform trans in _Selection)
		{
			trans.GetComponent<Geometry> ().IsActive = false;
		}
		_Selection.Clear ();

		if (OnSelectionCancel != null)
			OnSelectionCancel ();

		//_Movement._TargetTransforms.Clear();
		//_Movement._MovementEnabled = false;
	}
}

