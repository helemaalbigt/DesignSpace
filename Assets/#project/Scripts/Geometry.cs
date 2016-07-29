using UnityEngine;
using System.Collections;

public class Geometry : MonoBehaviour{

	public float _HoverOffTime = 0.1f;
	private float _LastHoverTimestamp = -99999;

	private bool isActive = false;
	public bool IsActive {
		get{return isActive;}
		set{ 
			isActive = value;
			if (isActive)
			{
				_ObjectBounds.EnableBounds(transform, _ObjectBounds._ColorSelected);
			} else
			{
				_ObjectBounds.DisableBounds(transform);
			}
		}
	}

	private bool isHoveredOn = false;
	public bool IsHoveredOn {
		get{ return isHoveredOn; }
		set{ 
			isHoveredOn = value;
			if (!IsActive)
			{
				if (isHoveredOn)
				{
					StartCoroutine (HoverOffCount ());
					_ObjectBounds.EnableBounds (transform, _ObjectBounds._ColorHover);
				} else
				{
					_ObjectBounds.DisableBounds (transform);
				}
			}
		}
	}

	private ObjectBounds _ObjectBounds;

	// Use this for initialization
	void Start () {
		_ObjectBounds = GameObject.FindGameObjectWithTag("GameController").GetComponent<ObjectBounds>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HoverOn(){
		_LastHoverTimestamp = Time.time;
		if (!IsHoveredOn)
		{
			IsHoveredOn = true;
		}
	}

	IEnumerator HoverOffCount(){
		while (Time.time - _LastHoverTimestamp < _HoverOffTime)
		{
			yield return null;
		}

		HoverOff ();
	}

	public void HoverOff(){
		IsHoveredOn = false;
	}
}
