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
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class Refresher : MonoBehaviour {



	private static Refresher INSTANCE ;
	public  static Refresher GetInstance() {return INSTANCE;}

	void Awake()
	{
		if(INSTANCE==null) INSTANCE=this;
		else {
			Debug.LogError("Only one refresher is accepted by scene, this one is delete:"+this +" ("+this.gameObject+")");
			Destroy(this);
		}

		GameObject[] objs = GameObject.FindObjectsOfType<GameObject>() as GameObject [];


		Component [] tabr  = null;
//		int i =0;
		foreach(GameObject go  in objs)
		{

			tabr = (Component [] ) go.GetComponents(typeof(Refreshable))  ;
			if(tabr!=null){
				foreach( Refreshable r in tabr)
					if (r!=null)
						Add(r, r.GetRefreshType());
			}
		}


	}

	private float time;
	private float executionAverageTime;
	private float timeBetweenUpdate;
	public float GetAverage(){return executionAverageTime;}
	public float GetAverageBetweenUpdate(){return timeBetweenUpdate;}
	
	public float sometime =1.5f;
	private float lastSometime ;
	private LinkedList<Refreshable> listSometime = new LinkedList<Refreshable>();

	public readonly float eachsecond =1f;
	private float lastEachsecond ;
	private LinkedList<Refreshable> listEachSecond = new LinkedList<Refreshable>();
	
	public float often =0.3f;
	private float lastOften;
	private LinkedList<Refreshable> listOften = new LinkedList<Refreshable>();

	public float quick =0.1f;
	private float lastQuick;
	private LinkedList<Refreshable> listQuick = new LinkedList<Refreshable>();

	public float update =0.03f;
	private float lastUpdate;
	private LinkedList<Refreshable> listUpdate = new LinkedList<Refreshable>();


	void FixedUpdate () {
		time = Time.timeSinceLevelLoad;
		float iT = ((float) DateTime.Now.Millisecond)/1000f;
		float tmpPassed=0f;

		tmpPassed= time-lastEachsecond;
		if( tmpPassed>=eachsecond)
		{
			lastEachsecond=time;
			Warn(listEachSecond,time);
		}

		
		tmpPassed= time-lastSometime;
		if( tmpPassed>sometime)
		{
			lastSometime=time;
		//	print ("Tic, tac   "+ time);
			Warn(listSometime,time);
		}

		
		tmpPassed= time-lastOften;
		if( tmpPassed>often)
		{
			lastOften=time;
			Warn(listOften,time);
		}

		
		tmpPassed= time-lastQuick;
		if( tmpPassed>quick)
		{
			lastQuick=time;
			Warn(listQuick,time);
		}
		
		tmpPassed= time-lastUpdate;
		if( tmpPassed>update)
		{
			lastUpdate=time;
			Warn(listUpdate,time);
		}
		

		//if(UnityEngine.Random.Range(0,9000)==1)	System.Threading.Thread.Sleep(10000);
		float eT = ((float) DateTime.Now.Millisecond)/1000f;
		executionAverageTime= (executionAverageTime+ (eT-iT))/2f;
		timeBetweenUpdate= (timeBetweenUpdate+ Time.deltaTime)/2f;
		//Debug.Log(eT+"-"+iT + " == "+executionAverageTime);
	}


	public void Add(Refreshable listener, RefreshType type)
	{

		switch(type)
		{
			
		case RefreshType.UpdateLike: listUpdate.AddFirst(listener); break;
		case RefreshType.Quick: listQuick.AddFirst(listener); break;
		case RefreshType.Often: listOften.AddFirst(listener); break;
		case RefreshType.EachSecond: listEachSecond.AddFirst(listener); break;
		case RefreshType.Sometime: listSometime.AddFirst(listener); break;
		}
	}



	void  Warn(LinkedList<Refreshable> toRefresh, float time)
	{
		foreach(Refreshable r  in toRefresh)
			r.Refresh(time);
	}

	public int GetLenght(RefreshType type)
	
	{
		
		switch(type)
		{
		case RefreshType.UpdateLike: return listUpdate.Count; 
		case RefreshType.Quick: return listQuick.Count; 
		case RefreshType.Often: return  listOften.Count; 
		case RefreshType.EachSecond: return  listEachSecond.Count; 
		case RefreshType.Sometime: return  listSometime.Count; 
		}
		return 0;

	}


	public enum RefreshType{ Sometime, EachSecond, Often, Quick,UpdateLike, NotNow}
	

}




public interface Refreshable 
{
	
	void Refresh( float time);
	Refresher.RefreshType GetRefreshType();
}


