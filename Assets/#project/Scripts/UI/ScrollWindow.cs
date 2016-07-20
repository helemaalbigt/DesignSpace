using UnityEngine;
using System.Collections;

public class ScrollWindow : MonoBehaviour {

	public RectTransform _Window;
	public RectTransform _Content;
	public ScrollBar _ScrollBar;
	
	// Update is called once per frame
	void Update () {
		_Content.localPosition = new Vector3 (
			_Content.localPosition.x,
			(_ScrollBar._Value) * (_Content.rect.height - _Window.rect.height),
			_Content.localPosition.z
		);

		//Debug.Log(_ScrollBar._Value +" - "+ (_Content.rect.height - (_Window.rect.height + _Window.localPosition.y)));
	}
}
