using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuToggle : MonoBehaviour {

	Transform _Tab;

	void Start(){
		_Tab = transform.Find ("TabContents");
	}

	public void TurnOn(){
		MenuToggle thisToggle = GetComponent<MenuToggle> ();

		if (transform.parent != null)
		{
			MenuToggle[] toggles = transform.parent.GetComponentsInChildren<MenuToggle> ();

			foreach (MenuToggle tog in toggles)
			{
				tog._Tab.gameObject.SetActive (false);
			}
		}

		thisToggle._Tab.gameObject.SetActive(true);
	}
}
