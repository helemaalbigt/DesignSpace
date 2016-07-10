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
			Debug.Log ("Waiting fir first point");

			cube.position = C.cursor.curPos;
			cube.rotation = Quaternion.LookRotation (C.hitNorm);

			yield return null;
		}

		_Points [0] = C.cursor.curPos;
		cube.parent = _GlobalWrapper;
		cube.localScale = Vector3.one;

		yield return null;

		while (!C.triggerDown)
		{
			Debug.Log ("Waiting for second point");

			cube.right = C.cursor.curPos - _Points [0];
			cube.localScale = new Vector3 (
				Vector3.Distance(C.cursor.curPos, _Points [0]) / _GlobalWrapper.localScale.x,
				cube.localScale.y,
				cube.localScale.z
			);
			cube.rotation = Quaternion.Euler(0, cube.rotation.eulerAngles.y, cube.rotation.eulerAngles.z);

			yield return null;
		}
			
		_Points [1] = C.cursor.curPos;

		yield return null;

		while(!C.triggerDown)
		{
			Debug.Log ("Waiting for third point");
			Vector3 cross = Vector3.Cross (_Points [1] - _Points [0], _Points [1] - C.cursor.curPos).normalized;
			Debug.Log (Vector3.Magnitude (cross));
			cube.localScale = new Vector3 (
				cube.localScale.x,
				cube.localScale.y,
				( Vector3.Distance(C.cursor.curPos, _Points [1]) / _GlobalWrapper.localScale.x ) //* Vector3.Magnitude(cross)
			);

			yield return null;
		}

		_Points [2] = C.cursor.curPos;
		Vector3 wandpos = C.transform.position;

		yield return null;

		while (!C.triggerDown)
		{
			Debug.Log ("Waiting for fourth point");

			cube.localScale = new Vector3 (
				cube.localScale.x,
				Vector3.Distance(wandpos, C.transform.position) / _GlobalWrapper.localScale.x,
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

		//end drawing
		InputController.inUse = false;
		_IsSelected = false;
	}
}
