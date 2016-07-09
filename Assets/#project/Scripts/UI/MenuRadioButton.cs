using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuRadioButton: MenuCheckbox {
	
	protected List<MenuRadioButton> _Buttons =  new List<MenuRadioButton>() ;

	public void Awake(){
		foreach (Transform button in transform.parent)
		{
			if (button.GetComponent<MenuRadioButton> () != null)
			{
				_Buttons.Add(button.GetComponent<MenuRadioButton> ());
			}
		}
	}

	public void HoverOn(WandController C){
		if (C.triggerDown)
		{
			if (!_IsSelected)
			{
				Select();
			}
		}

	}
		
	public void Select(){
		base.Select ();

		foreach (MenuRadioButton button in _Buttons)
		{
			if(button != this)
				button.Deselect ();
		}
	}

}
