using UnityEngine;
using System.Collections;

public class JSONbuttonSpawner : MonoBehaviour {

    public GameObject _ImagePrefab;
    public Transform _Parent;

    public string _FilePath;

    // Use this for initialization
    void Start () {

        _FilePath = Application.dataPath + _FilePath;

        //create directory if not exists
        if (!System.IO.Directory.Exists(_FilePath))
            System.IO.Directory.CreateDirectory(_FilePath);



    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
