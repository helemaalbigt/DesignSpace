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

[CustomEditor(typeof(Zone2D))]
public class Zone2DInspector : Editor {
	

	public override void OnInspectorGUI(){
		base.OnInspectorGUI ();
		
		Zone2D zone = (Zone2D)target;
		if(zone.IsValide()){
			EditorGUILayout.HelpBox(string.Format ("Width: {0,-10:F} m",zone.GetWidthInMeter() ), MessageType.Info);
			EditorGUILayout.HelpBox(string.Format ("Height: {0,-10:F} m" ,zone.GetHeightInMeter()), MessageType.Info);
			

		}
	}
}
