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
using System;

public class X360  {

	public static  UnityX360Init x360 = new UnityX360Init();

	public static  I_X360Data GetData()  
	{
		return x360.GetData();
	}

	public static bool IsA ()
	{
		x360.Refresh_A();
		return x360.GetData().IsA();
	}

	public static bool IsB ()
	{
		x360.Refresh_B();
		return x360.GetData().IsB();
	}

	public static bool IsX ()
	{
		
		x360.Refresh_X();
		return x360.GetData().IsX();
	}

	public static bool IsY ()
	{
		x360.Refresh_Y();
		return x360.GetData().IsY();
	}

	public static bool IsLb ()
	{
		x360.Refresh_LB();
		return x360.GetData().IsLb();
	}
    
    public static bool IsRb ()
	{
		x360.Refresh_RB();
		return x360.GetData().IsRb();
	}

	public static bool IsMenu ()
	{
		x360.Refresh_MENU();
		return x360.GetData().IsMenu();
	}

	public static bool IsStart ()
	{
		x360.Refresh_START();
		return x360.GetData().IsStart();
	}

	public static float GetLt ()
	{
		x360.Refresh_LT();
		return x360.GetData().GetLt();
	}

	public static bool IsLt ()
	{
		x360.Refresh_LT();
		return x360.GetData().IsLt();
	}

	public static float GetRt ()
	{
		x360.Refresh_RT();
		return x360.GetData().GetRt();
	}

	public static bool IsRt ()
	{
		x360.Refresh_RT();
		return x360.GetData().IsRt();
	}

	public static bool IsLeftPull ()
	{
		x360.Refresh_LEFT_PULL();
		return x360.GetData().IsLeftPull();
	}

	public static bool IsRightPull ()
	{
		x360.Refresh_RIGHT_PULL();
		return x360.GetData().IsRightPull();
	}

	public static Direction GetLeft ()
	{
		x360.Refresh_LEFT();
		return x360.GetData().GetLeft();
	}

	public static Direction GetRight ()
	{
		x360.Refresh_RIGHT();
		return x360.GetData().GetRight();
	}

	public static Direction GetArrows ()
	{
		x360.Refresh_ARROWS();
		return x360.GetData().GetArrows();
	}
}
