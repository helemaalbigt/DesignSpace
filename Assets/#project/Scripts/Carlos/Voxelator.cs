using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Voxelator : MonoBehaviour {

	public GameObject baseCube;
	public float CubeSize;

	private int Cant =20;
	private GameObject[] arrRandomCubes;
	private Vector3[] arrVecLocations;

	[Header("TEsting proximity...")]
	[Tooltip("information")]
	public bool pointIsInCube;
	private List<Vector3> lstCubePts;
	private List<int> lstCount;

	void Start () {
		
		arrVecLocations = RandomPoints (Cant);
		Cube testMap = new Cube (CubeSize);
		testMap.heatMap (arrVecLocations);

		lstCubePts = testMap.CubePts;

		GameObject plHolder;
		foreach (Vector3 v in lstCubePts) {
			plHolder = (GameObject) Instantiate(baseCube, v, Quaternion.identity);
			plHolder.transform.localScale = new Vector3 (CubeSize, CubeSize, CubeSize);
		}
			
	}
		
	// Update is called once per frame
	void Update () {
		DisplayPoints (arrVecLocations, 0.2f, Color.blue);
	}
		
	private GameObject[] CreateCubes(Vector3[] arrVecs)
	{
		int num = arrVecs.Length;
		GameObject[] arrGameObjects = new GameObject[num];
		int i = 0;
		foreach (Vector3 vec in arrVecs) {

			arrGameObjects[i] = GameObject.CreatePrimitive (PrimitiveType.Cube);
			arrGameObjects[i].transform.position = vec;
			arrGameObjects[i].transform.localScale = new Vector3 (1, 1, 1);
			arrGameObjects [i].GetComponent<Renderer> ().material.color = new Color(1, 0, 0, 0.3f);
	
			i++;
		}
		return arrGameObjects;
	}

	private void DisplayPoints(Vector3[] arrVecs, float s, Color cl)
	{
		s = s * 0.5f;
		Vector3 vecL, vecR, vecN, vecS, vecU, vecD;
		Color clr = Color.blue;
		foreach (Vector3 v in arrVecs) {
		
			vecL = new Vector3 (v.x + s, v.y, v.z);
			vecR = new Vector3 (v.x - s, v.y, v.z);
			vecN = new Vector3 (v.x, v.y, v.z + s);
			vecS = new Vector3 (v.x, v.y, v.z - s);
			vecU = new Vector3 (v.x, v.y + s, v.z);
			vecD = new Vector3 (v.x, v.y - s, v.z);
			Debug.DrawLine(vecL, vecR, clr);
			Debug.DrawLine(vecN, vecS, clr);
			Debug.DrawLine(vecU, vecD, clr);
		}

	}

	private Vector3[] RandomPoints(int number)
	{
		Vector3[] arrVecs = new Vector3[number];
		float step = 0.15f;
		float max = 0.05f;
		float min = -max;

		for (int i = 0; i < number; i++){
			arrVecs [i] = new Vector3 (i*step, Random.Range(min, max), Random.Range(min, max));
		}
		return arrVecs;
	}

}
