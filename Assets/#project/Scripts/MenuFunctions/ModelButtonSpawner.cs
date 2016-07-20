using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ModelButtonSpawner : MonoBehaviour {

	public Transform _GlobalWrapper;
	public GameObject _ModelPrefab;
	public Transform _Parent;

	public Text _NoModelText;

	private string _FilePath;
	public int _ClickableAtStart = 9;

	// Use this for initialization
	void Start(){

		if (Application.isEditor) {
			_FilePath = Application.dataPath + "/Resources/Models/"; 			//persistentDataPath
		} else {
			_FilePath = Application.dataPath + "/Resources/Models/"; 
		}
			
		//create directory if not exists
		if (!System.IO.Directory.Exists (_FilePath))
			System.IO.Directory.CreateDirectory (_FilePath);

		//get all images from directory
		List<string> files = new List<string> ();
		var info = new DirectoryInfo(_FilePath);
		var fileInfo = info.GetFiles();

		foreach(var file in fileInfo){
			if (file.Extension == ".obj")
			{
				files.Add(file.Name);
			}
		}

		if (files.Count == 0)
		{
			_NoModelText.text = "Place .obj files in: \n\n" + _FilePath;
		} 

		int i = 0;

		foreach (string file in files)
		{
			Debug.Log ("yuup");
			GameObject button = Instantiate (_ModelPrefab as Object, Vector3.zero, Quaternion.identity) as GameObject;
			button.transform.SetParent (_Parent,false);

			Text fileName = button.GetComponentInChildren<Text> ();
			fileName.text = file;

			ModelSpawner spawner = button.GetComponent<ModelSpawner>();
			spawner._GlobalWrapper = _GlobalWrapper;
			spawner._ModelFilePath = _FilePath + file;

			if (i < _ClickableAtStart)
			{
				button.gameObject.layer = LayerMask.NameToLayer ("Default");
			}

			i++;
		}

		RectTransform R = _Parent.GetComponent<RectTransform> ();
		R.sizeDelta = new Vector2(R.sizeDelta.x, (40f + 5f) * ((Mathf.CeilToInt((files.Count) / 3) + 1)));
	}
}
