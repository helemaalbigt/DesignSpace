using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class MenuCheckbox: MenuButton {
	
	public UnityEvent _ClickOn;
	public UnityEvent _ClickOff;

	public bool _IsSelected = false;
    public Color _SelectedColor;

    public void Start(){
		base.Start ();
        if (_Background)
            _Background.color = _IsSelected ? _SelectedColor : _NeutralColor;
        if(_Icon)
            _Icon.color = _IsSelected ? _IconSelectedColor : _IconNeutralColor;
        if (_Name)
            _Name.color = _IsSelected ? _IconSelectedColor : _IconNeutralColor;
    }

	public void HoverOn(WandController C){
		base.HoverOn(C);
		if (C.triggerDown)
		{
			if (_IsSelected)
			{
				Deselect ();
			} else
			{
				Select();
			}
		}
	}
		
	public void Deselect(){
		_IsSelected = false;
        if (_Background)
            _Background.color = _NeutralColor;
        if (_Icon)
            _Icon.color = _IconNeutralColor;
        if (_Name)
            _Name.color = _IconNeutralColor;
        _ClickOff.Invoke ();
	}

	public void Select(){
		_IsSelected = true;
        if (_Background)
            _Background.color = _SelectedColor;
        if (_Icon)
            _Icon.color = _IconSelectedColor;
        if (_Name)
            _Name.color = _IconSelectedColor;
        _ClickOn.Invoke ();
	}
}
