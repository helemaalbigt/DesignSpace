using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class MenuButton : MonoBehaviour {
	
	public UnityEvent _Click;

	protected Image _Highlight;
	protected Image _Background;

	private float _HoverOffTime = 0.05f;
	private float _LastHoverTimestamp;
	protected bool _IsHoveredOn = false;

	// Use this for initialization
	void Start () {
		_Highlight = transform.Find("ButtonHighlight").GetComponent<Image>();
		_Background = GetComponent<Image>();
		_Highlight.enabled = _IsHoveredOn;
	}
	void OnDisable(){
		HoverOff ();
	}


	public void HoverOn(WandController C){

		_LastHoverTimestamp = Time.time;
		if (!_IsHoveredOn)
		{
			_IsHoveredOn = true;
			StartCoroutine (HoverOffCount ());
		}

		_Highlight.enabled = true;

		if (C.triggerDown)
		{
			_Click.Invoke ();
		}
	}


	public void HoverOff(){
		_IsHoveredOn = false;
		_Highlight.enabled = false;
	}


	IEnumerator HoverOffCount(){
		while (Time.time - _LastHoverTimestamp < _HoverOffTime)
		{
			yield return null;
		}
		HoverOff ();
	}
}
