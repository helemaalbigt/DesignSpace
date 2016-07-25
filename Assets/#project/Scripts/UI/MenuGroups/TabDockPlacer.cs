using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TabDockPlacer : MonoBehaviour {

	public Transform _MenuDock;
	public int _Margin;
	public float _MenuDockSizeRestriction = 5f;
	public float _MenuDockButtonSize = 55f;

	public bool _Active = true;

	// Use this for initialization
	void Start () {
		//Invoke ("SetTabDockPos", .5f);
		SetTabDockPos();
	}

	public void SetTabDockPos(){
		if (_Active)
		{
			int factor = (int)Mathf.CeilToInt ((_MenuDock.childCount / _MenuDockSizeRestriction));

			GetComponent<RectTransform> ().anchoredPosition = new Vector2 (
				factor * _MenuDockButtonSize + _Margin, 
				GetComponent<RectTransform> ().anchoredPosition.y
			);
		}
	}
}
