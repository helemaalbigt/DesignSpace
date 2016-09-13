using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class MenuCheckbox: MenuButton {
	
	public UnityEvent _ClickOn;
	public UnityEvent _ClickOff;

	public bool _IsSelected = false;

    public new Color _SelectedColor;
    public new Color _TextNeutralColor;

    public void Start(){
		base.Start ();
        SetColor();
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
        SetColor();
        _ClickOff.Invoke ();
	}

	public void Select(){
		_IsSelected = true;
        SetColor();
        _ClickOn.Invoke ();
	}

    private void SetColor()
    {
        if (_Background)
            _Background.color = _IsSelected ? _SelectedColor : _NeutralColor;
        if (_Icon)
            _Icon.color = _IsSelected ? _IconSelectedColor : _IconNeutralColor;
        if (_Name)
            _Name.color = _IsSelected ? _IconSelectedColor : _IconNeutralColor;
        if (_Text)
            _Text.color = _IsSelected ? _TextSelectedColor : _TextNeutralColor;
    }
}
