/*
 * --------------------------BEER-WARE LICENSE--------------------------------
 * PrIMD42@gmail.com wrote this file. As long as you retain this notice you
 * can do whatever you want with this code. If you think
 * this stuff is worth it, you can buy me a beer in return, 
 *  S. E.
 * Donate a beer: http://www.primd.be/donate/ 
 * Contact: http://www.primd.be/
 * ----------------------------------------------------------------------------
 */

using UnityEngine;
using System;
using System.Collections.Generic;

public class MouseButton : MonoBehaviour
{
	public enum ButtonType:int { Left=0,Middle=1,Right=2,Button_3=3,Button_4=4,Other=-1}
	public ButtonType buttonType = ButtonType.Left;
	public int buttonNumber =0;

	public int GetNumButtonType(){
		if (buttonType == ButtonType.Other) {
						return buttonNumber;		
				} else
						return (int) buttonType;
	}
	private static MouseButton INSTANCE ;
	public  static MouseButton  GetInstance() {return INSTANCE;}
	
	void Awake()
	{
		if(INSTANCE==null) INSTANCE=this;
		else {
			Debug.LogError("Only one Input listener is accepted by scene, this one is delete:"+this +" ("+this.gameObject+")");
			Destroy(this);
		}
		
	}
	
	public float doubleClickTime =0.5f;
	public float doubleClickDelay =1f;

	public float time;
	public float x, y;
	
	public float lastPressed;
	public Vector2 lastPressedPos  = new Vector2();

	public float lastPressing;
	public Vector2 lastPressingPos = new Vector2();

	public float lastReleased;
	public Vector2 lastReleasedPos = new Vector2();

	protected float lastClick;
	protected float thisClick;


	public float slidePathRecordFrequence =0.1f;
	private float lastSlideRecord;

	public bool recordSlideMove=false;
	public LinkedList <Vector2> slide = new LinkedList<Vector2>();
	public LinkedList <Vector2> lastSlide = new LinkedList<Vector2>();

	public bool isPressing;
	public bool wasDoubleClick;
	public float lastDoubleClickTime;






	void Update()
	{
		//save basic data
		time = Time.timeSinceLevelLoad;
		x = Input.mousePosition.x;
		y = Screen.height-Input.mousePosition.y;
		isPressing =Input.GetMouseButton(GetNumButtonType()) || Input.GetMouseButtonDown(GetNumButtonType());

		// if the user press the screen
		if (  Input.GetMouseButtonDown (GetNumButtonType()) ) {
			

			
			//Switch the time to be able to determine if there was a double click
			lastClick=thisClick;
			thisClick=time;

			if(IsDoubleClick(doubleClickTime) && (time-lastDoubleClickTime)>doubleClickDelay)
			{
				lastDoubleClickTime= time;
				wasDoubleClick=true;
			}
			else wasDoubleClick= false;

			
			// Set new position of the last click
			lastPressedPos.x=x;
			lastPressedPos.y=y;
			lastPressed =time;
			

			//switcher and clean the slide recorder
			if(recordSlideMove)
			{
				lastSlideRecord = time;

				lastSlide.Clear();
				MouseButton.Transfert(ref slide, ref lastSlide);
				slide.Clear();
				//Add the entry point
				slide.AddLast( new Vector2(x,y));
			}	
			
		}

		
	
		// if the user release the screen
		if (Input.GetMouseButtonUp (GetNumButtonType())) {
			
			// Set new position of the last click
			lastReleasedPos.x=x;
			lastReleasedPos.y=y;
			lastReleased = time;

			//add the out point of the path
			if(recordSlideMove)
			{
				slide.AddLast( new Vector2(x,y));
			}
			
		}

		if(Input.GetMouseButton(GetNumButtonType()))
		{
			
			//Add the path points
			if(recordSlideMove  && time-lastSlideRecord>slidePathRecordFrequence)
			{
				lastSlideRecord = time;
				slide.AddLast( new Vector2(x,y));
				
			}

		}
		if(isPressing)
		{
			lastPressingPos.x=x;
			lastPressingPos.y=y;
			lastPressing=time;
		}
	


	}





	
	public float GetLastPressed(){return lastPressed;}
	public float GetLastPressing(){return lastPressing;}
	public float GetLastReleased(){return lastReleased;}
	
	public Vector2 GetLastPressedPosition(){return lastPressedPos;}
	public Vector2 GetLastPressingPosition(){return lastPressingPos;}
	public Vector2 GetLastReleasedPosition(){return lastReleasedPos;}

	public bool IsPressing(){return isPressing; }

	public LinkedList <Vector2> GetSlide()
	{
		return slide;
	}
	public LinkedList <Vector2> GetLastSlide()
	{
		return lastSlide;
	}

	
	private bool IsDoubleClick (float timeBetween)
	{
		return thisClick - lastClick < timeBetween;
	}
	public bool WasDoubleClick()
	{
		return wasDoubleClick;
	}
	
	public bool IsSliding (float distMinToBeSlide)
	{
		return Sliding (lastPressedPos, lastPressingPos, distMinToBeSlide);
	}

	
	public bool WasSliding (float distMinToBeSlide)
	{
		return Sliding (lastPressedPos, lastReleasedPos, distMinToBeSlide);
	}

	private bool Sliding( Vector3 o, Vector3 d, float dist)
	{
		float distance = Vector3.Distance(d, o);
		return distance>dist;
	}



	public Vector2 GetDirection(bool directionNormalized)
	{
		
		return GetDirection(lastPressedPos ,lastPressingPos, directionNormalized) ;
	}
	
	public Vector2 GetLastDirection(bool directionNormalized)
	{
		
		return GetDirection(lastPressedPos ,lastReleasedPos, directionNormalized) ;
		
	}

	public Vector2 GetDirectionOnScreen(bool directionNormalized)
	{
	
			// If the user do not use the mouse, there is no direction
			if(!isPressing) return Vector2.zero;
			
			Vector2 centerOfScreen = new Vector2();
			centerOfScreen.x = Screen.width/2f;
			centerOfScreen.y = Screen.height/2f;
			return GetDirection(centerOfScreen ,lastPressingPos, directionNormalized) ;

	}

	private Vector2 GetDirection(Vector2 o, Vector2 d, bool directionNormalized)
	{

		Vector2 val = o - d;
		val.x=-val.x;
		
		//normelize
		if(directionNormalized){
			val.x = val.x/(Screen.width/2f);
			val.y = val.y/(Screen.height/2f);
		}
		return val ;

	}



	
	private static void Transfert( ref LinkedList<Vector2> from, ref LinkedList<Vector2> to)
	{
		foreach( Vector2 v in from)
		{
			to.AddLast(v);
		}
	}  
}

