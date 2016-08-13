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

[CustomEditor(typeof(Zone2DPoint))]
public class Zone2DPointInspector : Editor {
	
	
	public override void OnInspectorGUI()
	{
		Zone2DPoint zone = (Zone2DPoint) target;
		zone.LookForTheFather ();
		if (zone.father == null) {

			if(zone.father==null)
			EditorGUILayout.HelpBox("No father define !!! Please add this point in a gameobject with a Zone2D script", MessageType.Error);
		}
		else{
			EditorGUILayout.HelpBox("Width: "+zone.Width +" m", MessageType.Info);
			EditorGUILayout.HelpBox("Height: "+zone.Height +" m", MessageType.Info);
			
			
		}
	}
}
