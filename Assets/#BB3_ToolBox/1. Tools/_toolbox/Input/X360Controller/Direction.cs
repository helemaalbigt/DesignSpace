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


using System.Collections;
using System;
using System.Text;

public class Direction {
	/** y = Axe up, x = Axe Right */
	private float x;
	private float y;
	private float intensity=1.0f;

	/**Return a direction where the x axe go in direction of the right and the y in direction of the up. Like in math*/
	public static Direction CreateInstance(float xAxe, float  yAxe)
	{
		return new Direction(xAxe, yAxe);
	}

	private Direction(float x, float y) {
	
		this.x = x;
		this.y = y;
	}
	
	public void SetX(float x) {
		
		this.x = CheckBounds(x);
	}
	
	public void SetY(float y) {
		this.y = CheckBounds(y);
	}
	
	public float GetX() {
		return x;
	}
	
	public float GetY() {
		return y;
	}

	private float CheckBounds(float value) {
		
		if (value > 1.0f)
			return 1.0f;
		else if (value < -1.0f)
			return -1.0f;
		return value;
	}

	
	public float GetIntensity() {
		return intensity;
	}


	public void SetIntensity(float intensity) {
		if (intensity < 0)
			this.intensity = 0f;
		else
			this.intensity = intensity;
	}
	
	public static void SetDirection(ref Direction d, float startX, float startY,
	                                float endX, float endY) {
		float xprime = endX - startX;
		float yprime = endY - startY;
		Direction.SetDirection(ref d, xprime, yprime);
		
	}
	
	public static void SetDirection(ref Direction d, float x, float y) {
		SetDirectionOnAngle(ref d, GetAngle(x, y));
	}
	
	public float GetAngle() {
		Direction d = this;
		return Direction.GetAngle(ref d);
	}
	
	public static float GetAngle(ref Direction d) {
		
		float x = d.GetX();
		float y = d.GetY();
		
		return GetAngle(x, y);
	}
	
	public static float GetAngle(float x, float y) {
		
		if (x == 0 || y == 0) {
			if (x == 0 && y > 0)
				return 90f;
			if (x == 0 && y < 0)
				return 270f;
			if (y == 0 && x > 0)
				return 0f;
			if (y == 0 && x < 0)
				return 180f;
			
		} else if (x > 0 && y > 0) {
			return RadianToDegree((float) Math.Atan(y / x));

		} else if (x < 0 && y > 0) {
			
			return -RadianToDegree((float)Math.Atan(x / y)) + 90f;
			
		} else if (x < 0 && y < 0) {
			
			return RadianToDegree((float)Math.Atan(y / x)) + 180f;
			
		} else if (x > 0 && y < 0) {
			
			return -RadianToDegree((float)Math.Atan(x / y)) + 270f;
			
		}
		
		return 0f;
		
	}
	private static float RadianToDegree(float angle)
	{
		return angle * (180.0f / (float)Math.PI);
	}
	private static  float DegreeToRadian(float angle)
	{
		return (float)Math.PI * angle / 180.0f;
	}
	public void SetDirectionOnAngle( float angle) {
		Direction d = this;
		SetDirectionOnAngle(ref d, angle);
	}
	
	public static void SetDirectionOnAngle(ref Direction d,  float angle) {
		float rad = 0f;
		if (angle <= 90f && angle >= 0f) {
			rad = DegreeToRadian(angle);
			d.SetX((float)Math.Cos(rad));
			d.SetY((float)Math.Sin(rad));
			
		} else if (angle <= 180) {
			rad = DegreeToRadian(angle - 90);
			d.SetY((float)Math.Cos(rad));
			d.SetX(-(float)Math.Sin(rad));
			
		} else if (angle <= 270) {
			rad = DegreeToRadian(angle - 180);
			d.SetX(-(float)Math.Cos(rad));
			d.SetY(-(float)Math.Sin(rad));
			
		} else if (angle <= 360) {
			rad = DegreeToRadian(angle - 270);
			d.SetY(-(float)Math.Cos(rad));
			d.SetX((float)Math.Sin(rad));
			
		}
		
	}
	
	public float GetRadius() {
		return (float) Math.Sqrt(x * x + y * y);
	}

	public override String ToString() {
	
		StringBuilder builder = new StringBuilder();
		builder.Append("Direction [x=");
		builder.Append(String.Format("{0}", x));
		builder.Append(", y=");
		builder.Append(String.Format("{0}", y));
		builder.Append("   ");
		builder.Append(GetCardinalPoints());
		builder.Append("  , intensity=");
		builder.Append(intensity);
		builder.Append("]");
		return builder.ToString();
	}
	
	public CardinalPoints GetCardinalPoints() {
		return Direction.GetCardinalPoints(GetX(), GetY());
	}
	
	public static CardinalPoints GetCardinalPoints(float x, float y) {
		if(x==0 && y==0) return CardinalPoints.Center;
		float angle = GetAngle(x, y);
		if (angle >= 0 && angle <= 22.5)
			return CardinalPoints.E;
		else if (angle <= 67.5)
			return CardinalPoints.NE;
		else if (angle <= 112.5)
			return CardinalPoints.N;
		else if (angle <= 157.5)
			return CardinalPoints.NO;
		else if (angle <= 202.5)
			return CardinalPoints.O;
		else if (angle <= 247.5)
			return CardinalPoints.SO;
		else if (angle <= 292.5)
			return CardinalPoints.S;
		else if (angle <= 337.5)
			return CardinalPoints.SE;
		else
			return CardinalPoints.E;
		
	}
	
	public void Inverse() {
		InverseX();
		InverseY();
		
	}
	
	public void InverseY() {
		y *= -1f;
		
	}
	
	public void InverseX() {
		x *= -1f;
		
	}
	
	public float GetPseudoIntensityLevel()
	{
		return (float) Math.Sqrt(x*x+y*y);
	}
	
	public void SetWith(ref Direction playerDirection) {
		
		x=playerDirection.x;
		y=playerDirection.y;
		intensity= playerDirection.intensity;

	}
	
	public static void SetDirectionOnRandom(ref Direction direction,Random rand) {
		if(rand==null) rand = new Random();
		float x =(float) rand.NextDouble();
		bool xsigne= rand.Next()%2==1;
		float y =(float) rand.NextDouble();
		bool ysigne=  rand.Next()%2==1;

		direction.SetX(x*(xsigne?-1:1));
		direction.SetY(y*(ysigne?-1:1));
		
	}
	
	public static void SetDirection(ref Direction toSetup,ref  Direction from) {
		toSetup.SetWith(ref from);
		
	}




}

