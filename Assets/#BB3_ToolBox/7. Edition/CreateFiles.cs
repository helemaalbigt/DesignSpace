
 #if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class CreateFiles : MonoBehaviour {


    [MenuItem("Assets/Create/C#/Interface Script")]
    public static void CreateIntrefaceForCS()
    {
        string choosedPath = AskPathToUser("Create Interface ...", "I_InterfaceName", "cs");
        string[] lines = { "public interface " + Path.GetFileNameWithoutExtension(choosedPath) + " {", "", "     void Methode (  );", "", "     }" };
        CreateFile(choosedPath, lines);
    }
    [MenuItem("Assets/Create/C#/Enum Script")]
    public static void CreateEnumForCS()
    {
        string choosedPath = AskPathToUser("Create Enum ...", "EnumName", "cs");
        string[] lines = { "public enum " + Path.GetFileNameWithoutExtension(choosedPath) + " {   }" };
        CreateFile(choosedPath, lines);
    }

    public static string AskPathToUser(string title = "Create file ...", string defaultFileName = "FileName", string extension = "txt")
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        return EditorUtility.SaveFilePanel(title, path, defaultFileName, extension);
    }

    public static void CreateFileInProject(string[] lines, string filename, string extension = "txt")
    {
        if (lines != null && filename != null && filename.Length > 0)
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            path += "/" + filename + extension;
            CreateFile(path, lines);
        }

    }
    private static void CreateFile(string path, string[] lines)
    {

        if (path.Length > 0 && lines != null)
        {
            System.IO.File.WriteAllLines(@path, lines);
            AssetDatabase.Refresh();
        }

    }
}
#endif