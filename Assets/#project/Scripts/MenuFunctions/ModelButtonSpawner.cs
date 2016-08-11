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
		var info = new DirectoryInfo(_FilePath);
		var fileInfo = info.GetFiles();

        bool filesFound = false;

		foreach(var file in fileInfo){
            switch (file.Extension)
            {
                case ".obj":
                    filesFound = true;
                    CreateModelButton(file.Name, ModelSpawner.FileType.obj);
                    break;

                case ".fbx":
                    filesFound = true;
                    CreateModelButton(file.Name, ModelSpawner.FileType.fbx);
                    break;
            }
		}

		if (!filesFound)
        {
			_NoModelText.text = "Place .obj files in: \n\n" + _FilePath;
		} 
	}

    private void CreateModelButton(string path, ModelSpawner.FileType type)
    {
        GameObject button = Instantiate(_ModelPrefab as Object, Vector3.zero, Quaternion.identity) as GameObject;
        button.transform.SetParent(_Parent, false);

        Text fileName = button.GetComponentInChildren<Text>();
        fileName.text = path;

        ModelSpawner spawner = button.GetComponent<ModelSpawner>();
        spawner._GlobalWrapper = _GlobalWrapper;
        spawner._ModelFilePath = _FilePath + path;
        spawner._FileType = type;
    }
}
