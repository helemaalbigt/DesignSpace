using UnityEngine;
using System.Collections;

public abstract class SaveAndRestore : MonoBehaviour {

    public string _fieldId="";

    public abstract string GetInfoToSaveFromSource();
    public abstract void SetInfoToOrignalSource(string text);
    
    void Reset () {
        _fieldId ="ID_"+ Random.Range(0, 9999).ToString()+"_"+gameObject.name;
	}

    void Awake() {
        SetInfoToOrignalSource(LoadFromPlayerPrefs());
    }
	
    void OnExit() {
        SaveToPlayerPrefs(GetInfoToSaveFromSource());
    }
    void OnApplicationQuit()
    {
        SaveToPlayerPrefs(GetInfoToSaveFromSource());
    }

    void SaveToPlayerPrefs(string text)
    {
        PlayerPrefs.SetString(_fieldId, text);
    }
    string LoadFromPlayerPrefs()
    {
        return PlayerPrefs.GetString(_fieldId,"");
    }
}
