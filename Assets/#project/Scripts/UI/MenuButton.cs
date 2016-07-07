using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class MenuButton : MonoBehaviour {
	
	public UnityEvent _Click;
	public UnityEvent _ClickOn;
	public UnityEvent _ClickOff;

	private Image _Highlight;
	private Image _Background;

	private float _HoverOffTime = 0.05f;
	private float _LastHoverTimestamp;
	private bool _IsHoveredOn;

	public Color _NeutralColor = new Color(255f,255f,255f,55f);
	public Color _SelectedColor = new Color(50f,130f,255f,55f);
	private bool _IsSelected = false;

	// Use this for initialization
	void Start () {
		_Highlight = transform.Find ("ButtonHighlight").GetComponent<Image>();
		_Background = GetComponent<Image>();
		_Highlight.enabled = _IsHoveredOn;
	}
	
	// Update is called once per frame
	void Update () {
		
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

			if (_IsSelected)
			{
				Deselect ();
			} else
			{
				Select();
			}
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

	public void Deselect(){
		_IsSelected = false;
		_Background.color = _NeutralColor;
		_ClickOff.Invoke ();
	}

	public void Select(){
		_IsSelected = true;
		_Background.color = _SelectedColor;
		_ClickOn.Invoke ();
	}
}
