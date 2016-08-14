using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Cube
{

	private Vector3 center;
	private float cubeSize;
	private float hSize;

	private List<Vector3> lstPtsCube;
	public List<Vector3> CubePts
	{
		get{
			return lstPtsCube;
		}
	}

	private int[] arrContent;

	public int[] arrCountNumbers
	{
		get{return arrContent;}
	}

	private Color[] arrColors;
	public Color[] ColorMap
	{
		get {
			return arrColors;
		}
	}

	public Cube (Vector3 Center, float CubeSize)
	{
		this.center = Center;
		this.cubeSize = CubeSize;
		halfsize ();
	}

	public Cube (float CubeSize)
	{
		this.cubeSize = CubeSize;
		halfsize ();
	}
		

	public void heatMap(Vector3[] arrPts){
		lstPtsCube = new List<Vector3> ();

		Vector3 ptTest;
		int count = 0;

		for (int i = 0; i < arrPts.Length; i++) {
		
			ptTest = arrPts [i];
			for (int j = i; j < arrPts.Length; j++) {

				if (TestInside (arrPts [j], ptTest)) {
					count++;			
				} else {
					break;
				}
			}
			lstPtsCube.Add (ptTest);
			i = count;
		}
			
		arrContent = new int[lstPtsCube.Count];
		int c = 0;
		foreach (Vector3 vec in lstPtsCube) {
		
			count = 0;
			foreach (Vector3 p in arrPts) {
				if (TestInside (p, vec))
					count++;
			}
			arrContent [c] = count;
			c++;
		}			
	}
		
	}
		
	private void halfsize()
	{
		hSize = cubeSize * 0.5f;
	}

	private bool TestInside(Vector3 p, Vector3 ptCube)
	{
		Vector3 ptStart = new Vector3 (ptCube.x - hSize, ptCube.y - hSize, ptCube.z - hSize);
		Vector3 ptEnd = new Vector3 (ptCube.x + hSize, ptCube.y + hSize, ptCube.z + hSize);

		if ((p.x > ptStart.x & p.x < ptEnd.x) & (p.z > ptStart.z & p.z < ptEnd.z) & (p.y > ptStart.y & p.y < ptEnd.y)) {
			return  true;
		} else {
			return false;
		}
	}

	public bool TestInside(Vector3 p)
	{
		Vector3 ptStart = new Vector3 (center.x-hSize, center.y-hSize, center.z-hSize);
		Vector3 ptEnd = new Vector3 (center.x + hSize, center.y + hSize, center.z + hSize);

		if ((p.x > ptStart.x & p.x < ptEnd.x) & (p.z > ptStart.z & p.z < ptEnd.z) & (p.y > ptStart.y & p.y < ptEnd.y)) {
			return  true;
		} else {
			return false;
		}
	}
}