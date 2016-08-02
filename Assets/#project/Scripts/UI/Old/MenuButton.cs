using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class MenuButton : MonoBehaviour {
	
	public UnityEvent _Click;

	protected Image _Highlight;
	protected Image _Background;
    protected Image _Icon;
    protected Text _Name;

    protected float _HoverOffTime = 0.05f;
	protected float _LastHoverTimestamp;
	protected bool _IsHoveredOn = false;

    protected Color _NeutralColor = new Color32(255, 255, 255, 115);
    protected Color _SelectedColor = new Color32(70, 155, 255, 155);
    protected Color _IconNeutralColor = new Color32(0, 0, 0, 255);
    protected Color _IconSelectedColor = new Color32(0, 0, 0, 255);

    // Use this for initialization
    public void Start () {
        if (transform.Find("ButtonHighlight"))
            _Highlight = transform.Find("ButtonHighlight").GetComponent<Image>();

        if(transform.Find("Icon"))
            _Icon = transform.Find("Icon").GetComponent<Image>();

        if (transform.Find("Name"))
            _Name = transform.Find("Name").GetComponent<Text>();

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

        if(_Highlight != null)
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
