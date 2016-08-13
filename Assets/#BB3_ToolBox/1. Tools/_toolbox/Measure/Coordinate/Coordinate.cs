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
using System.Collections;

public class Coordinate  {
	private Coordinate()
	{throw new UnityException("No reason to be instanciable");}

	public static int min=-1000;
	public static int max=1000;

	
	/**Return a random point in the scene between Coordinate.min and .max distance of zero*/
	public static Vector3 GetRandomCoordinate(){
		Vector3 r = new Vector3();
		r.x = Random.Range(min,max);
		r.y = Random.Range(min,max);
		r.z = Random.Range(min,max);
		return r;
	}
	
	/**Return a random point in the scene between Coordinate.min and .max distance of zero. Plus it will be in the 2D zone given*/
	public static Vector3 GetRandomCoordinateInZone( Zone2D zone){
		Vector3 r = new Vector3();
		r.x = Random.Range(min,max);
		r.y = Random.Range(min,max);
		r.z = Random.Range(min,max);
		if( zone!=null)
		zone.IsOutOfTheMapWithRectif(ref r);
		return r;
	}
	/**
	//Return modify the pt in aim to place it at the ground location
	public static bool GetGroundAt(ref Vector3 pt, int fromHeightToGround =200, string groundLayoutName="Ground"){
	
		RaycastHit hit ;
		Vector3 startPoint = new Vector3(pt.x,fromHeightToGround,pt.z);
		bool doesItHit = Physics.Raycast(startPoint, Vector3.down, out hit, Mathf.Abs(min)+Mathf.Abs(max) ,1<<LayerMask.NameToLayer( groundLayoutName) );
		if(doesItHit)
		{
			pt .x= hit.point.x;
			pt .y= hit.point.y;
			pt .z= hit.point.z;
		}
		Debug.DrawLine(startPoint, hit.point, Color.red, 10f);

		return doesItHit;
	}*/
}
