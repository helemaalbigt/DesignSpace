using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class FormManager_date : MonoBehaviour {

    public InputField min;
    public InputField hour;
    public InputField day;
    public InputField month;
    public InputField year;

    // Use this for initialization
    void Start () {
        UpdateFields(ModelInfo.dateTime.Year, ModelInfo.dateTime.Month, ModelInfo.dateTime.Day, ModelInfo.dateTime.Hour, ModelInfo.dateTime.Minute);

        //add listeners to each field
        min.onEndEdit.AddListener(UpdateModelInfo);
        hour.onEndEdit.AddListener(UpdateModelInfo);
        day.onEndEdit.AddListener(UpdateModelInfo);
        month.onEndEdit.AddListener(UpdateModelInfo);
        year.onEndEdit.AddListener(UpdateModelInfo);
    }
	
    private void UpdateFields(int Y, int M, int D, int h, int m)
    {
        year.text = Y+"";
        month.text = M + "";
        day.text = D + "";
        hour.text = h + "";
        min.text = m + "";
    }

    public void UpdateModelInfo(string value)
    {
        if (year.text != "" && month.text != "" && day.text != "" )
        {
            int hourValue = hour.text == "" ? ModelInfo.dateTime.Hour : int.Parse(hour.text);
            int minValue = min.text == "" ? ModelInfo.dateTime.Minute : int.Parse(min.text);
            ModelInfo.dateTime = GetValidDateTime(int.Parse(year.text), int.Parse(month.text), int.Parse(day.text), hourValue, minValue);
        }
    }

    private DateTime GetValidDateTime(int Y, int M, int D, int h, int m)
    {
        DateTime temp;
        Debug.Log("iterated");
        if (DateTime.TryParse(Y + "-" + M + "-" + D + " " + h + ":" + m, out temp))
        {
            return temp;
        }
        else
        {
            UpdateFields(Y, M, D - 1, h, m);
            return GetValidDateTime(Y, M, D-1, h, m);
        }
    }
}
