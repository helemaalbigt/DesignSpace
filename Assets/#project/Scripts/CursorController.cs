using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {

	public WandController _WandController;
	public WandController _WandControllerOther;
	public Transform _PointerAnchor;
	public MeshRenderer _Cursor;
	public LineRenderer _Line;

    public float _LineFactor;

	private bool locked = false;
	private float _StateChangeDistance = 0; //distance of cursor to controller on statechange - used for lockRad and lockRadXZ
	private Vector3 _StateChangePos;
	private Quaternion _StateChangeRot;
	public enum CursorState{
		unlocked, 	
		lockPos, 	//fixed in space
		lockRad, 	//fixed fistance from controller hold same rotation
		lockRadXZ, 	//fixed distance from controller but same Z distance
		lockRadNorm, //fixed distance and follow controller rotation
        maxDistance //don't go beyond fixed distance
	};
	public CursorState _CursorState = CursorState.unlocked;
	private CursorState _PrevCursorState;
    private float _MaxDistance = 1f;

	public Vector3 curPos;
	public Vector3 prevPos;

	void Start(){
		SetCursorState (CursorState.unlocked);
	}

	// Update is called once per frame
	void Update () {

		prevPos = curPos;
		curPos = transform.position;

        if (_WandController.rayHit || locked) {
           
            _Line.SetPosition (0, _PointerAnchor.position);
			_Line.SetPosition (1, _Cursor.transform.position);
			_Line.material.SetTextureOffset("_MainTex", new Vector2(Time.timeSinceLevelLoad*-1f,0f));
			_Line.material.SetTextureScale ("_MainTex", new Vector2((_Cursor.transform.position - _PointerAnchor.position).magnitude*_LineFactor, 1f));

			_Cursor.enabled = true;

			switch (_CursorState){

			case CursorState.lockRad:
				//position
				_Cursor.transform.parent = _PointerAnchor;
				_Cursor.transform.localPosition = new Vector3 (0f, 0f, _StateChangeDistance);
				_Cursor.transform.rotation = _StateChangeRot;
				//appearance
				_Cursor.materials [0].color = Color.red;
				_Line.material.color = Color.red;
				_Line.material.SetColor("_EmissionColor", Color.red);
			break;

			case CursorState.lockRadNorm:
				//position
				_Cursor.transform.parent = _PointerAnchor;
				_Cursor.transform.localPosition = new Vector3 (0f, 0f, _StateChangeDistance);
				_Cursor.transform.up = _PointerAnchor.position - _Cursor.transform.position;
				//appearance
				_Cursor.materials [0].color = Color.red;
				_Line.material.color = Color.red;
				_Line.material.SetColor("_EmissionColor", Color.red);
				break;

			case CursorState.lockRadXZ:
				//position
				_Cursor.transform.parent = _PointerAnchor;
				_Cursor.transform.localPosition = new Vector3 (0f, 0f, _StateChangeDistance);
				_Cursor.transform.position =  new Vector3( _Cursor.transform.position.x , _StateChangePos.y, _Cursor.transform.position.z);
				_Cursor.transform.rotation = _StateChangeRot;
				//appearance
				_Cursor.materials [0].color = Color.red;
				_Line.material.color = Color.black;
				_Line.material.SetColor("_EmissionColor", Color.black);
				_Cursor.transform.parent = null;
			break;

			case CursorState.lockPos:
				//appearance
				_Cursor.materials [0].color = Color.red;
				_Line.material.color = Color.black;
				_Cursor.transform.parent = null;
			break;

            case CursorState.maxDistance:
                //position
                _Cursor.transform.parent = _PointerAnchor; 
               
                if(_WandController.hitDistance < _MaxDistance && _WandController.rayHit)
                {
                    _Cursor.transform.position = _WandController.hitPos;
                    _Cursor.transform.rotation = Quaternion.FromToRotation(Vector3.up, _WandController.hitNorm);
                }
                else
                {
                    _Cursor.transform.localPosition = new Vector3(0f, 0f, _MaxDistance);
                    _Cursor.transform.up = _PointerAnchor.position - _Cursor.transform.position;
                    //_Cursor.transform.forward = Vector3.up;
                    //Quaternion looktAtController = Quaternion.LookRotation(_Cursor.transform.position - _PointerAnchor.position);
                    //_Cursor.transform.rotation = Quaternion.Euler(new Vector3(_Cursor.transform.eulerAngles.x, looktAtController.y, _Cursor.transform.eulerAngles.z));                               
                }
                //appearance
                _Cursor.materials [0].color = Color.black;
				_Line.material.color = Color.black;
				_Cursor.transform.parent = null;
			break;

			default:
				//position
				_Cursor.transform.position = _WandController.hitPos;
				_Cursor.transform.rotation = Quaternion.FromToRotation (Vector3.up, _WandController.hitNorm);
				//appearance
				_Cursor.materials [0].color = Color.black;
				_Line.material.color = Color.black;
				_Line.material.SetColor("_EmissionColor", Color.black);
				_Cursor.transform.parent = _PointerAnchor;
				break;
			}

		} else {
			//hide the cursor and the line
			_Cursor.enabled = false;
			//_Line.SetPosition (0, Vector3.zero);
			//_Line.SetPosition (1, Vector3.zero);
			_Line.SetPosition (0, _PointerAnchor.position);
			_Line.SetPosition (1, _PointerAnchor.position + _PointerAnchor.forward*10f);
			_Line.material.color = Color.black;
			_Line.material.SetColor("_EmissionColor", Color.black);
		}
	}

	public void SetCursorState(CursorState state){
		if (InputController.inUse)
			return; 
		
		_CursorState = state;

		if (state == CursorState.unlocked) {
			locked = false;
		} else {
			locked = true;
		}

		if (_PrevCursorState != _CursorState)
		{
			UpdateCursorTransformState ();
		}

		_PrevCursorState = _CursorState;
	}

	private void UpdateCursorTransformState(){
		_StateChangeDistance = _WandController.hitDistance;
		_StateChangePos = _Cursor.transform.position;
		_StateChangeRot = _Cursor.transform.rotation;
	}

	public void SetDrawLock(){
		UpdateCursorTransformState ();
		_StateChangeDistance = 0.01f;

		_CursorState = CursorState.lockRad;
		locked = true;
	}
}



