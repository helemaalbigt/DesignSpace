using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class MenuCheckbox: MenuButton {
	
	public UnityEvent _ClickOn;
	public UnityEvent _ClickOff;

	public bool _IsSelected = false;
    protected Color _NeutralColor = new Color32(0,0,0,155);
	protected Color _SelectedColor = new Color32(0, 0, 0, 255);
    public Color HexSelecter;

	public void Start(){
		base.Start ();
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
		_Background.color = _NeutralColor;
		_ClickOff.Invoke ();
	}

	public void Select(){
		_IsSelected = true;
		_Background.color = _SelectedColor;
		_ClickOn.Invoke ();
	}
}
