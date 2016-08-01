using UnityEngine;
using System.Collections;

public class PairTransform : MonoBehaviour {

    public Transform _Target;	

	// Update is called once per frame
	void Update () {
        transform.position = _Target.position;
        transform.rotation = _Target.rotation;
	}
}
