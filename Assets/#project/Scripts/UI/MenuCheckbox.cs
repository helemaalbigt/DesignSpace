using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class MenuCheckbox: MenuButton {
	
	public UnityEvent _ClickOn;
	public UnityEvent _ClickOff;

	protected bool _IsSelected = false;
	public Color _NeutralColor = new Color(255f,255f,255f,55f);
	public Color _SelectedColor = new Color(50f,130f,255f,55f);

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
