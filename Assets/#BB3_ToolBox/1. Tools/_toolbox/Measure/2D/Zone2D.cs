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

[ExecuteInEditMode]
public class Zone2D : MonoBehaviour,I_Zone2D {
	public Transform left;
	public Transform right;
	public Transform top;
	public Transform bottom;
	/**U * ratio = m :  1 unit * 1.2 ratio = 1.20 m*/
	public float ratioMeterPerUnit = 1.0f;

	public enum Axe{ XY_2D, XZ_3DTopView }
	public Axe viewType = Axe.XY_2D;
	void Start () {
		
		if (left == null || right == null || top == null || bottom == null ) {
			
			throw new UnityException("Coordinate(s) is(are) not define: "+this.gameObject);
		}
		
		
	}
	

	private float lastCalculatedWidth,lastCalculatedHeight;
	public float GetLastCalculatedWidth(){return lastCalculatedWidth;}
	public float GetLastCalculatedHeight(){return lastCalculatedHeight;}


	public bool IsOutOfTheMap(Vector3  element )
	{
		if (Axe.XY_2D.Equals (viewType)) {
			if (element.y < bottom.position.y)
				return true;
			else if (element.y > top.position.y)
				return true;
			else if (element.x < left.position.x)
				return true;
			else if (element.x > right.position.x)
				return true;
		}
		if (Axe.XZ_3DTopView.Equals (viewType)) {
			if (element.z < bottom.position.z)
				return true;
			else if (element.z > top.position.z)
				return true;
			else if (element.x < left.position.x)
				return true;
			else if (element.x > right.position.x)
				return true;
			
		}
		return false;	
	}
	
	public bool IsOutOfTheMapWithRectif(ref Vector3  element )
	{
		if (Axe.XY_2D.Equals (viewType)) {
			if (element.y < bottom.position.y){
				element.y = bottom.position.y;
			}
			if (element.y > top.position.y){
				element.y = top.position.y;
			}
			if (element.x < left.position.x){
				element.x = left.position.x;
			}
			if (element.x > right.position.x){
				element.x = right.position.x;
			}
			return true;
		}
		if (Axe.XZ_3DTopView.Equals (viewType)) {
			if (element.z < bottom.position.z){
				element.z = bottom.position.z;
			}
			if (element.z > top.position.z){
				element.z = top.position.z;
			}
			if (element.x < left.position.x){
				element.x = left.position.x;
			}
			if (element.x > right.position.x){
				element.x = right.position.x;
			}
			return true;
		}
		return false;	
	}
	
	public float GetHeight()
	{
		float distance = 0f;
		if (Axe.XY_2D.Equals (viewType))
			distance = GetDistanceBetweenTwoPoint(bottom.position.y,top.position.y);
		else if (Axe.XZ_3DTopView.Equals (viewType))
			distance =  GetDistanceBetweenTwoPoint(bottom.position.z,top.position.z);
		lastCalculatedHeight = distance;
		return distance;
	}
	public float GetHeightInMeter()
	{
		return ApplyRatio(GetHeight());
	}

	public float ApplyRatio(float value){
		return value * ratioMeterPerUnit;
	}
	public float GetWidth()
	{
		float distance = GetDistanceBetweenTwoPoint(left.position.x,right.position.x)*ratioMeterPerUnit;
		lastCalculatedWidth = distance;
		return distance;

	}
	public float GetWidthInMeter()
	{
		return ApplyRatio(GetWidth());
	}
	
	private float GetDistanceBetweenTwoPoint(float smallOne,float bigOne)
	{
		float xl =smallOne;
		float xr =bigOne;
		if(xr<xl)
		{	
			//	Debug.LogWarning("Please Bitch, the other left...");
			float tmp = xr;
			xr = xl;
			xl=tmp;
		}
		//  o--o   |
		if(xl<0f && xr<=0f)
		{
			return -xl+xr;
		}
		//  o--|--o
		else 
			if(xl<=0f && xr>=0f)
		{
			return Mathf.Abs(xl)+xr;
		}
		
		//  |o----o
		else 
			if(xl>=0f && xr>=0f && xr>=xl)
		{
			return xr-xl;
		}
		Debug.Log("There is something wrong");
		
		return 0;
	}
	
	

	
	public bool IsValide(){
		
		return left != null && right != null && top != null && bottom != null;
	}


	Vector3 topLeft = new Vector3 ();
	Vector3 topRight = new Vector3 ()	;
	Vector3 botLeft = new Vector3 ()	;
	Vector3 botRight = new Vector3 ()	;

	public void Update()
	{
		if (Application.isEditor && IsValide()) {
			
			
			topLeft.x = left.position.x;
			topRight.x = right.position.x;
			botLeft.x = left.position.x;
			botRight.x = right.position.x;
			
			if (Axe.XY_2D.Equals (viewType)){
				float z = left.position.z+right.position.z+top.position.z+bottom.position.z;		

				topLeft.y = top.position.y;
				topRight.y = top.position.y;
				botLeft.y = bottom.position.y;
				botRight.y = bottom.position.y;
				topLeft.z = z;
				topRight.z = z;
				botLeft.z = z;
				botRight.z = z;
			}
			if (Axe.XZ_3DTopView.Equals (viewType)){
				float y = left.position.y+right.position.y+top.position.y+bottom.position.y;		

				topLeft.z = top.position.z;
				topRight.z = top.position.z;
				botLeft.z = bottom.position.z;
				botRight.z = bottom.position.z;
				topLeft.y = y;
				topRight.y = y;
				botLeft.y = y;
				botRight.y = y;
				
			}
			
			Debug.DrawLine (topLeft, botLeft);
			Debug.DrawLine (topLeft, topRight);
			Debug.DrawLine (botRight, topRight);
			Debug.DrawLine (botRight, botLeft);


		}
	}
	
	
	public Vector3 GetRandomPointInZone()
	{
		Vector3 randomPt = new Vector3 ();
		
		randomPt.x = Random.Range(left.transform.position.x, right.transform.position.x);
		if (Axe.XY_2D.Equals (viewType)) {
			randomPt.y = Random.Range(left.transform.position.y, right.transform.position.y);
			
		}
		if (Axe.XZ_3DTopView.Equals (viewType)) {
			randomPt.z = Random.Range(bottom.transform.position.z, top.transform.position.z);
			
		}
		return randomPt;
	}
	
	
}
