using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class ScrollBar : MonoBehaviour {

	public Image _Handle;
	private float _hH; //handle height
	private float _hPos; //handle height
	private float _bH; //bar height

	[Range (0f,1f)] public float _Startvalue = 1f;
	public float _Value;

	public UnityEvent _OnValueChange;

	// Use this for initialization
	void Start () {
		_bH = GetComponent<RectTransform> ().rect.height;
		_hH = _Handle.rectTransform.rect.height;
	}

	public void HoverOn(WandController C){
		if (C.triggerPress)
		{
			/*_hPos = Mathf.Clamp(transform.InverseTransformPoint (C.hitPos).y + _hH/2, -(_bH - _hH), 0);

			_Handle.rectTransform.localPosition = new Vector3(
				_Handle.rectTransform.localPosition.x, 
				_hPos,
				_Handle.rectTransform.localPosition.z
			);

			_Value = -_hPos / (_bH - _hH);*/
			StartCoroutine (FollowCursor (C));
			_OnValueChange.Invoke();
		}
	}

	IEnumerator FollowCursor(WandController C){
		while (!C.triggerUp)
		{
			_hPos = Mathf.Clamp(transform.InverseTransformPoint (C.hitPos).y + _hH/2, -(_bH - _hH), 0);

			_Handle.rectTransform.localPosition = new Vector3(
				_Handle.rectTransform.localPosition.x, 
				_hPos,
				_Handle.rectTransform.localPosition.z
			);

			_Value = -_hPos / (_bH - _hH);

			yield return null;
		}
	}
}
