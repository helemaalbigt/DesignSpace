using UnityEngine;
using System.Collections;
using BlackBox.Tools;
using BlackBox.Beans.Basic;
using System.Collections.Generic;
using BlackBox.Tools.IO;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

public class LookPathToMySQLAsJson : MonoBehaviour {
    
    public string _postLookPathUrl="http://jams.center/UnityAccess/DesignSpace/???.php";


    public string RemoveAllNonAlphaByUnderScore(string value) {
        Regex rgx = new Regex("[^a-zA-Z0-9 -]");
        return rgx.Replace(value, "_");

    }

    public void PostLookPathData(LookPath lookPath) {
        Loading loading = new Loading();
        StartCoroutine(PostLookPath(loading,lookPath));
    }

    public IEnumerator PostLookPath(Loading loading, LookPath lookPath)
    {
        loading.SetLoadingState(true);

        List<WebAccessor.ParamsProperty> properties = new List<WebAccessor.ParamsProperty>();
        properties.Add(new WebAccessor.ParamsProperty("sessionName", RemoveAllNonAlphaByUnderScore(lookPath._createdTime), WebAccessor.ParamsProperty.SendType.POST));
        properties.Add(new WebAccessor.ParamsProperty("data", JsonConvert.SerializeObject(lookPath), WebAccessor.ParamsProperty.SendType.POST));
        properties.Add(new WebAccessor.ParamsProperty("action", "saveData", WebAccessor.ParamsProperty.SendType.POST));


        WebPageUnityLoader loader = WebAccessor.LoadPage(_postLookPathUrl, properties, true);
        loader.onEndPageLoading += Destroy;
        while (loading.IsLoading())
            yield return new WaitForEndOfFrame();
        loading.SetLoadingState(false);


    }
    public IEnumerator DeleteAllFromDB(Loading loading)
    {
        loading.SetLoadingState(true);

        List<WebAccessor.ParamsProperty> properties = new List<WebAccessor.ParamsProperty>();
        properties.Add(new WebAccessor.ParamsProperty("action", "deleteAll", WebAccessor.ParamsProperty.SendType.POST));


        WebPageUnityLoader loader = WebAccessor.LoadPage(_postLookPathUrl, properties, true);
        loader.onEndPageLoading += Destroy;
        while (loading.IsLoading())
            yield return new WaitForEndOfFrame();
        loading.SetLoadingState(false);


    }



    public delegate void OnJsonDetected(string sessionName, string json);
    public void LoadJsonFile(OnJsonDetected onJsonLoaded, Loading isAllSonLoaded) {
       
        string[] sessions = null;
        StartCoroutine(LoadSessionCoroutine(isAllSonLoaded, sessions,onJsonLoaded));
    }
  
    public IEnumerator LoadSessionCoroutine(Loading loading, string[] sessions, OnJsonDetected onJsonDetected)
    {
        loading.SetLoadingState(true);
        List<WebAccessor.ParamsProperty> properties = new List<WebAccessor.ParamsProperty>();
        properties.Add(new WebAccessor.ParamsProperty("action", "getSessionNames", WebAccessor.ParamsProperty.SendType.POST));


        WebPageUnityLoader loader = WebAccessor.LoadPage(_postLookPathUrl, properties, true);
        loader.onEndPageLoading += Destroy;
        while (loader.IsLoading())
            yield return new WaitForSeconds(0.05f);

        if (string.IsNullOrEmpty(loader.Error)) {
            string sessionRaw = loader.TextLoaded.Replace(" ", "");
           sessions = sessionRaw.Split(',');

            foreach (string session in sessions)
            {
                string str = session;
                if(!string.IsNullOrEmpty(str))
                yield return StartCoroutine(LoadJsonOfSession(str, onJsonDetected));

            }
        }

        loading.SetLoadingState(false);
        yield break;


    }

    private IEnumerator LoadJsonOfSession(string session, OnJsonDetected onJsonDetected)
    {
       
        List<WebAccessor.ParamsProperty> properties = new List<WebAccessor.ParamsProperty>();
        properties.Add(new WebAccessor.ParamsProperty("action", "getData", WebAccessor.ParamsProperty.SendType.POST));
        properties.Add(new WebAccessor.ParamsProperty("sessionName",session, WebAccessor.ParamsProperty.SendType.POST));


        WebPageUnityLoader loader = WebAccessor.LoadPage(_postLookPathUrl, properties, true);
        loader.onEndPageLoading += Destroy;
        while (loader.IsLoading())
            yield return new WaitForSeconds(0.05f);

        if (string.IsNullOrEmpty(loader.Error))
        {
            onJsonDetected(session, loader.TextLoaded);
        }
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
