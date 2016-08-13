#if UNITY_EDITOR

/*
 * --------------------------BEER-WARE LICENSE--------------------------------
 * PrIMD42@gmail.com wrote this file. As long as you retain this notice you
 * can do whatever you want with this code. If you think
 * this stuff is worth it, you can buy me a beer in return, 
 *  S. E.
 * Donate a beer: http://www.primd.be/donate/ 
 * Contact: http://www.primd.be/
 * ----------------------------------------------------------------------------
 */
using UnityEngine;
using UnityEditor;
using System.Collections;

public class QuickUnityTrick {

    public static class Dialogue
    {

        public static string AskPathToUser(string title = "Create file ...", string defaultFileName = "FileName", string extension = "txt")
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            return EditorUtility.SaveFilePanel(title, path, defaultFileName, extension);
        }
    }

    public static class File
    {
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
}
#endif