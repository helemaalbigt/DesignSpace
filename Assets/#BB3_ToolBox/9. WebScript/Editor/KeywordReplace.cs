//Assets/Editor/KeywordReplace.cs
using UnityEngine;
using UnityEditor;
using System.Collections;
public class KeywordReplace : UnityEditor.AssetModificationProcessor
{

    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        string file = path.Substring(index);
        if (file != ".cs" && file != ".js" && file != ".boo") return;
        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        file = System.IO.File.ReadAllText(path);
        bool doApply = path.IndexOf("KeywordReplace.cs") < 0;
        if (doApply) { 
            file = file.Replace("#CREATIONDATE#", System.DateTime.Now + "");
            file = file.Replace("#PROJECTNAME#", PlayerSettings.productName);
            file = file.Replace("#COMPANYNAME#", PlayerSettings.companyName);
        }
        System.IO.File.WriteAllText(path, file);
        AssetDatabase.Refresh();
    }
}
