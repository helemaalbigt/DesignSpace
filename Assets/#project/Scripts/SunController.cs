using UnityEngine;
using System.Collections;

public class SunController : MonoBehaviour {
	
	public Transform _GlobalWrapper;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler(0, _GlobalWrapper.rotation.eulerAngles.y, 0);
	}
}
