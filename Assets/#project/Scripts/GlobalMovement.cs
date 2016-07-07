using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalMovement : MonoBehaviour {

	public Transform _GlobalWrapper;
	private List<Transform> _TargetTransforms = new List<Transform> ();

	private List<Vector3> _Focuspoints = new List<Vector3> ();
	private List<Vector3> _FocuspointsPrev = new List<Vector3> (); //previous frame
	private List<GameObject> _ActiveCursor = new List<GameObject> ();
	private List<WandController> _ActiveWand = new List<WandController> ();


	void Start(){
		_TargetTransforms.Add (_GlobalWrapper);
	}

	private void OnEnable(){
		InputController.OnNoGrips += SetCursorState;
		InputController.OnOneGrip += SetCursorState;
		InputController.OnBothGrips += SetCursorState;

		InputController.OnOneGrip += DoTranslate;
		InputController.OnBothGrips += DoTranslate;
		InputController.OnBothGrips += DoRotate;
		InputController.OnBothGrips += DoScale;

		//bug after scaling/rotating and holding one grip down, the model jumps to cursors new hitpoint of cursor
		//Todo - save position of cursor in TargetTransforms local space at start of transformation, and use this for transformation
	}

	private void OnDisable(){
		InputController.OnNoGrips -= SetCursorState;
		InputController.OnOneGrip -= SetCursorState;
		InputController.OnBothGrips -= SetCursorState;

		InputController.OnOneGrip -= DoTranslate;
		InputController.OnBothGrips -= DoTranslate;
		InputController.OnBothGrips -= DoRotate;
		InputController.OnBothGrips -= DoScale;
	}

	private void DoTranslate(WandController[] C){
		
		//skip a frame when changing states to avoid jumpy translations
		if (C[0].gripDown || C[1].gripDown || C[1].gripUp || C[0].gripUp ||
			C[0].triggerDown || C[1].triggerDown || C[1].triggerUp || C[0].triggerUp)
		{
			return;
		}

		Vector3 averagePoint; 
		Vector3 averagePointPrev;

		if (InputController.activeGrips > 1)
		{
			averagePoint = GetAverage (new Vector3[] {C[0].cursor.curPos, C[1].cursor.curPos});
			averagePointPrev = GetAverage (new Vector3[] {C[0].cursor.prevPos, C[1].cursor.prevPos});
		} else
		{
			averagePoint = C[0].cursor.curPos;
			averagePointPrev = C[0].cursor.prevPos;
		}

		Vector3 diff = averagePoint - averagePointPrev;
		//if (limitXZ)
			//diff = new Vector3 (diff.x, 0, diff.y);

		foreach (Transform trans in _TargetTransforms)
		{
			trans.Translate (diff, Space.World);
		}
	}

	private void DoRotate(WandController[] C){

		foreach (Transform trans in _TargetTransforms)
		{
			//skip a frame when changing states to avoid jumpy translations
			if (C[0].gripDown || C[1].gripDown || C[1].gripUp || C[0].gripUp ||
				C[0].triggerDown || C[1].triggerDown || C[1].triggerUp || C[0].triggerUp)
			{
				return;
			}

			//get rot angle and axis
			Vector3 oldDir = C[0].cursor.prevPos - C[1].cursor.prevPos;
			Vector3 newDir = C[0].cursor.curPos - C[1].cursor.curPos;
			if (true)
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

		foreach (Transform trans in _TargetTransforms)
		{
			//skip a frame when changing states to avoid jumpy translations
			if (C[0].gripDown || C[1].gripDown || C[1].gripUp || C[0].gripUp ||
				C[0].triggerDown || C[1].triggerDown || C[1].triggerUp || C[0].triggerUp)
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

			Vector3 correction = averagePostTransform -averagePreTransform;
			trans.Translate (-correction, Space.World);
		}
	
	}

	private void SetCursorState(WandController[] C){
		foreach (WandController cont in C)
		{
			if (cont.gripPress)
			{
				if (InputController.activeGrips > 1)
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

	public void SetScale(float scale){
		float value = 1f / scale;
		_GlobalWrapper.localScale = new Vector3 (value, value, value);
	}
}
