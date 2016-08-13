using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JSONbutton : MonoBehaviour {

    public string JsonFileLocation = null;

    public Text buttonNameText;

    private string buttonName;
    public string ButtonName
    {
        get { return buttonName; }
        set
        {
            buttonName = value;
            buttonNameText.text = buttonName;
        }
    }
}
