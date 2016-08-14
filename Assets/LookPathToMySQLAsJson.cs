using UnityEngine;
using System.Collections;
using BlackBox.Tools;
using BlackBox.Beans.Basic;
using System.Collections.Generic;
using BlackBox.Tools.IO;
using Newtonsoft.Json;
using System;

public class LookPathToMySQLAsJson : MonoBehaviour {
    
    public string _postLookPathUrl="http://jams.center/UnityAccess/DesignSpace/???.php";




    public void PostLookPathData(LookPath lookPath) {
        Loading loading = new Loading();
        StartCoroutine(PostLookPath(loading,lookPath));
    }

    public IEnumerator PostLookPath( Loading loading, LookPath lookPath) {
        loading.SetLoadingState(true);

        List<WebAccessor.ParamsProperty> properties = new List<WebAccessor.ParamsProperty>();
        properties.Add(new WebAccessor.ParamsProperty("sessionName", lookPath._createdTime, WebAccessor.ParamsProperty.SendType.POST));
        properties.Add(new WebAccessor.ParamsProperty("data", JsonConvert.SerializeObject(lookPath), WebAccessor.ParamsProperty.SendType.POST));
        properties.Add(new WebAccessor.ParamsProperty("action", "saveData", WebAccessor.ParamsProperty.SendType.POST));


        WebPageUnityLoader loader = WebAccessor.LoadPage(_postLookPathUrl, properties, true);
        loader.onEndPageLoading += Destroy;
        while (loading.IsLoading())
            yield return new WaitForEndOfFrame();
        loading.SetLoadingState(false);


    }

    public delegate void OnLookPathLoadedAsJson(string sessionName, string json);

    public void LoadJsonFile( OnLookPathLoadedAsJson onJsonLoaded) {


    }

    private void Destroy(string urlSent, string textReceived, string error, ref WebPageUnityLoader info)
    {
        if (!string.IsNullOrEmpty(error)) {
            Debug.LogError("WEB ACCES ERROR:"+error);
        }
        //Debug.Break();
        //info.AutoDestruction();
    }
}
