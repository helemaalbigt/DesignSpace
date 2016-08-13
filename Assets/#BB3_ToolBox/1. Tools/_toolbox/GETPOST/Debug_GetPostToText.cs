using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using BlackBox.Tools;
public class Debug_GetPostToText : MonoBehaviour {
    public WebPageUnityLoader webAccessor;
    public Text tLoadingState;
    public Text tTitle;
    public Text tLoaded;
    public bool loadAccessorAtStart;

	void Start ()
    {
        webAccessor.onStartPageLoading += RefreshView;
        webAccessor.onEndPageLoading += RefreshView;

        tLoadingState.text = "Nothing Load";
        tTitle.text = "Access to: " + webAccessor.GetUrl();
        if (loadAccessorAtStart)
            webAccessor.LoadWithCoroutine();
    }

    private void RefreshView(string urlSent,  string textReceived, string error,ref  WebPageUnityLoader info)
    {

        tLoadingState.text = "Loaded";
        tLoaded.text = string.IsNullOrEmpty(error) ? textReceived : error;
    }

    private void RefreshView(string urlSent, ref WebPageUnityLoader info)
    {

        tLoadingState.text = "Loading";
        tLoaded.text = "Url: " + webAccessor.GetUrl()+"\n";
        foreach (WebAccessor.ParamsProperty item in webAccessor.Fields)
        {
            tLoaded.text += string.Format(" -{0} {1} ", item.key, item.value);
        }

    }
    
}
