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
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MouseButton))]
public class MouseButtonInspector : Editor {
	
	
	public override void OnInspectorGUI(){
		base.OnInspectorGUI ();
		
		MouseButton zone = (MouseButton)target;

		if (! (zone.buttonType == MouseButton.ButtonType.Other) ) {
			zone.buttonNumber= (int) zone.buttonType;		
		}
	}
}
