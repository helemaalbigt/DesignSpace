using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataVizController : MonoBehaviour {

    public Transform _JsonButtonsParent;
    public Transform _TargetModel;

    private List<JSONbutton> JsonButtons = new List<JSONbutton>();
    private bool _IsRendering = false;
    private bool _FinishedRendering = false;
    private bool _Paused = false;

	// Use this for initialization
	void Start () {
        Invoke("RefreshJsonButtons", 0.5f);
	}

    private void RefreshJsonButtons()
    {
        foreach (Transform child in _JsonButtonsParent)
        {
            if (child.GetComponent<JSONbutton>() != null)
                JsonButtons.Add(child.GetComponent<JSONbutton>());
        }
    }

    public void StartPlayBack()
    {
       Debug.Log( GetActiveButtons().Count );
    }

    public void Pause()
    {
        _Paused = true;
    }

    public void Resume()
    {
        _Paused = false;
    }


    private List<JSONbutton> GetActiveButtons()
    {
        List<JSONbutton> returnList = new List<JSONbutton>();
        foreach(JSONbutton jsb in JsonButtons)
        {
            if(jsb.GetComponent<MenuCheckbox>() != null)
            {
                if (jsb.GetComponent<MenuCheckbox>()._IsSelected)
                    returnList.Add(jsb);
            }
        }
        return returnList;
    }
}
