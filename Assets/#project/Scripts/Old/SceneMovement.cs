/*
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using System.Collections;

public class SceneMovement : MonoBehaviour {

	[Header("General")]
	public Transform _Camera;
	public Transform _MoveWrapper;
	public Transform _ScaleWrapper;
	public Transform _RotateWrapper;
	public static bool IsMoving = false;
	public int _MouseButton = 2;

	//private CursorManager _CursorManager;
	private float _DistGrabbedObject = 0;
	private float _PrevFrameY;
	private float _StartScale;

	[Header("Rulers")]
	public float _SceneScale = 1;
	public float _ScaleMinLimit = 0.1248180f;
	public Material _BoxMaterial;
	public Transform[] _RulerUnits;
	public Text _UnitOfMeasurement;
	public float _BaseValue;
	public float _RulerTextCorrection = 8;
	public float _RulerWidthCorrection = 50;

	private float _PrevScale = 1;
	private float _ScaleSpeed = 1;
	private int _Iteration = 0;
	private float _UnitOfMeasurementFactor = 1;

	[Header("Scene Navigation")]
	public float _TranslateSpeedFactor = 0.1f;
	public float _RotateSpeedFactor = 0.1f;
	public float _ScaleSpeedFactor = 0.1f;
	[Range(0,1)] public float _MouseSensitvity = 1;


	void Start (){
		//_CursorManager = GameObject.FindGameObjectWithTag ("Cursor").GetComponent<CursorManager> ();

		_StartScale = _ScaleWrapper.localScale.x;
		_BoxMaterial.mainTextureScale = new Vector2 (1,1);
		UpdateBox ();
		UpdateRuler ();
	}

	void Update () {
		//Debug.Log ("MouseX: "+Input.GetAxis ("Mouse X")+", Mouse Y: "+Input.GetAxis ("Mouse Y"));

		if (Input.GetMouseButton (_MouseButton)) {
			IsMoving = true;
			TransformScene ();
			UpdateBox ();
			UpdateRuler ();
		} else {
			IsMoving = false;
		}
			
	}


	private void TransformScene(){
		//translate X,Z
		if (!Input.GetKey (KeyCode.LeftControl) && !Input.GetKey (KeyCode.LeftShift)) {
			_MoveWrapper.Translate (_MouseSensitvity * _TranslateSpeedFactor * Input.GetAxis ("Mouse X"), 0, _MouseSensitvity * _TranslateSpeedFactor * Input.GetAxis ("Mouse Y"));
			_CursorManager.SetCursor("moveXZ");
		}

		//translate Y
		if (Input.GetKey (KeyCode.LeftShift) && !Input.GetKey (KeyCode.LeftControl)) {
			//MOUSE
			_MoveWrapper.Translate (0, _MouseSensitvity * _TranslateSpeedFactor * Input.GetAxis ("Mouse Y"), 0);
			_CursorManager.SetCursor("moveY");
		}

		//rotate
		if (!Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.LeftControl)) {
			_RotateWrapper.RotateAround (transform.position, Vector3.up, -1 * _MouseSensitvity * _RotateSpeedFactor * Input.GetAxis ("Mouse X")); //transform.GetComponent<Renderer>().bounds.center
			_CursorManager.SetCursor("rotate");
		}

		//scale
		if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.LeftControl)) {
			//Debug.Log ( Mathf.Clamp (Input.GetAxis("Mouse X"), -1, 1) + 1);
			_ScaleWrapper.localScale *= Mathf.Clamp (Input.GetAxis ("Mouse X"), -1, 1) * _MouseSensitvity * _ScaleSpeedFactor + 1;
			_SceneScale = _ScaleWrapper.localScale.x / _StartScale;

			if (_SceneScale < _ScaleMinLimit) {
				_SceneScale = _ScaleMinLimit;
				_ScaleWrapper.localScale = new Vector3( _SceneScale*_StartScale, _SceneScale*_StartScale, _SceneScale*_StartScale);
			}

			_CursorManager.SetCursor("scale");
		}
	}

	private void UpdateBox(){
		
		float texScale = GetTextureScale(_SceneScale);
		if (_SceneScale != 1) {
			//float _modSceneScale = (_SceneScale > 1) ? 1 + (_SceneScale - 1 )%1 : (_SceneScale + 1)%1;
			_BoxMaterial.mainTextureScale = new Vector2(texScale, texScale);
		}

		//_BoxMaterial.mainTextureOffset = new Vector2(- _MoveWrapper.position.x *20f, - _MoveWrapper.position.z *20f);
	}


	//Get texture scale and the factor
	private float GetTextureScale(float scale){


		float relativeScale = _PrevScale + ((scale - _PrevScale) * _ScaleSpeed);
		_PrevScale = scale;

		float textureScale = 1 / relativeScale;
		float scaleSpeed = 1f;
		float baseValue = 10;

		int iteration = 0;
		bool done = false;
		if (textureScale < 1) {
			while (!done) {
				//devide by 2 (eg: 10 -> 5)
				if (iteration % 2 == 0) {
					if (textureScale < 0.5f) {
						textureScale *= 2;
						scaleSpeed /= 2;
						iteration++;
					} else {
						done = true;
					}
				} 
				//devide by 5 (eg: 5 -> 1)
				else {
					if (textureScale < 0.2f) {
						textureScale *= 5;
						scaleSpeed /= 5;
						iteration++;
					} else {
						done = true;
					}
				}
			}
		} else {
			while (!done) {

				if (textureScale > 2) {
					textureScale /= 2;
					scaleSpeed *= 2;
					iteration++;
				} else {
					done = true;
				}
			}
		}

		_ScaleSpeed = scaleSpeed;
		_Iteration = iteration;

		return textureScale;
	}

	private void UpdateRuler(){
		
		for (int i = 0; i < _RulerUnits.Length; i++) {

			//position
			float xOffset = (10 * (i + 1) * _SceneScale);
			//value
			float val = _BaseValue * (i + 1);

			if (_SceneScale > 1 && _Iteration > 0) 
			{
				if (_Iteration % 2 != 0) {
					float factor = (_Iteration + 1) / 2;
					float denom2 = (_Iteration == 0) ? 1f : Mathf.Pow(2, factor) ; 
					float denom5 = ((_Iteration-2) <= 0) ? 1f : Mathf.Pow(5, (factor-1)) ;

					xOffset /= denom2;
					val /= denom2;

					xOffset /= denom5;
					val /= denom5;
				} else {
					float factor = (_Iteration) / 2;
					float denom2 = ((_Iteration - 1) == 0) ? 1f :  Mathf.Pow(2, factor);
					float denom5 = ((_Iteration - 1) == 0) ? 1f :  Mathf.Pow(5, factor);

					xOffset /= denom2;
					val /=  denom2;

					xOffset /= denom5;
					val /= denom5;
				}
			}

			if (_SceneScale < 1 && _Iteration > 0)
			{
				if (_Iteration % 2 != 0) {
					float factor = (_Iteration + 1) / 2;
					float denom2 = (_Iteration == 0) ? 1f : Mathf.Pow(2, factor) ; 
					float denom5 = ((_Iteration-2) <= 0) ? 1f : Mathf.Pow(5, (factor-1)) ;

					xOffset *= denom2;
					val *= denom2;

					xOffset *= denom5;
					val *= denom5;
				} else {
					
					float factor = (_Iteration) / 2;
					float denom5 = ((_Iteration - 1) == 0) ? 1f :  Mathf.Pow(5, factor);
					float denom2 = ((_Iteration - 1) == 0) ? 1f :  Mathf.Pow(2, factor);

					xOffset *= denom2;
					val *=  denom2;

					xOffset *= denom5;
					val *= denom5;
				}
				
			}

			//unit adjustment
			if (i == 0) {
				if (val  <= 1) {
					_UnitOfMeasurementFactor = 10;
					_UnitOfMeasurement.text = "mm";
				} else if (val >= 20) {
					_UnitOfMeasurementFactor = 0.01f;
					_UnitOfMeasurement.text = "m";
				} else {
					_UnitOfMeasurementFactor = 1f;
					_UnitOfMeasurement.text = "cm";
				}
			}

			val *= _UnitOfMeasurementFactor;

			//position
			_RulerUnits [i].GetComponent<RectTransform> ().localPosition = new Vector3 (xOffset - _RulerTextCorrection - _RulerWidthCorrection, _RulerUnits [i].localPosition.y, _RulerUnits [i].localPosition.z);

			//value
			Text unitValue = _RulerUnits [i].GetComponent<Text> (); //Debug.Log (val);
			unitValue.text = "" + val;

			//visibility
			if (xOffset < 100) {
				_RulerUnits [i].gameObject.SetActive (true);
			} else {
				_RulerUnits [i].gameObject.SetActive (false);
			}

		}
	}
}*/