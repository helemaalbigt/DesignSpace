using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SunController : MonoBehaviour {

	public Transform _Azimuth;
	public Transform _Height;

	public GameObject _Sunplane;
	public Transform _Sun;

	public Vector2 _SunRise;
	public Vector2 _SunSet;
	public Text _TimeReadout;

	private float _LastHoverTimestamp = -999999f;
	private float _HoverOffTime = 0.1f;
	private bool _IsHoverdOn = false;

	private Ray _Ray;
	private RaycastHit _Hit;
	private float rad = 0.75f;

	void Start(){
		_Sunplane.SetActive (false);
	}

	void OnEnable(){
		InputController.OnOneTrigger += UpdateSunPos;
		InputController.OnBothTriggers += UpdateSunPos;
	}

	void OnDisable(){
		InputController.OnOneTrigger -= UpdateSunPos;
		InputController.OnBothTriggers -= UpdateSunPos;
	}

	// Update is called once per frame
	void HoverOn() {
		
		_LastHoverTimestamp = Time.time;
		if (!_IsHoverdOn)
		{
			_Sunplane.SetActive (true);
			StartCoroutine (HoverOffCount ());
			_IsHoverdOn = true;
		}
		//_Height.RotateAround (_Height.position, _Height.up, 1f);
	}


	IEnumerator HoverOffCount(){
		while (Time.time - _LastHoverTimestamp < _HoverOffTime)
		{
			if(InputController.activeTriggers > 0)
				_LastHoverTimestamp = Time.time;

			yield return null;
		}

		HoverOff ();
	}

	public void HoverOff(){
		_Sunplane.SetActive (false);
		_IsHoverdOn = false;
	}

	private void UpdateSunPos (WandController[] C){
		if (InputController.inUse)
			return;
		
		if (!_IsHoverdOn)
			return;

		Vector2 S, P; //Sun, Cursor Point

		_Ray.origin = C[0]._PointerAnchor.position;
		_Ray.direction = C[0]._PointerAnchor.forward;

		int layerMask = 1 << LayerMask.NameToLayer("SunPlane");

		if (Physics.Raycast (_Ray, out _Hit, 10f, layerMask))
		{
			Vector3 LocalHitPos = _Sunplane.transform.InverseTransformPoint(_Hit.point);
			P.x = LocalHitPos.x;
			P.y = LocalHitPos.z;

			float nom = Mathf.Pow (rad, 2);
			float denom = 1f + Mathf.Pow ((P.x / P.y), 2);
			S.x = Mathf.Sqrt (nom/denom);
			S.y = (P.x / P.y) * S.x;

			Mathf.Clamp (S.x, -rad, rad);
			Mathf.Clamp (S.y, -rad, rad);

			float angle = Mathf.Atan2 (S.y, S.x);
			angle /= Mathf.PI;
			angle += 0.5f;
			UpdateTime (angle);


			_Sun.localPosition = new Vector3 (-S.y, 0, -S.x);
		}
	}

	private void UpdateTime(float factor){
		float timeMinutes = Mathf.Lerp(_SunRise.x*60 + _SunRise.y, _SunSet.x*60 + _SunSet.y, factor);
		Vector2 newTime;
		newTime.x = Mathf.Floor(timeMinutes / 60f);
		newTime.y = Mathf.Floor(timeMinutes % 60f);

		string minutes = newTime.y < 10f ? "0" + newTime.y : newTime.y+"";

		string affix = newTime.x <= 12 ? "AM" : "PM";

		_TimeReadout.text = newTime.x + ":" + minutes +" "+ affix;
	}
		
}
