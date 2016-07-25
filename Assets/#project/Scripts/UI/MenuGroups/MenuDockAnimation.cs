using UnityEngine;
using System.Collections;

public class MenuDockAnimation : MonoBehaviour {

	private float _ExitTime = 0.1f;
	private float _LastEnterTime = -99999f;
	private bool _Entered = false;

	public void Entered(){
		_LastEnterTime = Time.time;

		if(!_Entered){
			StartCoroutine (Exited ());
		}
	}

	IEnumerator Exited(){
		_Entered = true;
		GetComponent<Animator> ().SetBool ("InsideDropZone", true);

		while (Time.time - _LastEnterTime < _ExitTime)
		{
			yield return null;
		}

		_Entered = false;
		GetComponent<Animator> ().SetBool ("InsideDropZone", false);
	}
}
