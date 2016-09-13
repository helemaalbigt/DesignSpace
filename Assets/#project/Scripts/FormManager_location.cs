using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FormManager_location : MonoBehaviour {

    public InputField latitude;
    public InputField longitude;

    // Use this for initialization
    void Start () {
        UpdateFields(ModelInfo.latitude, ModelInfo.longitude);

        latitude.onEndEdit.AddListener(UpdateModelInfo);
        longitude.onEndEdit.AddListener(UpdateModelInfo);
    }

    private void UpdateFields(float lat, float lon)
    {
        latitude.text = lat+"";
        longitude.text = lon+"";
    }

   // Update is called once per frame
    private void UpdateModelInfo(string value)
    {
        if(latitude.text != "" && longitude.text != "")
        {
            ModelInfo.latitude = float.Parse(latitude.text);
            ModelInfo.longitude = float.Parse(longitude.text);
        }
    }
}
