using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class MenuButton : MonoBehaviour {
	
	public UnityEvent _Click;

	protected Image _Highlight;
	protected Image _Background;

	protected float _HoverOffTime = 0.05f;
	protected float _LastHoverTimestamp;
	protected bool _IsHoveredOn = false;


	// Use this for initialization
	public void Start () {
		_Highlight = transform.Find("ButtonHighlight").GetComponent<Image>();
		_Background = GetComponent<Image>();
		if(_Highlight != null)
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


	protected void HoverOff(){
		_IsHoveredOn = false;
		if(_Highlight != null)
			_Highlight.enabled = false;
	}


	protected IEnumerator HoverOffCount(){
		while (Time.time - _LastHoverTimestamp < _HoverOffTime)
		{
			yield return null;
		}
		HoverOff ();
	}
}
