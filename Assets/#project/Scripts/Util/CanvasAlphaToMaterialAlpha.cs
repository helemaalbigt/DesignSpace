using UnityEngine;
using System.Collections;

public class CanvasAlphaToMaterialAlpha : MonoBehaviour {

    public CanvasGroup _canvasGroup;

	// Update is called once per frame
	void Update () {
        Color color = GetComponent<Renderer>().material.color;
        color.a = _canvasGroup.alpha;
        GetComponent<Renderer>().material.color = color;
	}
}
