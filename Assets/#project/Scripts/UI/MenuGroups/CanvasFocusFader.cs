using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasFocusFader : MonoBehaviour {

	public CanvasGroup _CanvasGroup;
	public float _VisibilityAngle;
	public float _TransitionAngle;
	private Transform _Camera;

	// Use this for initialization
	void Start () {
		_Camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}

	// Update is called once per frame
	void Update () {
		float A = Vector3.Angle (_CanvasGroup.transform.forward, _Camera.forward);

        if(A > _VisibilityAngle)
        {
            _CanvasGroup.transform.gameObject.SetActive(false);
        }

		float alpha = 0;
		if (A <= _VisibilityAngle)
		{
            _CanvasGroup.transform.gameObject.SetActive(true);
            alpha = Mathf.InverseLerp (_VisibilityAngle, _VisibilityAngle - _TransitionAngle, A);
		}

		_CanvasGroup.alpha = alpha;
	}
}
