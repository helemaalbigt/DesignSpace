using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Text;

[CanEditMultipleObjects, CustomEditor(typeof(PointToPointMeasure))]
public class PointToPointInspector : Editor {
	

	public override void OnInspectorGUI(){


		
		if ( targets !=null && targets.Length > 1) {
						float dist = 0f;

			foreach (Object gamo in (Object []) targets ) {
				PointToPointMeasure point=((PointToPointMeasure) gamo);
				//TODO add a control that check if that sepcial case like next are only count one= A->B B->A
				if(point.otherPoint!=null )
						dist+= point.GetDistance();

			}
			EditorGUILayout.HelpBox (string.Format ("Distance: {0,-10:F} m", dist), MessageType.Info);
				} else {
			base.OnInspectorGUI ();
						PointToPointMeasure ptp = (PointToPointMeasure)target;
						if (ptp.ratioMeterPerUnit == 1f)
								EditorGUILayout.HelpBox (string.Format ("Distance: {0,-10:F} m", ptp.ApplyRatio (ptp.GetDistance ())), MessageType.Info);
						else
								EditorGUILayout.HelpBox (string.Format ("Distance: {0,-10:F} m ({1,-10:F})", ptp.ApplyRatio (ptp.GetDistance ()), ptp.GetDistance ()), MessageType.Info);
	
						if (ptp.otherPoint == null) {
        
								if (GUILayout.Button ("Auto complete")) {
										Transform nearestPTP = null;
										float distance = 10000f;

										if (ptp.transform.parent != null)
												foreach (PointToPointMeasure point in ptp.transform.parent.GetComponentsInChildren<PointToPointMeasure>() as PointToPointMeasure []) {
														if (ptp != point) {
																float d = Vector3.Distance (ptp.transform.position, point.transform.position);
																if (d < distance && point.otherPoint != ptp) {
																		distance = d;
																		nearestPTP = point.transform;
																}
														}
												}

										if (nearestPTP == null) {
												foreach (PointToPointMeasure point in GameObject.FindObjectsOfType<PointToPointMeasure>() as PointToPointMeasure []) {
														if (ptp != point) {
																float d = Vector3.Distance (ptp.transform.position, point.transform.position);
																if (d < distance && point.otherPoint != ptp) {
																		distance = d;
																		nearestPTP = point.transform;

																}
														}
												}

										}

										ptp.otherPoint = nearestPTP;
										ptp.Display ();
								}
						}
				}
	}
}


