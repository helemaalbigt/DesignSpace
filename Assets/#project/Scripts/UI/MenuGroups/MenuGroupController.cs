using UnityEngine;
using System.Collections;

public class MenuGroupController : MonoBehaviour {

	private static GameObject[] _MenuGroups;

	// Use this for initialization
	void Start () {
		_MenuGroups = GameObject.FindGameObjectsWithTag("MenuGroup");
	}
	
	public static void EnableMenuGroups(){
		foreach (GameObject group in _MenuGroups)
		{
			if(group.transform.Find("DockingArea") != null)
				group.transform.Find("DockingArea").gameObject.SetActive (true);
		}
	}

	public static void DisableMenuGroups(){
		foreach (GameObject group in _MenuGroups)
		{
			if(group.transform.Find("DockingArea") != null)
				group.transform.Find("DockingArea").gameObject.SetActive (false);
		}
	}

	public static void RecalcTabDock(){
		foreach (GameObject group in _MenuGroups)
		{
			group.transform.Find("TabDock").GetComponent<TabDockPlacer>().SetTabDockPos ();
		}
	}


}
