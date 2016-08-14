using UnityEngine;
using System.Collections;
using System.IO;
using BlackBox.Tools.IO;

public class ProjectMinimumStructure : MonoBehaviour {
    [Header("The file must exist in your resources folder as txt !")]

    public ProjectDirectoryPath [] _folders;
    public ProjectFilePath[] _files;

    //Add button load selection resources folder
    //It auto field the class with a resource folder structure

        //Add button set all with this project name/

    void Start() {

        //for (int i = 0; i < _folders.Length; i++)
        //{
        //    _folders[i].CreateFolder();
        //}
        for (int i = 0; i < _files.Length; i++)
        {

            if (!_files[i].Exist()) {
                string resPath = _files[i].GetFilePathParts(false, false, true, true, false);

                Debug.Log("--> " + resPath);

                TextAsset fileContent = Resources.Load(resPath) as TextAsset;
              
                if (fileContent != null && ! string.IsNullOrEmpty(fileContent.text))
                {
                    Debug.LogWarning("Your file (" + resPath + ") is created: " + _files[i].GetPath(true)) ;
                   ProjectPathTools.SaveFileAt( fileContent.text, _files[i], true);
                }
                else Debug.LogWarning("Your file (" + resPath + ") do not exist in your resources folders");


            }


        }
        
    }
    
	
}
