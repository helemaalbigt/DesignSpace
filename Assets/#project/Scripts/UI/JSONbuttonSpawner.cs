using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class JSONbuttonSpawner : MonoBehaviour {

    public GameObject _ButtonPrefab;
    public Transform _Parent;
    public string _FilePath;

    public LookPathToJSON _lookPathToJSON;

    // Use this for initialization
    void Start () {

        _FilePath = Application.dataPath + _FilePath;

        //create directory if not exists
        if (!System.IO.Directory.Exists(_FilePath))
            System.IO.Directory.CreateDirectory(_FilePath);

        //get all json files from directory
        var info = new DirectoryInfo(_FilePath);
        var fileInfo = info.GetFiles();

        int i = 0;

        foreach (LookPath path in _lookPathToJSON.RecoverAllPaths())
        {
            GameObject button = Instantiate(_ButtonPrefab as UnityEngine.Object, Vector3.zero, Quaternion.identity) as GameObject;
            button.transform.parent = _Parent;
            button.transform.localScale = Vector3.one;

            JSONbutton jsb = button.GetComponent<JSONbutton>();
            jsb._LookPath = path;
            i++;
            jsb.ButtonName = "user "+i;
        }

        /*
        foreach (var file in fileInfo)
        {
            if (file.Extension == ".json")
            {
                GameObject button = Instantiate(_ButtonPrefab as UnityEngine.Object, Vector3.zero, Quaternion.identity) as GameObject;
                button.transform.parent = _Parent;
                button.transform.localScale = Vector3.one;

                JSONbutton jsb = button.GetComponent<JSONbutton>();
                jsb.JsonFileLocation = file.FullName;
                string min = file.CreationTime.Minute > 10 ? file.CreationTime.Minute + "" : "0" + file.CreationTime.Minute;
                jsb.ButtonName = file.CreationTime.Day + "/" + file.CreationTime.Month + "/" + file.CreationTime.Year + " | " + file.CreationTime.Hour + ":" + min;
            }
        }
        */
    }

}
