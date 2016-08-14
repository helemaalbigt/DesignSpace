using UnityEngine;
using System.Collections;
using BlackBox.Beans.Basic;

using BlackBox.Tools.IO;
using UnityEngine.Events;

public class LoadJsonFromDbToHardDrive : MonoBehaviour {

    public LookPathToMySQLAsJson _webServerAcces;
    public ProjectDirectoryPath _jsonFolderPath;

    public UnityEvent _onFinishDownloadingJson;

    public bool _autoLoadAtStart=true;
    public bool _autoFlushOnLoad = true;
    public void Start()
    {
        if (_autoLoadAtStart)
        {
            LoadAllJson();
        }
        else
        {
            _onFinishDownloadingJson.Invoke();
        }

    }

    public void LoadAllJson() {

        Loading isAllJsonLoaded = new Loading();
        isAllJsonLoaded.SetLoadingState(true);
        isAllJsonLoaded.onLoadingChangeState += SayHelloWhenAllJsonAreLoaded;


        if (_autoFlushOnLoad)
            FlushFolderBeforeDownload();

        _webServerAcces.LoadJsonFile(DisplayJSON, isAllJsonLoaded);
    }

    public void FlushFolderBeforeDownload() {
            ProjectPathTools.RemoveAllIn(_jsonFolderPath);
    }

    private void SayHelloWhenAllJsonAreLoaded(Loading loading, bool isLoading)
    {
        if (!isLoading)
        {
            print("Hello json");
            _onFinishDownloadingJson.Invoke();
        }
    }

    private void DisplayJSON(string sessionName, string json)
    {

            print(">>>> " + sessionName);
        print("> " + json);
        ProjectFilePath filePath = ProjectFilePath.CreateFrom(_jsonFolderPath);
        filePath.SetFileName(sessionName);
        filePath.SetFileType("json");

        print("<<< " + filePath.GetPath(false));
        ProjectPathTools.SaveFileAt(json, filePath);
    }
}
