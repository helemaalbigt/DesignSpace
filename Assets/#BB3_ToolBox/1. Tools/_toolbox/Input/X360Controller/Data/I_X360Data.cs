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

 public  interface I_X360Data {


	
	bool IsA();
	
	void SetA(bool a);
	
	bool IsB() ;
	
	void SetB(bool b);
	
	bool IsX();
	
	void SetX(bool x);
	
	bool IsY();
	
	void SetY(bool y) ;
	
	bool IsLb();
	
	void SetLb(bool lb) ;
	
	bool IsRb();
	void SetRb(bool rb);
	
	bool IsMenu();
	
	void SetMenu(bool menu);

	bool IsStart() ;
	
	void SetStart(bool start) ;
	
	float GetLt() ;
	bool IsLt() ;
	void SetLt(float lt);
	float GetRt();
	bool IsRt();
	void SetRt(float rt);
	
	

	bool IsLeftPull();
	void SetLeftPull(bool leftPull);
	bool IsRightPull() ;
	void SetRightPull(bool rightPull);

	
	void SetLeft(Direction left);
	void SetRight(Direction right) ;
	void SetArrows ( Direction arrow);
	Direction GetLeft() ;
	Direction GetRight();
	Direction GetArrows();


	string ToString();
}
