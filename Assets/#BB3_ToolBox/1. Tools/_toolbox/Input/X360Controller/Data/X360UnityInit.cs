/*
 * --------------------------BEER-WARE LICENSE--------------------------------
 * PrIMD42@gmail.com wrote this file. As long as you retain this notice you
 * can do whatever you want with this code. If you think
 * this stuff is worth it, you can buy me a beer in return, 
 *  S. E.
 * Donate a beer: http://www.primd.be/donate/ 
 * Contact: http://www.primd.be/
 * ----------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

public class UnityX360Init :  I_X360InitData {

	I_X360Data data = new X360ControllerData();
	
	public string xAxisLeft="X axis";
	public string yAxisLeft="Y axis";
	
	public string xAxisRight="5th axis";
	public string yAxisRight="4th axis";
	
	public string xAxisArrow="6th axis";
	public string yAxisArrow="7th axis";
	
	public string triggers="3rd axis triggers";


	public void RefreshData ()
	{
		if(data!=null)
		{

			Refresh_A();
			Refresh_B();
			Refresh_X();
			Refresh_Y();

			Refresh_LB();
			Refresh_RB();

			Refresh_Trigger();

			Refresh_MENU();
			Refresh_START();
			Refresh_LEFT_PULL();
			Refresh_RIGHT_PULL();

			Refresh_LEFT();
			Refresh_RIGHT();
			Refresh_ARROWS();
		

		}
	}

	
	public void Refresh_LEFT()
	{

		Direction d = null;
		d = data.GetLeft();
		try{
			d.SetX(Input.GetAxis(xAxisLeft));
		}catch(UnityException e)
		{
			
			Debug.LogWarning (GetAxeWarning(xAxisLeft)+e);
		}
		try{
			d.SetY(Input.GetAxis(yAxisLeft));
		}catch(UnityException e)
		{
			
			Debug.LogWarning (GetAxeWarning(yAxisLeft)+e);
		}

	}
	
	public void Refresh_RIGHT()
	{
		Direction d = null;
		d = data.GetRight();
		try{
			d.SetX(Input.GetAxis(xAxisRight));
		}catch(UnityException e)
		{
			
			Debug.LogWarning (GetAxeWarning(xAxisRight)+e);
		}
		try{
			d.SetY(Input.GetAxis(yAxisRight));
		}catch(UnityException e)
		{

			Debug.LogWarning (GetAxeWarning(yAxisRight)+e);
		}


	}
	
	public void Refresh_ARROWS()
	{
		Direction d = null;
		d = data.GetArrows();
		try{
			d.SetX(Input.GetAxis(xAxisArrow));
		}catch(UnityException e)
		{
			Debug.LogWarning (GetAxeWarning(xAxisArrow)+e);	
		}
		try{
			d.SetY(Input.GetAxis(yAxisArrow));
		}catch(UnityException e)
		{
			Debug.LogWarning (GetAxeWarning(yAxisArrow)+e);
		}

	}
	private static string GetAxeWarning(string axe)
	{
		return "Please define the axe \""+axe+"\"  in the Unity3D INPUTMANAGER to be able to track this data.";
	}

	public void Refresh_A()
	{
		data.SetA(Input.GetKey(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton0));
		
	}
	public void Refresh_Y()
	{
		data.SetY(Input.GetKey(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.JoystickButton3));

	}
	public void Refresh_X()
	{
		data.SetX(Input.GetKey(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.JoystickButton2));

	}
	public void Refresh_B()
	{
		data.SetB(Input.GetKey(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.JoystickButton1));

	}

	
	public void Refresh_LT()
	{
		Refresh_Trigger();

	}
	public void Refresh_RT()
	{
		Refresh_Trigger();
	}

	private void Refresh_Trigger(){	
		float v = Input.GetAxis(triggers);
		if( v<0) data.SetLt(-v);
		else if (v>0) data.SetRt(v);
		else {data.SetLt(0);data.SetRt(0);}
	}
	public void Refresh_LB()
	{
		data.SetLb(Input.GetKey(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.JoystickButton4));

	}
	public void Refresh_RB()
	{
		data.SetRb(Input.GetKey(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.JoystickButton5));

	}

	
	public void Refresh_MENU()
	{
		data.SetMenu(Input.GetKey(KeyCode.JoystickButton6) || Input.GetKeyDown(KeyCode.JoystickButton6));

	}
	public void Refresh_START()
	{
		data.SetStart(Input.GetKey(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.JoystickButton7));

	}

	public void Refresh_LEFT_PULL()
	{
		data.SetLeftPull(Input.GetKey(KeyCode.JoystickButton8) || Input.GetKeyDown(KeyCode.JoystickButton8));

	}
	public void Refresh_RIGHT_PULL()
	{
		data.SetRightPull(Input.GetKey(KeyCode.JoystickButton9) || Input.GetKeyDown(KeyCode.JoystickButton9));

	}

	public I_X360Data GetData ()
	{
		return data;
	}
}
