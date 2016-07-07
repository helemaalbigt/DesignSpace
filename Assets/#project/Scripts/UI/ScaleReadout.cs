using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScaleReadout : MonoBehaviour {

	public Transform _GlobalWrapper;
	public Text _Output;
	
	// Update is called once per frame
	void Update () {
		_Output.text = "1:" + Mathf.RoundToInt((1f / _GlobalWrapper.localScale.x));
	}
}
