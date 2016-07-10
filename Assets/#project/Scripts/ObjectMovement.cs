using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectMovement : MonoBehaviour {

	private List<Transform> _TargetTransforms = new List<Transform> ();
	private bool _GlobalMovement = true;

	private List<Vector3> _Focuspoints = new List<Vector3> ();
	private List<Vector3> _FocuspointsPrev = new List<Vector3> (); //previous frame
	private List<GameObject> _ActiveCursor = new List<GameObject> ();
	private List<WandController> _ActiveWand = new List<WandController> ();

	private bool movementEnabled = false;


	void Start(){
		_TargetTransforms.Clear ();
	}

	private void OnEnable(){
		InputController.OnNoTriggers += SetCursorState;
		InputController.OnOneTrigger += SetCursorState;
		InputController.OnBothTriggers += SetCursorState;

		InputController.OnOneTrigger += DoTranslate;
		InputController.OnBothTriggers += DoTranslate;
		InputController.OnBothTriggers += DoRotate;
		InputController.OnBothTriggers += DoScale;

		SelectionController.OnSelectionChange += SetTarget;
		SelectionController.OnSelectionCancel += SetTarget;

		SelectionController.OnSelectionChange += Enable;
		SelectionController.OnSelectionCancel += Disable;
		//bug after scaling/rotating and holding one grip down, the model jumps to cursors new hitpoint of cursor
		//Todo - save position of cursor in TargetTransforms local space at start of transformation, and use this for transformation
	}

	private void OnDisable(){
		InputController.OnNoTriggers -= SetCursorState;
		InputController.OnOneTrigger -= SetCursorState;
		InputController.OnBothTriggers -= SetCursorState;

		InputController.OnOneTrigger -= DoTranslate;
		InputController.OnBothTriggers -= DoTranslate;
		InputController.OnBothTriggers -= DoRotate;
		InputController.OnBothTriggers -= DoScale;

		SelectionController.OnSelectionChange -= SetTarget;
		SelectionController.OnSelectionCancel -= SetTarget;

		SelectionController.OnSelectionChange -= Enable;
		SelectionController.OnSelectionCancel -= Disable;
	}

	private void DoTranslate(WandController[] C){

		if (InputController.inUse)
			return;

		//skip a frame when changing states to avoid jumpy translations
		if (C[0].triggerDown || C[1].triggerDown || C[1].triggerUp || C[0].triggerUp)
		{
			return;
		}

		Vector3 averagePoint; 
		Vector3 averagePointPrev;

		if (InputController.activeTriggers > 1)
		{
			averagePoint = GetAverage (new Vector3[] {C[0].cursor.curPos, C[1].cursor.curPos});
			averagePointPrev = GetAverage (new Vector3[] {C[0].cursor.prevPos, C[1].cursor.prevPos});
		} else
		{
			averagePoint = C[0].cursor.curPos;
			averagePointPrev = C[0].cursor.prevPos;
		}

		//Debug.Log (C [0].cursor.curPos + " - " + C [1].cursor.curPos);

		Vector3 diff = averagePoint - averagePointPrev;
		//if (limitXZ)
			//diff = new Vector3 (diff.x, 0, diff.y);

		foreach (Transform trans in _TargetTransforms)
		{
			trans.Translate (diff, Space.World);
		}
			
	}

	private void DoRotate(WandController[] C){

		if (InputController.inUse)
			return;

		foreach (Transform trans in _TargetTransforms)
		{
			//skip a frame when changing states to avoid jumpy translations
			if (C[0].triggerDown || C[1].triggerDown || C[1].triggerUp || C[0].triggerUp)
			{
				return;
			}

			//get rot angle and axis
			Vector3 oldDir = C[0].cursor.prevPos - C[1].cursor.prevPos;
			Vector3 newDir = C[0].cursor.curPos - C[1].cursor.curPos;

			if(!(C[1].hitObj.tag == "Image"))
			{
				oldDir = new Vector3 (oldDir.x, 0, oldDir.z);
				newDir = new Vector3 (newDir.x, 0, newDir.z);
			}

			Vector3 cross = Vector3.Cross (oldDir, newDir);
			float rotAngle = Vector3.Angle (oldDir, newDir);

			//correction part 1 - get current global pos and localpos for focuspoints
			Vector3[] localPointsPreTransform = new Vector3[] {
				trans.InverseTransformPoint (C[0].cursor.curPos),
				trans.InverseTransformPoint (C[1].cursor.curPos)
			};
			Vector3 averagePreTransform = GetAverage( new Vector3[] {C[0].cursor.curPos,C[1].cursor.curPos} );

			//rotate
			trans.Rotate (cross.normalized, rotAngle, Space.World);

			//correction part 2 - convert localpos back to globalpos to see diff with original, then correct diff
			Vector3[] localPointsPostTransform = new Vector3[] {
				trans.TransformPoint(localPointsPreTransform[0]),
				trans.TransformPoint(localPointsPreTransform[1])
			};
			Vector3 averagePostTransform = GetAverage (localPointsPostTransform);
			
			Vector3 correction = averagePostTransform -averagePreTransform;
			trans.Translate (-correction, Space.World);
		}
			
	}

	private void DoScale(WandController[] C){

		if (InputController.inUse)
			return;

		foreach (Transform trans in _TargetTransforms)
		{
			//skip a frame when changing states to avoid jumpy translations
			if (C[0].triggerDown || C[1].triggerDown || C[1].triggerUp || C[0].triggerUp)
			{
				return;
			}

			//get magintude diff
			float oldMag = Vector3.Magnitude(C[0].cursor.prevPos - C[1].cursor.prevPos);
			float newMag = Vector3.Magnitude(C[0].cursor.curPos - C[1].cursor.curPos);
			float factor = newMag / oldMag;

			//correction part 1 - get current global pos and localpos for focuspoints
			Vector3[] localPointsPreTransform = new Vector3[] {
				trans.InverseTransformPoint (C[0].cursor.curPos),
				trans.InverseTransformPoint (C[1].cursor.curPos)
			};
			Vector3 averagePreTransform = GetAverage( new Vector3[] {C[0].cursor.curPos,C[1].cursor.curPos} );

			//scale
			trans.localScale = new Vector3 (factor * trans.localScale.x, factor * trans.localScale.y, factor * trans.localScale.z);

			//correction part 2 - get worldpoints for converted focuspoints and check diff 
			Vector3[] localPointsPostTransform = new Vector3[] {
				trans.TransformPoint(localPointsPreTransform[0]),
				trans.TransformPoint(localPointsPreTransform[1])
			};
			Vector3 averagePostTransform = GetAverage (localPointsPostTransform);

			Vector3 correction = averagePostTransform - averagePreTransform;
			trans.Translate (-correction, Space.World);
		}
			
	}

	private void SetTarget(){
		if (SelectionController._Selection.Count > 0)
		{
			if (SelectionController._Selection.Count > _TargetTransforms.Count)
			{
				_TargetTransforms.Clear ();
				foreach (Transform trans in SelectionController._Selection)
				{
					_TargetTransforms.Add (trans);
				}
			}
		} else
		{
			_TargetTransforms.Clear ();
		}
	}

	private void Enable(){
		movementEnabled = true;
	}

	private void Disable(){
		movementEnabled = false;
		foreach (WandController C in InputController.controllers)
		{
			C.cursor.SetCursorState (CursorController.CursorState.unlocked);
		}
	}

	private void SetCursorState(WandController[] C){

		if (InputController.inUse)
			return;

		foreach (WandController cont in C)
		{
			if (cont.triggerPress)
			{
				if (InputController.activeTriggers > 1)
				{
					cont.cursor.SetCursorState (CursorController.CursorState.lockRad);
				} else
				{
					cont.cursor.SetCursorState (CursorController.CursorState.lockRad);
				}
			} else
			{
				cont.cursor.SetCursorState (CursorController.CursorState.unlocked);
			}
		}
	}

	private Vector3 GetAverage(List<Vector3> list){
		Vector3 averagePoint = new Vector3();
		foreach (Vector3 pos in list)
		{
			averagePoint += pos;
		}
		averagePoint /= list.Count;
		return averagePoint;
	}

	private Vector3 GetAverage(Vector3[] list){
		Vector3 averagePoint = new Vector3();
		foreach (Vector3 pos in list)
		{
			averagePoint += pos;
		}
		averagePoint /= list.Length;
		return averagePoint;
	}
}
