using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuIcon : MonoBehaviour {

	public Transform _TabContents;
	public float _HoldTime = 1f;

	private Vector3 _StartPos;
	private bool _Held = false;					//trigger is held down while hovering on
	private bool _Grabbed = false;
	private float _HoldStartTime = -9999f;

	private Transform _Tab;

	// Use this for initialization
	void Start () {
		Invoke ("PositionTab", 0.5f);
	}
	
	/**
	 * Handle Pickup
	 */
	public void HoverOn(WandController C) {

		if (C.triggerPress && !InputController.inUse)
		{

			if (!_Held)
			{
				_Held = true;
				_HoldStartTime = Time.time;
			}

			if (Time.time - _HoldStartTime > _HoldTime && !_Grabbed)
				StartCoroutine (MoveMenu (C));
		} else
		{
			_Held = false;
		}
	}

	IEnumerator MoveMenu(WandController C){

		C.cursor.SetCursorState(CursorController.CursorState.lockRadNorm);
		InputController.inUse = true;
		_Grabbed = true;
		C.VibratePeriod (3, 0.02f);

		MenuGroupController.EnableMenuGroups ();

		transform.Find ("MenuButton").GetComponent<Toggle> ().group = null;
		transform.parent = null;

		Transform menuDock = null;

		while (C.triggerPress)
		{
			Transform foundMenuDock = null;

			//position button
			transform.position = C.cursor.transform.position;
			transform.forward = -C.cursor.transform.up;

			//check if you hit a menudock
			Collider[] hits = Physics.OverlapSphere(C.cursor.transform.position, 0.02f);
			foreach (Collider col in hits)
			{
				if (col.gameObject.tag == "MenuDock")
				{
					col.SendMessage ("Entered");
					foundMenuDock = col.transform;
					C.VibratePeriod (3, 0.05f);
				} 
			}

			menuDock = (foundMenuDock == null) ? null : foundMenuDock;

			yield return null;
		}

		yield return null;

		MenuGroupController.DisableMenuGroups ();

		//if menu is in menudock, place it there, otherwise, put it in place
		if (menuDock != null)
		{
			transform.parent = menuDock.parent.Find ("MenuDock");
			transform.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3(
				transform.GetComponent<RectTransform> ().anchoredPosition3D.x,
				transform.GetComponent<RectTransform> ().anchoredPosition3D.y,
				0f
			);
			transform.GetComponent<Canvas> ().worldCamera = GameObject.Find ("Controller UI Camera").GetComponent<Camera> ();
			transform.Find ("MenuButton").GetComponent<Toggle> ().group = menuDock.parent.Find ("MenuDock").GetComponent<ToggleGroup> ();
			transform.GetComponent<RectTransform> ().localRotation = Quaternion.identity;
		} else
		{
			transform.parent = null;
			transform.GetComponent<RectTransform> ().position = C.cursor.transform.position;
			transform.GetComponent<Canvas> ().worldCamera = GameObject.Find ("Controller UI Camera").GetComponent<Camera> ();
			transform.Find ("MenuButton").GetComponent<Toggle> ().group = null;
		}


		InputController.inUse = false;
		_Grabbed = false;
		MenuGroupController.RecalcTabDock ();
		PositionTab ();
		C.cursor.SetCursorState (CursorController.CursorState.unlocked);

	}

	/**
	 * Place menu contents; either at tabdock distance or next to the menu button
	 */
	public void PositionTab(){
		if (transform.parent != null)
		{
			//_TabContents.GetComponent<RectTransform> ().anchoredPosition3D = transform.parent.parent.Find ("TabDock").GetComponent<RectTransform> ().anchoredPosition3D;
			//_TabContents.GetComponent<RectTransform> ().anchoredPosition3D = _TabContents.transform.InverseTransformPoint(transform.parent.parent.Find ("TabDock").GetComponent<RectTransform> ().position);
			_TabContents.parent =  transform.parent.parent.Find ("TabDock");

			transform.parent.parent.Find ("TabDock").GetComponent<TabDockPlacer> ().SetTabDockPos ();

			_TabContents.forward = transform.forward;
			_TabContents.GetComponent<RectTransform> ().anchoredPosition3D = Vector3.zero;
			_TabContents.GetComponent<RectTransform> ().localRotation = Quaternion.identity;
		} else
		{
			_TabContents.parent = transform;
			_TabContents.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (GetComponent<RectTransform> ().rect.width + 5f,0,0);
			_TabContents.forward = transform.forward;
		}
	}
}
