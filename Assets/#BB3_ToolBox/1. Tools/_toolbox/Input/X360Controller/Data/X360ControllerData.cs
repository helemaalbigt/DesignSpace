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
using System;
using System.Text;

public class X360ControllerData : I_X360Data {

	
	/** A = 0: 0<->1 */
	private bool a;
	
	/** B = 1: 0<->1 */
	private bool b;
	
	/** X = 2: 0<->1 */
	private bool x;
	
	/** Y = 3: 0<->1 */
	private bool y;
	
	/** LB = 4: 0<->1 */
	private bool lb;
	
	/** RB = 5: 0<->1 */
	private bool rb;
	
	/** MENU = 6: 0<->1 */
	private bool menu;
	
	/** START = 7: 0<->1 */
	private bool start;
	
	/** LEFT TRIGGER LT =0 (no pression->1 full pression) */
	private float lt;
	
	/** Right TRIGGER RT =0 (no pression->1 full pression) */
	private float rt;
	
	/** Press stick Left= 8: 0<->1 */
	private bool leftPull;
	
	/** Press stick Right = 9: 0<->1 */
	private bool rightPull;
	
	private  Direction arrows = Direction.CreateInstance(0f,0f);
	private  Direction left = Direction.CreateInstance(0f,0f);
	private  Direction right = Direction.CreateInstance(0f,0f);
	
	public bool IsA() {
		return a;
	}
	
	public void SetA(bool a) {
		this.a = a;
	}
	
	public bool IsB() {
		return b;
	}
	
	public void SetB(bool b) {
		this.b = b;
	}
	
	public bool IsX() {
		return x;
	}
	
	public void SetX(bool x) {
		this.x = x;
	}
	
	public bool IsY() {
		return y;
	}
	
	public void SetY(bool y) {
		this.y = y;
	}
	
	public bool IsLb() {
		return lb;
	}
	
	public void SetLb(bool lb) {
		this.lb = lb;
	}
	
	public bool IsRb() {
		return rb;
	}
	
	public void SetRb(bool rb) {
		this.rb = rb;
	}
	
	public bool IsMenu() {
		return menu;
	}
	
	public void SetMenu(bool menu) {
		this.menu = menu;
	}
	
	public bool IsStart() {
		return start;
	}
	
	public void SetStart(bool start) {
		this.start = start;
	}
	
	public float GetLt() {
		return lt;
	}
	public bool IsLt() {
		return lt<0f;
	}
	
	public void SetLt(float lt) {
		if (lt < 0f)
			lt = 0.0f;
		else if (lt > 1f)
			lt = 1.0f;
		this.lt = lt;
	}
	
	public float GetRt() {
		return rt;
	}
	public bool IsRt() {
		return rt>0f;
	}
	
	public void SetRt(float rt) {
		if (rt < 0f)
			rt = 0.0f;
		else if (rt > 1f)
			rt = 1.0f;
		 this.rt = rt;
	}
	

	
	public void SetArrows ( Direction arrow) {
		this.arrows.SetX(arrow.GetX());
		this.arrows.SetY(arrow.GetY());
	}
	public Direction GetArrows() {
		return arrows;
	}

	public bool IsLeftPull() {
		return leftPull;
	}
	
	public void SetLeftPull(bool leftPull) {
		this.leftPull = leftPull;
	}
	
	public bool IsRightPull() {
		return rightPull;
	}
	
	public void SetRightPull(bool rightPull) {
		this.rightPull = rightPull;
	}
	
	public Direction GetLeft() {
		return left;
	}
	
	public void SetLeft(Direction left) {
		this.left.SetX(left.GetX());
		this.left.SetY(left.GetY());
	}
	
	public Direction GetRight() {
		return right;
	}
	
	public void SetRight(Direction right) {
		this.right.SetX(right.GetX());
		this.right.SetY(right.GetY());
		
	}
	


	public override String ToString() {
		StringBuilder builder = new StringBuilder();
		builder.Append("X360ControllerData [");
		
		if (a) {
			builder.Append("\t A");
		}
		if (b) {
			builder.Append("\t B");
		}
		if (x) {
			builder.Append("\t X");
		}
		if (y) {
			builder.Append("\t Y");
		}
		if (lb) {
			builder.Append("\t LB");
		}
		if (rb) {
			builder.Append("\t RB");
		}
		if (menu) {
			builder.Append("\t menu");
		}
		if (start) {
			builder.Append("\t start");
		}
		if (lt > 0.1) {
			builder.Append("\t LT=");
			builder.Append(lt);
		}
		if (rt > 0.1) {
			builder.Append("\t RT=");
			builder.Append(rt);
		}
		if (leftPull) {
			builder.Append("\t left ->O");
		}
		if (rightPull) {
			builder.Append("\t right ->O");
		}
		if (Math.Abs(arrows.GetX()) > 0.1f || Math.Abs(arrows.GetY()) > 0.1f) {
			builder.Append("\t <-î->=");
			builder.Append(arrows);
		}
		if (Math.Abs(left.GetX()) > 0.1f || Math.Abs(left.GetY()) > 0.1f) {
			builder.Append("\t left=");
			builder.Append(left);
		}
		if (Math.Abs(right.GetX()) > 0.1f || Math.Abs(right.GetY()) > 0.1f) {
			builder.Append("\t right=");
			builder.Append(right);
		}
		builder.Append("]");
		return builder.ToString();
	}
	
	
}
