using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class JSONbuttonSpawner : MonoBehaviour {

    public GameObject _ButtonPrefab;
    public Transform _Parent;

    public string _FilePath;

    // Use this for initialization
    void Start () {

        _FilePath = Application.dataPath + _FilePath;

        //create directory if not exists
        if (!System.IO.Directory.Exists(_FilePath))
            System.IO.Directory.CreateDirectory(_FilePath);

        //get all json files from directory
        var info = new DirectoryInfo(_FilePath);
        var fileInfo = info.GetFiles();

        foreach (var file in fileInfo)
        {
            if (file.Extension == ".json")
            {
                GameObject button = Instantiate(_ButtonPrefab as Object, Vector3.zero, Quaternion.identity) as GameObject;

                JSONbutton jsb = button.GetComponent<JSONbutton>();
                jsb.JsonFileLocation = file.FullName;
                string min = file.CreationTime.Minute > 10 ? file.CreationTime.Minute + "" : "0" + file.CreationTime.Minute;
                jsb.ButtonName = file.CreationTime.Day + "/" + file.CreationTime.Month + "/" + file.CreationTime.Year + " | " + file.CreationTime.Hour + ":" + min;

                button.transform.parent = _Parent;
                button.transform.localScale = Vector3.one;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
