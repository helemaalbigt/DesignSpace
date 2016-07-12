using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class CreateCube : MenuCheckbox
{
	public GameObject _CubePrefab;
	public Transform _GlobalWrapper;
	private Vector3[] _Points = new Vector3[4];

	public void HoverOn(WandController C){
		base.HoverOn(C);

		if (_IsSelected && C.triggerDown)
		{
			C.cursor.SetCursorState (CursorController.CursorState.unlocked);
			InputController.inUse = true;

			Debug.Log ("Start Cube");
			StartCoroutine(MakeCube(C));
		}
	}

	IEnumerator MakeCube(WandController C){
		
		yield return null;

		Quaternion startRot = Quaternion.LookRotation (C.hitNorm);
		GameObject inst = Instantiate (_CubePrefab, _Points [0], startRot) as GameObject;
		Transform cube = inst.transform;

		while (!C.triggerDown)
		{
			Debug.Log ("Waiting for first point");

			cube.position = C.cursor.curPos;
			cube.rotation = Quaternion.LookRotation (C.hitNorm);

			yield return null;
		}

		_Points [0] = C.cursor.curPos;
		cube.parent = _GlobalWrapper;
		cube.localScale = Vector3.one;

		yield return null;

		cube.localScale = new Vector3 (
			-cube.localScale.x,
			cube.localScale.y,
			cube.localScale.z
		);

		while (!C.triggerDown)
		{
			Debug.Log ("Waiting for second point");

			cube.rotation = Quaternion.LookRotation (C.cursor.curPos - _Points [0], C.hitNorm);
			cube.localScale = new Vector3 (
				cube.localScale.x,
				cube.localScale.y,
				Vector3.Distance(C.cursor.curPos, _Points [0]) / _GlobalWrapper.localScale.x
			);
			//cube.rotation = Quaternion.Euler(0, cube.rotation.eulerAngles.y, cube.rotation.eulerAngles.z);

			yield return null;
		}
			
		_Points [1] = C.cursor.curPos;

		yield return null;

		while(!C.triggerDown)
		{
			Debug.Log ("Waiting for third point");

			float sign = (Vector3.Angle(_Points [1] - C.cursor.curPos, cube.right)) <= 90f ? -1f : 1f; //determine sign by position of cursor relative to cube right direction

			Vector3 cursorPosDelta = C.cursor.curPos - _Points [1];
			Vector3 deltaProjected = Vector3.Project (cursorPosDelta, cube.right);

			cube.localScale = new Vector3 (
				sign*( Vector3.Magnitude(deltaProjected) / _GlobalWrapper.localScale.x ),
				cube.localScale.y,
				cube.localScale.z//* Vector3.Magnitude(cross)
			);

			yield return null;
		}

		_Points [2] = C.cursor.curPos;
		Vector3 wandpos = C.transform.position;
		//float wandDistPre = Vector3.Distance (_Points [2], C.transform.position); //distance between wand and point 2 before positioning

		yield return null;

		while (!C.triggerDown)
		{
			Debug.Log ("Waiting for fourth point");

			float sign = (Vector3.Angle(C.transform.position -  wandpos, cube.up)) <= 90f ? 1f : -1f;

			Vector3 controllerPosDelta = C.transform.position - wandpos; //vector from pos controller at start extrusion to cur pos
			Vector3 deltaProjected = Vector3.Project (controllerPosDelta, cube.up); //projected on up direction

			cube.localScale = new Vector3 (
				cube.localScale.x,
				sign*Vector3.Magnitude(deltaProjected) / _GlobalWrapper.localScale.x,
				cube.localScale.z
			);

			yield return null;
		}

		//add pickup
		GameObject childCube = cube.transform.Find("Cube").gameObject;

		childCube.transform.parent = _GlobalWrapper;

		childCube.AddComponent<Geometry>();
		BoxCollider col = childCube.AddComponent<BoxCollider>();
		//col.center = new Vector3(0.5f, 0.5f, 0.5f);
		childCube.layer = LayerMask.NameToLayer ("Model");

		yield return null;

		//end drawing
		InputController.inUse = false;
		_IsSelected = false;
	}
}
