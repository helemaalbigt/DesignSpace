using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuRadioButton: MenuCheckbox {
	
	protected List<MenuRadioButton> _Buttons =  new List<MenuRadioButton>() ;
	public bool _AlwaysOneSelected = false;

    public void Start(){
		base.Start ();

        RefreshButtonsList();

        if (_IsSelected)
			Select ();
	}

	public void Awake(){
        RefreshButtonsList();
    }

    private void RefreshButtonsList()
    {
        _Buttons.Clear();

        foreach (Transform button in transform.parent)
        {
            if (button.GetComponent<MenuRadioButton>() != null)
            {
                _Buttons.Add(button.GetComponent<MenuRadioButton>());
            }
        }
    }

	public void HoverOn(WandController C){
		
		if (C.triggerDown)
		{
			if (!_IsSelected)
			{
				Select ();
			} else
			{
				if(!_AlwaysOneSelected)
					Deselect ();
			}
		}


		//copypasta from parent parent class - resolve this better in the future
		//*_LastHoverTimestamp = Time.time;
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
		
	public void Select(){
		base.Select ();

		foreach (MenuRadioButton button in _Buttons)
		{
			if(button != this)
				button.Deselect ();
		}
	}

}
