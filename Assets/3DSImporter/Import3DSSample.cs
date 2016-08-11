using UnityEngine;
using System.Collections;
using System.IO;

public class Import3DSSample : MonoBehaviour {

	public 	string 		sFilename = "assets/head_test.3ds";

	// Use this for initialization
	void Start () {
	
	 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	Rect rcWindow = new Rect(10, 10, 240, 160);
	void OnGUI()
	{
		rcWindow = GUI.Window(0, rcWindow, ImportWindow, "Import 3DS");
	}
	
	void ImportWindow(int windowID) 
	{
		
			GUI.Label (new Rect (10, 20, 220, 20), "3DSMesh File :");
			sFilename = GUI.TextField (new Rect (10, 45, 180, 20), sFilename);
			
			GUI.Label (new Rect (10, 70, 220, 20), "Texture Path :");
			_3DSReader.TexturePath = GUI.TextField (new Rect (10, 95, 180, 20), _3DSReader.TexturePath );

		 
			if (GUI.Button (new Rect (10, 125, 220, 20), "Import Mesh")) 
			{
				_3DSReader.Import3DS(sFilename);
			}
	}


}
